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
    public partial class AppointmentsEmployee : Form
    {
        public AppointmentsEmployee()
        {
            InitializeComponent();
        }
        public static String id, nameSurname, totalSum;

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            lbPrometPresmetan.Text = calculatePercentage();
        }
        public string calculatePercentage()
        {
            string[] parts = lbPromet.Text.Split(' ');
            string num = parts[0].Replace(".", "");
            int promet = Convert.ToInt32(num);
            int percent = Convert.ToInt32(tbPercent.Text);
            double result = (percent / 100.0) * promet;
            return result.ToString() + " ден.";
        }


        private void tbPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                lbPrometPresmetan.Text = calculatePercentage();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnNextPage.Visible = false;
            btnPreviousPage.Visible = false;
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "[dbo].[SelectAppointmentsSpecificDatesFromToEmployee]";

            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
                objCommand.Connection = conn;
            }
            SqlDataAdapter adapter = new SqlDataAdapter(objCommand);
            try
            {
                if (objCommand.Connection.State == ConnectionState.Closed)
                {
                    objCommand.Connection.Open();
                }
                if (objCommand.CommandType == CommandType.StoredProcedure)
                {
                    objCommand.Parameters.Clear();
                    objCommand.Parameters.Add("@ID", SqlDbType.SmallInt).Value = Convert.ToInt16(id);
                    objCommand.Parameters.Add("@DateFrom", SqlDbType.Date).Value = dtpFrom.Value.ToString("yyyy-MM-dd");
                    objCommand.Parameters.Add("@DateTo", SqlDbType.Date).Value = dtpTo.Value.ToString("yyyy-MM-dd");
                }
                adapter = new SqlDataAdapter(objCommand);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.Visible = true;
                    dataGridView1.DataSource = dataset.Tables[0];
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.Columns["Id"].Visible = false;
                    dataGridView1.Columns["Status"].Visible = false;
                    lbEmployee.Text = nameSurname;
                    lbPromet.Text = totalSum;
                    dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    //dataGridView1.Columns["ParentId"].Visible = false;
                    //dataGridView1.Columns["Name"].HeaderText = "Име";
                    //dataGridView1.Columns["Description"].HeaderText = "Опис";
                    //dataGridView1.Columns["Price"].HeaderText = "Цена";
                    //dataGridView1.Columns["Duration"].HeaderText = "Времетраење";

                    //dgvVraboteni.Columns["Возраст"].HeaderText = "Година на раѓање";
                    //dgvVraboteni.Columns["Адреса"].Visible = false;
                    //dgvVraboteni.Columns["Плата"].Visible = false;
                    //dgvVraboteni.Columns["Email"].Visible = false;
                    //dgvVraboteni.Columns["Матичен број"].Visible = false;
                    //dgvVraboteni.Columns["Трансакциска сметка"].Visible = false;
                    dataGridView1.Columns[4].DefaultCellStyle.Format = "HH:mm";
                    dataGridView1.Columns[9].DefaultCellStyle.Format = "HH:mm";
                    //dataGridView1.Columns["Edit"].DisplayIndex = 11;
                    //dataGridView1.Columns["Delete"].DisplayIndex = 11;
                    //dataGridView1.Columns["StatusCheckBox"].DisplayIndex = 11;
                    //dgvVraboteni.Columns[11].HeaderText = "Преостанати денови";
                    //dgvVraboteni.Columns["IDChef"].Visible = false;
                    //dgvVraboteni.Columns["IDPosition"].Visible = false;
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 12);

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Height = 30;
                    }

                    string sql = string.Format(@"select e.Id, CONCAT(SUM(CONVERT(smallint,a.TotalPrice)),' ',N'ден.') as Сума 
	                                            from Appointment a
	                                            join Employee e
	                                            on e.Id=a.EmployeeId
	                                            where e.Status='A' and e.Id=@Id and CONVERT(date,a.StartTime)>=@DateFrom and CONVERT(date,a.FinishTime)<=@DateTo and a.Status='A'
	                                            group by e.Id,e.Name, e.Nickname, e.Surname, e.City, e.YearOfBirth, e.PhoneNumber, e.EmployedFrom, 
	                                            CONVERT(smallint,e.NumberOfClients)
	                                            order by SUM(CONVERT(smallint,a.TotalPrice)) desc");


                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@Id", SqlDbType.SmallInt).Value = Convert.ToInt16(id);
                    cmd.Parameters.Add("@DateFrom", SqlDbType.Date).Value = dtpFrom.Value.ToString("yyyy-MM-dd");
                    cmd.Parameters.Add("@DateTo", SqlDbType.Date).Value = dtpTo.Value.ToString("yyyy-MM-dd");
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CultureInfo customCulture = new CultureInfo("en-US");
                        customCulture.NumberFormat.NumberGroupSeparator = ".";
                        string[] parts = reader["Сума"].ToString().Split(' ');

                        string formattedNumber = Int32.Parse(parts[0]).ToString("#,0.##", customCulture);
                        lbPromet.Text = formattedNumber + " ден.";
                    }



                    dataGridView1.BackgroundColor = Form1.backColor;
                    dataGridView1.Rows[0].Selected = false;
                    dataGridView1.DefaultCellStyle.BackColor = Form1.backColor;
                    dataGridView1.DefaultCellStyle.ForeColor = Form1.whiteColor;
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Form1.backColor;
                    dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Form1.foreColor;
                    dataGridView1.GridColor = Form1.whiteColor;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    dataGridView1.BorderStyle = BorderStyle.None;
                    dataGridView1.Columns[1].Width = 100;
                    dataGridView1.Columns[2].Width = 100;
                    dataGridView1.Columns[4].Width = 150;
                    dataGridView1.Columns[5].Width = 150;
                    //datagv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    // datagv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    foreach (DataGridViewRow myRow in dataGridView1.Rows)
                    {
                        myRow.HeaderCell.Value = (myRow.Index + 1).ToString();
                        //myRow.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9);
                        myRow.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        myRow.HeaderCell.Style.BackColor = Form1.backColor;
                        myRow.HeaderCell.Style.ForeColor = Form1.foreColor;
                    }
                    //lbTotAppoints.Text = Convert.ToString(datagv.Rows.Count);

                }
                else
                {
                    dataGridView1.Visible = false;
                    lbPromet.Text = "";
                    MessageBox.Show("Нема внесено термини за одбраниот вработен!");

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Nastana greshka");
            }
            finally
            {

                objCommand.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void tbPercent_Click(object sender, EventArgs e)
        {
            tbPercent.Text = "";
        }
        int currentPage = 1;
        int itemsPerPage = 25;
        DataTable employeeAppointments = new DataTable();
        private void AppointmentsEmployee_Load(object sender, EventArgs e)
        {
            this.BackColor = Form1.backColor;
            lbEmployee.ForeColor = Form1.foreColor;
            tbPercent.BackColor = Form1.backColor;
            int employeeId = Convert.ToInt16(id);
            LoadAppointmentsForEmployee(employeeId);

            // Now, call a method to bind the data to the DataGridView with pagination.
            BindDataToDataGridView();
            
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            btnPreviousPage.Visible = true;
            if (currentPage < Math.Ceiling((double)employeeAppointments.Rows.Count / itemsPerPage))
            {
                currentPage++; // Increment the current page.
                BindDataToDataGridView();
                if (currentPage == Math.Ceiling((double)employeeAppointments.Rows.Count / itemsPerPage))
                {
                    btnNextPage.Visible = false;
                }
            }
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--; // Decrement the current page.
                BindDataToDataGridView();

                // Make sure the "Next Page" button is visible when you go back to a previous page.
                btnNextPage.Visible = true;
                if (currentPage == 1)
                {
                    btnPreviousPage.Visible = false;
                }
            }
        }
        private void LoadAppointmentsForEmployee(int customerId)
        {
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand objCommand = new SqlCommand("[dbo].[SelectAppointmentsSpecificEmployee]", conn))
                {
                    objCommand.CommandType = CommandType.StoredProcedure;

                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                        objCommand.Connection = conn;
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(objCommand))
                    {
                        if (objCommand.Connection.State == ConnectionState.Closed)
                        {
                            objCommand.Connection.Open();
                        }
                        if (objCommand.CommandType == CommandType.StoredProcedure)
                        {
                            objCommand.Parameters.Clear();
                            objCommand.Parameters.Add("@ID", SqlDbType.SmallInt).Value = customerId;
                        }
                        adapter.Fill(employeeAppointments);

                        if (employeeAppointments.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = employeeAppointments;

                            dataGridView1.AllowUserToAddRows = false;
                            dataGridView1.Columns["Id"].Visible = false;
                            dataGridView1.Columns["Status"].Visible = false;
                            
                            lbEmployee.Text = nameSurname;
                            CultureInfo customCulture = new CultureInfo("en-US");
                            customCulture.NumberFormat.NumberGroupSeparator = ".";
                            string[] parts = totalSum.Split(' ');

                            string formattedNumber = Int32.Parse(parts[0]).ToString("#,0.##", customCulture);
                            lbPromet.Text = formattedNumber + " ден.";
                            //lbPromet.Text = totalSum;
                            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 12);


                            dataGridView1.BackgroundColor = Form1.backColor;
                            dataGridView1.Rows[0].Selected = false;
                            dataGridView1.DefaultCellStyle.BackColor = Form1.backColor;
                            dataGridView1.DefaultCellStyle.ForeColor = Form1.whiteColor;
                            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Form1.backColor;
                            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Form1.foreColor;
                            dataGridView1.GridColor = Form1.whiteColor;
                            dataGridView1.EnableHeadersVisualStyles = false;
                            dataGridView1.BorderStyle = BorderStyle.None;
                            dataGridView1.Columns[1].Width = 100;
                            dataGridView1.Columns[2].Width = 100;
                            dataGridView1.Columns[4].Width = 150;
                            dataGridView1.Columns[5].Width = 150;
                        }
                        else
                        {
                            this.Close();
                            MessageBox.Show("Нема внесено термини за одбраниот вработен!");
                        }
                    }
                }
            }
        }

        private void BindDataToDataGridView()
        {
            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, employeeAppointments.Rows.Count);

            if (startIndex >= 0 && endIndex > startIndex)
            {
                DataTable dataToShow = employeeAppointments.AsEnumerable()
                    .Skip(startIndex)
                    .Take(endIndex - startIndex)
                    .CopyToDataTable();

                dataGridView1.DataSource = dataToShow;
                if (dataToShow.Rows.Count >= itemsPerPage)
                {
                    btnNextPage.Visible = true;
                }
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }
    }
}
