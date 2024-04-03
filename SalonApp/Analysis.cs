using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Configuration;
using System.Data.SqlClient;

namespace SalonApp
{
    public partial class Analysis : Form
    {
        public Analysis()
        {
            InitializeComponent();
        }

        private void Analysis_Load(object sender, EventArgs e)
        {
            this.BackColor = Form1.backColor;
            lbDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            flpLeftNav.BackColor = Form1.backColor;
            btnMonthly.BackColor = Form1.foreColor;
            btnMonthly.ForeColor = Form1.whiteColor;
            lbTitle.ForeColor = Form1.foreColor;
            btnAnalysis.BackColor = Form1.foreColor;
            cmbMonths.DataSource = CultureInfo.InvariantCulture.DateTimeFormat
                                                     .MonthNames.Take(12).ToList();
            cmbMonths.SelectedItem = CultureInfo.InvariantCulture.DateTimeFormat
                                                    .MonthNames[DateTime.Now.AddMonths(-1).Month];

            cmbYear.DataSource = Enumerable.Range(2020, DateTime.Now.Year - 2020 + 3).ToList();
            cmbYear.SelectedItem = DateTime.Now.Year;
            cmbMonths.BackColor = Form1.backColor;
            cmbMonths.ForeColor = Form1.whiteColor;
            cmbYear.BackColor = Form1.backColor;
            cmbYear.ForeColor = Form1.whiteColor;
            fillDgv();
        }

        private void btnPocetna_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();
            this.Close();
        }

