using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalonApp
{
    public partial class ComparativeAnalysis : Form
    {
        public ComparativeAnalysis()
        {
            InitializeComponent();
        }

        private readonly string cmbOriginalTextDenes = "Денес";
        private readonly string cmbOriginalTextTekovenMesec = "Тековен месец";
        private readonly string cmbOriginalTextTekovnaGodina = "Тековна година";
        private readonly string cmbTextOpseg = "Опсег";
        private readonly string cmbComparisonTextPrethodenDen = "Претходен ден";
        private readonly string cmbComparisonTextPrethodenMesec = "Претходен месец";
        private readonly string cmbComparisonTextPrethodnaGodina = "Претходна година";
        public Label[] labelsComparison;
        private readonly string errorMessageVrednostiSporedbaPonovi = "Невалиден внес! Вредностите за споредба треба да се постари во однос на останатите вредности!";
        private readonly string errorMessageKraenDatumPonov = "Невалиден внес! Крајниот датум треба да е постар од почетниот!";

        private void ComparativeAnalysis_Load(object sender, EventArgs e)
        {
            this.BackColor = Form1.backColor;
            flpLeftNav.BackColor = Form1.backColor;
            cmbOriginal.BackColor = Form1.backColor;
            cmbComparison.BackColor = Form1.backColor;
            cmbOriginal.ForeColor = Form1.whiteColor;
            cmbComparison.ForeColor = Form1.whiteColor;
            btnComparativeAnalysis.BackColor = Form1.foreColor;
            lbSporedba.ForeColor = Form1.foreColor;
            lbKompAnaliza.ForeColor = Form1.foreColor;
            UpdateControlPositions();
            labelsComparison = new Label[]
            {
                lbAppointmentsComparison,
                lbTotalBillingComparison,
                lbNewClientsComparison,
                lbEndodoncijaComparison,
                lbHirurgijaComparison,
                lbParodontologijaComparison,
                lbRestavrativnaStomatologijaComparison,
                lbProtetikaComparison
            };
        }

        private void cmbComparison_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Претходен ден
            //Претходен месец
            //Претходна година
            //Опсег

            if (cmbComparison.SelectedItem.ToString() == cmbComparisonTextPrethodenDen)
            {
                DateTime previousDay = DateTime.Now.Date.AddDays(-1);

                if (previousDay.Year == previousDay.AddDays(1).Year &&
                    (cmbOriginal.Text == cmbOriginalTextTekovenMesec ||
                    (dtpFrom.Value.Date <= previousDay.Date &&
                    dtpTo.Value.Date >= previousDay.Date)))
                {
                    MessageBox.Show(errorMessageKraenDatumPonov);
                    return;
                }

                setVisibilityDateTimePickers_Button_Label(isComparison: true);

                lbDateComparison.Text = previousDay.ToString("dd.MM.yyyy");
                CalculateCountOfCategories(previousDay, null, null, true);
                CalculateResultOfComparison();
            }

            else if (cmbComparison.SelectedItem.ToString() == cmbComparisonTextPrethodenMesec)
            {
                // Get today's date
                DateTime today = DateTime.Today;

                // Get the first day of the current month
                DateTime firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);

                // Get the first day of the previous month
                DateTime firstDayOfPreviousMonth = firstDayOfCurrentMonth.AddMonths(-1);

                // Get the last day of the previous month
                DateTime lastDayOfPreviousMonth = firstDayOfCurrentMonth.AddDays(-1);

                if (lastDayOfPreviousMonth.Year == firstDayOfCurrentMonth.Year &&
                    cmbOriginal.Text == cmbOriginalTextTekovnaGodina)
                {
                    MessageBox.Show(errorMessageKraenDatumPonov);
                    return;
                }

                if (dtpFrom.Visible == true || dtpTo.Visible == true)
                {
                    if (dtpFrom.Value.Month == lastDayOfPreviousMonth.Month ||
                        dtpTo.Value.Month == lastDayOfPreviousMonth.Month)
                    {
                        MessageBox.Show(errorMessageVrednostiSporedbaPonovi);
                        return;
                    }
                }

                setVisibilityDateTimePickers_Button_Label(isComparison: true);

                lbDateComparison.Text = firstDayOfPreviousMonth.ToString("dd.MM.yyyy") + " - " + lastDayOfPreviousMonth.ToString("dd.MM.yyyy");
                CalculateCountOfCategories(null, firstDayOfPreviousMonth, lastDayOfPreviousMonth, true);
                CalculateResultOfComparison();
            }

            else if (cmbComparison.SelectedItem.ToString() == cmbComparisonTextPrethodnaGodina)
            {
                // Get today's date
                DateTime today = DateTime.Today;

                // Get the first day of the previous year
                DateTime firstDayOfPreviousYear = new DateTime(today.AddYears(-1).Year, 1, 1);

                // Get the first day of the current year
                DateTime firstDayOfCurrentYear = firstDayOfPreviousYear.AddYears(1);

                // Get the last day of the previous year
                DateTime lastDayOfPreviousYear = firstDayOfCurrentYear.AddDays(-1);

                setVisibilityDateTimePickers_Button_Label(isComparison: true);

                if (dtpFrom.Visible == true || dtpTo.Visible == true)
                {
                    if (dtpFrom.Value.Year == lastDayOfPreviousYear.Year ||
                       dtpTo.Value.Year == lastDayOfPreviousYear.Year)
                    {
                        MessageBox.Show(errorMessageVrednostiSporedbaPonovi);
                        return;
                    }
                }

                lbDateComparison.Text = firstDayOfPreviousYear.ToString("dd.MM.yyyy") + " - " + lastDayOfPreviousYear.ToString("dd.MM.yyyy");
                CalculateCountOfCategories(null, firstDayOfPreviousYear, lastDayOfPreviousYear, true);
                CalculateResultOfComparison();
            }

            else if (cmbComparison.SelectedItem.ToString() == cmbTextOpseg)
            {
                lbDateComparison.Visible = false;
                dtpFromComparison.Visible = true;
                dtpToComparison.Visible = true;
                btnSearchComparative.Visible = true;
            }
        }

        private void cmbOriginal_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbComparison.Visible = true;

            //Денес
            //Тековен месец
            //Тековна година
            //Опсег

            if (cmbOriginal.SelectedItem.ToString() == cmbOriginalTextDenes)
            {
                setVisibilityDateTimePickers_Button_Label(isComparison: false);

                lbDateOriginal.Text = DateTime.Now.Date.ToString("dd.MM.yyyy");

                resetComparisonValues();

                CalculateCountOfCategories(DateTime.Now, null, null, false);
                CalculateResultOfComparison();
            }

            if (cmbOriginal.SelectedItem.ToString() == cmbOriginalTextTekovenMesec)
            {
                // Get today's date
                DateTime today = DateTime.Today;

                // Get the first day of the current month
                DateTime firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);

                // Get the first day of the next month
                DateTime firstDayOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);

                // Subtract one day from the first day of the next month to get the last day of the current month
                DateTime lastDayOfCurrentMonth = firstDayOfNextMonth.AddDays(-1);

                resetComparisonValues();

                setVisibilityDateTimePickers_Button_Label(isComparison: false);

                lbDateOriginal.Text = firstDayOfCurrentMonth.ToString("dd.MM.yyyy") + " - " + lastDayOfCurrentMonth.ToString("dd.MM.yyyy");
                CalculateCountOfCategories(null, firstDayOfCurrentMonth, lastDayOfCurrentMonth, false);
                CalculateResultOfComparison();
            }

            if (cmbOriginal.SelectedItem.ToString() == cmbOriginalTextTekovnaGodina)
            {
                // Get today's date
                DateTime today = DateTime.Today;

                // Get the first day of the current year
                DateTime firstDayOfCurrentYear = new DateTime(today.Year, 1, 1);

                // Get the first day of the current year
                DateTime firstDayOfNextYear = firstDayOfCurrentYear.AddYears(1);

                // Get the last day of the previous year
                DateTime lastDayOfCurrentYear = firstDayOfNextYear.AddDays(-1);

                resetComparisonValues();

                setVisibilityDateTimePickers_Button_Label(isComparison: false);

                lbDateOriginal.Text = firstDayOfCurrentYear.ToString("dd.MM.yyyy") + " - " + lastDayOfCurrentYear.ToString("dd.MM.yyyy");
                CalculateCountOfCategories(null, firstDayOfCurrentYear, lastDayOfCurrentYear, false);
                CalculateResultOfComparison();
            }

            else if (cmbOriginal.SelectedItem.ToString() == cmbTextOpseg)
            {
                lbDateOriginal.Visible = false;
                dtpTo.Visible = true;
                dtpFrom.Visible = true;
                btnSearch.Visible = true;
            }
        }

        private void CalculateCountOfCategories(DateTime? date, DateTime? dateFrom, DateTime? dateTo, bool isComparison)
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand objCommand = new SqlCommand("[dbo].[SelectCountCategoriesDateRange]", conn))
                {
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.Add("@Date", SqlDbType.DateTime).Value = (object)date ?? DBNull.Value;
                    objCommand.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = (object)dateFrom ?? DBNull.Value;
                    objCommand.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = (object)dateTo ?? DBNull.Value;

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = objCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    UpdateUI(reader, isComparison);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No data returned from the database.");
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
        }

        private void UpdateUI(SqlDataReader reader, bool isComparison)
        {
            CultureInfo customCulture = new CultureInfo("en-US");
            customCulture.NumberFormat.NumberGroupSeparator = ".";
            string formattedNumberCountBilling = Int32.Parse(reader["CountBilling"].ToString()).ToString("#,0.##", customCulture) + " ден.";
            string countNewCustomers = reader["CountNewCustomers"].ToString();
            string countAppointments = reader["CountAppointments"].ToString();
            string countEndodoncija = reader["CountEndodoncija"].ToString();
            string countHirurgija = reader["CountHirurgija"].ToString();
            string countRestavrativnaStomatologija = reader["CountRestavrativnaStomatologija"].ToString();
            string countProtetika = reader["CountProtetika"].ToString();
            string countParodontologija = reader["CountParodontologija"].ToString();

            if (!isComparison)
            {
                lbTotalBilling.Text = formattedNumberCountBilling;
                lbNewClients.Text = countNewCustomers;
                lbAppointments.Text = countAppointments;
                lbEndodoncija.Text = countEndodoncija;
                lbHirurgija.Text = countHirurgija;
                lbRestavrativnaStomatologija.Text = countRestavrativnaStomatologija;
                lbProtetika.Text = countProtetika;
                lbParodontologija.Text = countParodontologija;
            }
            else
            {
                lbTotalBillingComparison.Text = formattedNumberCountBilling;
                lbNewClientsComparison.Text = countNewCustomers;
                lbAppointmentsComparison.Text = countAppointments;
                lbEndodoncijaComparison.Text = countEndodoncija;
                lbHirurgijaComparison.Text = countHirurgija;
                lbRestavrativnaStomatologijaComparison.Text = countRestavrativnaStomatologija;
                lbProtetikaComparison.Text = countProtetika;
                lbParodontologijaComparison.Text = countParodontologija;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime dtpFromValue = dtpFrom.Value.Date;
            DateTime dtpToValue = dtpTo.Value.Date;

            if (dtpToValue >= dtpFromValue)
            {
                resetComparisonValues();

                CalculateCountOfCategories(null, dtpFromValue, dtpToValue, false);
                CalculateResultOfComparison();
            }
            else
            {
                MessageBox.Show(errorMessageKraenDatumPonov);
                return;
            }
        }

        private void btnSearchComparative_Click(object sender, EventArgs e)
        {
            DateTime dtpFromComparisonValue = dtpFromComparison.Value.Date;
            DateTime dtpToComparisonValue = dtpToComparison.Value.Date;
            DateTime dateTimeNow = DateTime.Now.Date;
            DateTime dtpFromValue = dtpFrom.Value.Date;
            DateTime dtpToValue = dtpTo.Value.Date;

            if (dtpToComparisonValue >= dtpFromComparisonValue)
            {
                if (cmbOriginal.Text == cmbOriginalTextDenes)
                {
                    if (dtpFromComparisonValue >= dateTimeNow &&
                        dtpToComparisonValue >= dateTimeNow)
                    {
                        CalculateCountOfCategories(null, dtpFromComparisonValue, dtpToComparisonValue, true);
                        CalculateResultOfComparison();
                    }
                    else
                    {
                        MessageBox.Show(errorMessageVrednostiSporedbaPonovi);
                        return;
                    }
                }

                else if (cmbOriginal.Text == cmbOriginalTextTekovenMesec)
                {
                    // Get today's date
                    DateTime today = DateTime.Today;

                    // Get the first day of the current month
                    DateTime firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);

                    // Get the first day of the next month
                    DateTime firstDayOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);

                    // Subtract one day from the first day of the next month to get the last day of the current month
                    DateTime lastDayOfCurrentMonth = firstDayOfNextMonth.AddDays(-1);

                    if (dtpFromComparisonValue < firstDayOfCurrentMonth &&
                        dtpFromComparisonValue < lastDayOfCurrentMonth &&
                        dtpToComparisonValue < lastDayOfCurrentMonth &&
                        dtpToComparisonValue < firstDayOfCurrentMonth)
                    {
                        CalculateCountOfCategories(null, dtpFromComparisonValue, dtpToComparisonValue, true);
                        CalculateResultOfComparison();
                    }

                    else
                    {
                        MessageBox.Show(errorMessageVrednostiSporedbaPonovi);
                        return;
                    }
                }

                else if (cmbOriginal.Text == cmbOriginalTextTekovnaGodina)
                {
                    // Get today's date
                    DateTime today = DateTime.Today;

                    // Get the first day of the current year
                    DateTime firstDayOfCurrentYear = new DateTime(today.Year, 1, 1);

                    // Get the first day of the current year
                    DateTime firstDayOfNextYear = firstDayOfCurrentYear.AddYears(1);

                    // Get the last day of the previous year
                    DateTime lastDayOfCurrentYear = firstDayOfNextYear.AddDays(-1);

                    if (dtpFromComparisonValue < firstDayOfCurrentYear &&
                        dtpFromComparisonValue < lastDayOfCurrentYear &&
                        dtpToComparisonValue < firstDayOfCurrentYear &&
                        dtpToComparisonValue < lastDayOfCurrentYear)
                    {
                        CalculateCountOfCategories(null, dtpFromComparisonValue, dtpToComparisonValue, true);
                        CalculateResultOfComparison();
                    }

                    else
                    {
                        MessageBox.Show(errorMessageVrednostiSporedbaPonovi);
                        return;
                    }
                }

                else if (cmbOriginal.Text == cmbTextOpseg)
                {
                    if (dtpFromComparisonValue < dtpFromValue &&
                        dtpFromComparisonValue < dtpToValue &&
                        dtpToComparisonValue < dtpToValue &&
                        dtpToComparisonValue < dtpFromValue)
                    {
                        CalculateCountOfCategories(null, dtpFromComparisonValue, dtpToComparisonValue, true);
                        CalculateResultOfComparison();
                    }
                    else
                    {
                        MessageBox.Show(errorMessageVrednostiSporedbaPonovi);
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show(errorMessageKraenDatumPonov);
                return;
            }
        }

        private void CalculateResultOfComparison()
        {
            // Promet
            CalculateResultNumbersFromComparison(lbTotalBilling, lbTotalBillingComparison, lbTotalBillingResult);
            SetVisibilityOfArrows(lbTotalBillingResult, pbIncreaseTotalBilling, pbDecreaseTotalBilling);

            // Novi klienti
            CalculateResultNumbersFromComparison(lbNewClients, lbNewClientsComparison, lbNewClientsResult);
            SetVisibilityOfArrows(lbNewClientsResult, pbIncreaseNewClients, pbDecreaseNewClients);

            // Termini
            CalculateResultNumbersFromComparison(lbAppointments, lbAppointmentsComparison, lbAppointmentsResult);
            SetVisibilityOfArrows(lbAppointmentsResult, pbIncreaseAppointments, pbDecreaseAppointments);

            // Endodoncija
            CalculateResultNumbersFromComparison(lbEndodoncija, lbEndodoncijaComparison, lbEndodoncijaResult);
            SetVisibilityOfArrows(lbEndodoncijaResult, pbIncreaseEndodoncija, pbDecreaseEndodoncija);

            // Hirurgija
            CalculateResultNumbersFromComparison(lbHirurgija, lbHirurgijaComparison, lbHirurgijaResult);
            SetVisibilityOfArrows(lbHirurgijaResult, pbIncreaseHirurgija, pbDecreaseHirurgija);

            // Restavrativna stomatologija
            CalculateResultNumbersFromComparison(lbRestavrativnaStomatologija, lbRestavrativnaStomatologijaComparison, lbRestavrativnaStomatologijaResult);
            SetVisibilityOfArrows(lbRestavrativnaStomatologijaResult, pbIncreaseRestavrativnaStomatologija, pbDecreaseRestavrativnaStomatologija);

            // Protetika
            CalculateResultNumbersFromComparison(lbProtetika, lbProtetikaComparison, lbProtetikaResult);
            SetVisibilityOfArrows(lbProtetikaResult, pbIncreaseProtetika, pbDecreaseProtetika);

            // Parodontologija
            CalculateResultNumbersFromComparison(lbParodontologija, lbParodontologijaComparison, lbParodontologijaResult);
            SetVisibilityOfArrows(lbParodontologijaResult, pbIncreaseParodontologija, pbDecreaseParodontologija);
        }

        private void CalculateResultNumbersFromComparison(Label label, Label labelComparison, Label labelResult)
        {
            decimal totalBilling = Convert.ToDecimal(label.Text.Split(' ').First().Replace(".", ""));
            decimal totalBillingComparison = Convert.ToDecimal(labelComparison.Text.Split(' ').First().Replace(".", ""));
            CultureInfo customCulture = new CultureInfo("en-US");
            customCulture.NumberFormat.NumberGroupSeparator = ".";
            decimal temp = totalBilling - totalBillingComparison;
            string result = totalBillingComparison != 0 ?
                            Math.Floor(
                                        ((totalBilling - totalBillingComparison) / totalBillingComparison) * 100
                                      )
                            .ToString() :
                            totalBillingComparison == 0 ?
                            Math.Floor(
                                        ((totalBilling - totalBillingComparison) / 1) * 100
                                      )
                           .ToString() : "0";

            string formattedNumber = Int32.Parse(result).ToString("#,0.##", customCulture);
            labelResult.Text = formattedNumber + " %";
        }

        private void SetVisibilityOfArrows(Label labelResult, PictureBox increase, PictureBox decrease)
        {
            if (Convert.ToDecimal(labelResult.Text.Split(' ').First().Replace(".", "")) > 0)
            {
                decrease.Visible = false;
                increase.Visible = true;
            }
            else if (Convert.ToDecimal(labelResult.Text.Split(' ').First().Replace(".", "")) == 0)
            {
                increase.Visible = false;
                decrease.Visible = false;
            }
            else
            {
                increase.Visible = false;
                decrease.Visible = true;
            }
        }

        private void resetComparisonValues()
        {
            foreach (Label lb in labelsComparison)
            {
                lb.Text = "0";
            }
        }

        private void setVisibilityDateTimePickers_Button_Label(bool isComparison)
        {
            if (!isComparison)
            {
                dtpTo.Visible = false;
                dtpFrom.Visible = false;
                btnSearch.Visible = false;
                lbDateOriginal.Visible = true;
            }
            else
            {
                dtpFromComparison.Visible = false;
                dtpToComparison.Visible = false;
                btnSearchComparative.Visible = false;
                lbDateComparison.Visible = true;
            }
        }

        private void btnPocetna_Click(object sender, EventArgs e)
        {
            Dashboard.openNewTab(currentForm: this, desiredForm: new Dashboard());
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            Dashboard.openNewTab(currentForm: this, desiredForm: new Employees());
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            Dashboard.openNewTab(currentForm: this, desiredForm: new Appointments());
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            Dashboard.openNewTab(currentForm: this, desiredForm: new Categories());
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            Dashboard.openNewTab(currentForm: this, desiredForm: new Customers());
        }

        private void btnAnalysis_Click(object sender, EventArgs e)
        {
            Dashboard.openNewTab(currentForm: this, desiredForm: new Analysis());
        }

        private const int OriginalScreenWidth = 1920;
        private const int OriginalScreenHeight = 1080;

        // Original location
        private const int OriginalLocationX = 620;
        private const int OriginalLocationComparisonX = 1223;
        private const int OriginalLocationResultLabelX = 1717;
        private const int OriginalLocationResultPictureBoxX = 1687;
        private const int OriginalLocationSecondVerticalDividerX = 933;

        private const int OriginalLocationFirstRowY = 241;
        private const int OriginalLocationSecondRowY = 321;
        private const int OriginalLocationThirdRowY = 401;
        private const int OriginalLocationFourthRowY = 481;
        private const int OriginalLocationFifthRowY = 561;
        private const int OriginalLocationSixthRowY = 648;
        private const int OriginalLocationSeventhRowY = 741;
        private const int OriginalLocationEightRowY = 821;
        private const int OriginalLocationSecondVerticalDividerY = 209;


        private void UpdateControlPositions()
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            // Calculate the ratio of current resolution to original resolution
            float widthRatio = (float)screenWidth / OriginalScreenWidth;
            float heightRatio = (float)screenHeight / OriginalScreenHeight;

            // Calculate the new location based on the ratio
            int newX = (int)(OriginalLocationX * widthRatio);
            int newComparisonX = (int)(OriginalLocationComparisonX * widthRatio);
            int newResultLabelX = (int)(OriginalLocationResultLabelX * widthRatio);
            int newResultPictureBoxX = (int)(OriginalLocationResultPictureBoxX * widthRatio);
            int newResultSecondVerticalDividerX = (int)(OriginalLocationSecondVerticalDividerX * widthRatio);

            int newY = (int)(OriginalLocationFirstRowY * heightRatio);
            int newSecondRowY = (int)(OriginalLocationSecondRowY * heightRatio);
            int newThirdRowY = (int)(OriginalLocationThirdRowY * heightRatio);
            int newFourthRowY = (int)(OriginalLocationFourthRowY * heightRatio);
            int newFifthRowY = (int)(OriginalLocationFifthRowY * heightRatio);
            int newSixthRowY = (int)(OriginalLocationSixthRowY * heightRatio);
            int newSeventhRowY = (int)(OriginalLocationSeventhRowY * heightRatio);
            int newEightRowY = (int)(OriginalLocationEightRowY * heightRatio);
            int newResultSecondVerticalDividerY = (int)(OriginalLocationSecondVerticalDividerY * heightRatio);

            // Set the new location
            secondVerticalDivider.Location = new Point(newResultSecondVerticalDividerX, newResultSecondVerticalDividerY);

            lbTotalBilling.Location = new Point(newX, newY);
            lbAppointments.Location = new Point(newX, newSecondRowY);
            lbNewClients.Location = new Point(newX, newThirdRowY);
            lbEndodoncija.Location = new Point(newX, newFourthRowY);
            lbHirurgija.Location = new Point(newX, newFifthRowY);
            lbRestavrativnaStomatologija.Location = new Point(newX, newSixthRowY);
            lbProtetika.Location = new Point(newX, newSeventhRowY);
            lbParodontologija.Location = new Point(newX, newEightRowY);


            lbTotalBillingComparison.Location = new Point(newComparisonX, newY);
            lbAppointmentsComparison.Location = new Point(newComparisonX, newSecondRowY);
            lbNewClientsComparison.Location = new Point(newComparisonX, newThirdRowY);
            lbEndodoncijaComparison.Location = new Point(newComparisonX, newFourthRowY);
            lbHirurgijaComparison.Location = new Point(newComparisonX, newFifthRowY);
            lbRestavrativnaStomatologijaComparison.Location = new Point(newComparisonX, newSixthRowY);
            lbProtetikaComparison.Location = new Point(newComparisonX, newSeventhRowY);
            lbParodontologijaComparison.Location = new Point(newComparisonX, newEightRowY);

            ///result column
            pbIncreaseTotalBilling.Location = new Point(newResultPictureBoxX, newY);
            pbDecreaseTotalBilling.Location = new Point(newResultPictureBoxX, newY);
            lbTotalBillingResult.Location = new Point(newResultLabelX, newY);

            pbIncreaseAppointments.Location = new Point(newResultPictureBoxX, newSecondRowY);
            pbDecreaseAppointments.Location = new Point(newResultPictureBoxX, newSecondRowY);
            lbAppointmentsResult.Location = new Point(newResultLabelX, newSecondRowY);

            pbIncreaseNewClients.Location = new Point(newResultPictureBoxX, newThirdRowY);
            pbDecreaseNewClients.Location = new Point(newResultPictureBoxX, newThirdRowY);
            lbNewClientsResult.Location = new Point(newResultLabelX, newThirdRowY);

            pbIncreaseEndodoncija.Location = new Point(newResultPictureBoxX, newFourthRowY);
            pbDecreaseEndodoncija.Location = new Point(newResultPictureBoxX, newFourthRowY);
            lbEndodoncijaResult.Location = new Point(newResultLabelX, newFourthRowY);

            pbIncreaseHirurgija.Location = new Point(newResultPictureBoxX, newFifthRowY);
            pbDecreaseHirurgija.Location = new Point(newResultPictureBoxX, newFifthRowY);
            lbHirurgijaResult.Location = new Point(newResultLabelX, newFifthRowY);

            pbIncreaseRestavrativnaStomatologija.Location = new Point(newResultPictureBoxX, newSixthRowY);
            pbDecreaseRestavrativnaStomatologija.Location = new Point(newResultPictureBoxX, newSixthRowY);
            lbRestavrativnaStomatologijaResult.Location = new Point(newResultLabelX, newSixthRowY);

            pbIncreaseProtetika.Location = new Point(newResultPictureBoxX, newSeventhRowY);
            pbDecreaseProtetika.Location = new Point(newResultPictureBoxX, newSeventhRowY);
            lbProtetikaResult.Location = new Point(newResultLabelX, newSeventhRowY);

            pbIncreaseParodontologija.Location = new Point(newResultPictureBoxX, newEightRowY);
            pbDecreaseParodontologija.Location = new Point(newResultPictureBoxX, newEightRowY);
            lbParodontologijaResult.Location = new Point(newResultLabelX, newEightRowY);
        }
    }
}