        public void fillDgv()
        {
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            //MessageBox.Show("Успешна конекција");

            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "[dbo].[SelectLoyalCustomers]";

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
                adapter = new SqlDataAdapter(objCommand);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = dataset.Tables[0];

                    //dataGridView1.Columns["Id"].Visible = false;




                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 12);

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Height = 35;
                    }

                    dataGridView1.BackgroundColor = Form1.backColor;

                    dataGridView1.DefaultCellStyle.BackColor = Form1.backColor;
                    dataGridView1.DefaultCellStyle.ForeColor = Form1.whiteColor;
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Form1.backColor;
                    dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Form1.foreColor;
                    dataGridView1.GridColor = Form1.whiteColor;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    dataGridView1.BorderStyle = BorderStyle.None;
                    dataGridView1.Rows[0].Selected = false;
                    //dataGridView1.Columns["Edit"].DisplayIndex = 5;
                    //dataGridView1.Columns["Delete"].DisplayIndex = 5;

                    ////this.dgvVraboteni.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    foreach (DataGridViewRow myRow in dataGridView1.Rows)
                    {
                        myRow.HeaderCell.Value = (myRow.Index + 1).ToString();
                        myRow.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9);
                        myRow.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        myRow.HeaderCell.Style.BackColor = Form1.backColor;
                        myRow.HeaderCell.Style.ForeColor = Form1.foreColor;
                    }
                    dataGridView1.AllowUserToAddRows = false;

                }
                else
                {
                    dataGridView1.Visible = false;
                    MessageBox.Show("Нема внесено клиенти");
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

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employees dashboard = new Employees();
            dashboard.ShowDialog();
            this.Close();
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customers dashboard = new Customers();
            dashboard.ShowDialog();
            this.Close();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            this.Hide();
            Categories dashboard = new Categories();
            dashboard.ShowDialog();
            this.Close();
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            this.Hide();
            Appointments dashboard = new Appointments();
            dashboard.ShowDialog();
            this.Close();
        }

        private void cbAnnually_CheckedChanged(object sender, EventArgs e)
        {
            cmbMonths.Visible = !cbAnnually.Checked;
            lbMonth.Visible = !cbAnnually.Checked;
        }

        public void countServices(string query, Label label)
        {
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = string.Format(query);
            connection.Open();

            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Add("@DateFrom", SqlDbType.Date).Value = dtpFrom.Value.ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@DateTo", SqlDbType.Date).Value = dtpTo.Value.ToString("yyyy-MM-dd");
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (label.Name == lbTotalBilling.Name)
                {
                    CultureInfo customCulture = new CultureInfo("en-US");
                    customCulture.NumberFormat.NumberGroupSeparator = ".";
                    string formattedNumber = Int32.Parse(reader["Count"].ToString()).ToString("#,0.##", customCulture);
                    label.Text = formattedNumber + " ден.";
                }
                else
                {
                    label.Text = reader["Count"].ToString();
                }
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            if(dtpTo.Value.Date < dtpFrom.Value.Date)
            {
                MessageBox.Show("Вредноста во филтерот \"До\" треба да е поголема од \"Од\"");
            }
            else
            {
                //int godina = Convert.ToInt32(cmbYear.SelectedItem);

                string sql = string.Format("select count(distinct Id) as Count from Appointment where CONVERT(Date,StartTime)>=@DateFrom and CONVERT(Date,StartTime)<=@DateTo and Status='A'");
                countServices(sql, lbNumAppointments);

                string sql2 = string.Format("select Case when SUM(CAST(TotalPrice as smallint))> 0 then SUM(CAST(TotalPrice as smallint)) else 0 end as Count  from Appointment where CONVERT(Date,StartTime)>=@DateFrom and CONVERT(Date,StartTime)<=@DateTo and Status='A'");
                countServices(sql2, lbTotalBilling);

                string sql3 = string.Format("select count(Id) as Count from Customer where CONVERT(Date,CustomerFrom)>=@DateFrom and CONVERT(Date,CustomerFrom)<=@DateTo and Status='A'");
                countServices(sql3, lbNumNewClients);

                string sql4 = string.Format(@"select count(app.Id) as Count
                                            from Appointment app
                                            cross apply string_split(RTRIM(REPLACE(app.CategoryIds,',',' ')),' ')
                                            join Category cat
                                            on cat.Id=value
                                            where CONVERT(date,app.StartTime)>=@DateFrom and CONVERT(date,app.FinishTime)<=@DateTo and cat.Id >= 22 and cat.Id <= 31 and app.Status='A'");
                countServices(sql4, lbEndodoncija);

                string sql5 = string.Format(@"select count(app.Id) as Count
                                        from Appointment app
                                        cross apply string_split(RTRIM(REPLACE(app.CategoryIds,',',' ')),' ')
                                        join Category cat
                                        on cat.Id=value
                                        where CONVERT(date,app.StartTime)>=@DateFrom and CONVERT(date,app.FinishTime)<=@DateTo and cat.Id >= 32 and cat.Id <= 36 and app.Status='A'");
                countServices(sql5, lbHirurgija);

                string sql6 = string.Format(@"select count(app.Id) as Count
                                        from Appointment app
                                        cross apply string_split(RTRIM(REPLACE(app.CategoryIds,',',' ')),' ')
                                        join Category cat
                                        on cat.Id=value
                                        where CONVERT(date,app.StartTime)>=@DateFrom and CONVERT(date,app.FinishTime)<=@DateTo and cat.Id >= 132 and cat.Id <= 142 and app.Status='A'");
                countServices(sql6, lbProtetika);

                string sql7 = string.Format(@"select count(app.Id) as Count
                                        from Appointment app
                                        cross apply string_split(RTRIM(REPLACE(app.CategoryIds,',',' ')),' ')
                                        join Category cat
                                        on cat.Id=value
                                        where CONVERT(date,app.StartTime)>=@DateFrom and CONVERT(date,app.FinishTime)<=@DateTo and cat.Id >= 37 and cat.Id <= 40 and app.Status='A'");
                countServices(sql7, lbParodontologija);

                string sql8 = string.Format(@"select count(app.Id) as Count
                                            from Appointment app
                                            cross apply string_split(RTRIM(REPLACE(app.CategoryIds,',',' ')),' ')
                                            join Category cat
                                            on cat.Id=value
                                            where CONVERT(date,app.StartTime)>=@DateFrom and CONVERT(date,app.FinishTime)<=@DateTo and cat.Id>=18 and cat.Id <= 21 and app.Status='A'");
                countServices(sql8, lbRestavrativnaStomatologija);

            }
            
        }
    }
}
