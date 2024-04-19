using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalonApp
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            timer1.Start();
            btnPocetna.BackColor = Form1.foreColor;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            //btnDashboard.ForeColor = Form1.foreColor;
            flpLeftNav.BackColor = Form1.backColor;
            this.BackColor = Form1.backColor;
            flpStatusDown.BackColor = Form1.backColor;
            flpStatusDown.ForeColor = Form1.whiteColor;
            //DateTime date = DateTime.Now.Date.ToString();
            lbDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection connection = new SqlConnection(connectionString);
            
            string sql = string.Format("select count(distinct Id) as Count from Appointment where CONVERT(date,StartTime)=(select cast(GETDATE() as date)) and Status='A'");
            connection.Open();
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lbTodaysNumAppointments.Text = reader["Count"].ToString();
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            string sql2 = string.Format("select count(distinct Id) as Count from Employee where Status='A'");
            connection.Open();
            SqlCommand cmd2 = new SqlCommand(sql2, connection);
            
            SqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                lbNumEmployees.Text = reader2["Count"].ToString();
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            string sql3 = string.Format("select count(distinct Id) as Count from Customer where Status='A'");
            connection.Open();
            SqlCommand cmd3 = new SqlCommand(sql3, connection);

            SqlDataReader reader3 = cmd3.ExecuteReader();
            while (reader3.Read())
            {
                lbNumClients.Text = reader3["Count"].ToString();
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            string sql4 = string.Format("select Case when SUM(CAST(TotalPrice as smallint))>0 then SUM(CAST(TotalPrice as smallint)) else 0 end as Sum from Appointment where CONVERT(date,StartTime)=(select CAST(GETDATE() as date)) and Status='A'");
            connection.Open();
            SqlCommand cmd4 = new SqlCommand(sql4, connection);

            SqlDataReader reader4 = cmd4.ExecuteReader();
            while (reader4.Read())
            {
                CultureInfo customCulture = new CultureInfo("en-US");
                customCulture.NumberFormat.NumberGroupSeparator = ".";
                string formattedNumber = Int32.Parse(reader4["Sum"].ToString()).ToString("#,0.##", customCulture);
                lbTotalBilling.Text = formattedNumber + " ден.";
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            String connectionString2 = ConfigurationManager.AppSettings["ConnectionString"];

            SqlConnection conn = new SqlConnection(connectionString2);
            conn.Open();
            DateTime date = DateTime.Now.Date;
            //MessageBox.Show("Успешна конекција");

            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "[dbo].[SelectAppointmentsSpecificDate]";

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
                    objCommand.Parameters.Add("@Datum", SqlDbType.Date).Value = date;
                }
                adapter = new SqlDataAdapter(objCommand);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = dataset.Tables[0];

                    dataGridView1.Columns["Id"].Visible = false;
                    dataGridView1.Columns["Status"].Visible = false;

                    dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 12);

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Height = 35;
                    }
                    dataGridView1.Columns[1].DefaultCellStyle.Format = "HH:mm";
                    dataGridView1.Columns[6].DefaultCellStyle.Format = "HH:mm";
                    dataGridView1.BackgroundColor = Form1.backColor;
                    //dataGridView1.Columns["Edit"].DisplayIndex = 9;
                    //dataGridView1.Columns["Delete"].DisplayIndex = 9;
                    dataGridView1.DefaultCellStyle.BackColor = Form1.backColor;
                    dataGridView1.DefaultCellStyle.ForeColor = Form1.whiteColor;
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Form1.backColor;
                    dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Form1.foreColor;
                    dataGridView1.GridColor = Form1.whiteColor;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    dataGridView1.Rows[0].Selected = false;
                    dataGridView1.BorderStyle = BorderStyle.None;
                    //dgvEmployees.Columns[5].DefaultCellStyle.Format = "dd\\/MM\\/yyyy";


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
                    //lbNumEmployees.Text = dataGridView1.Rows.Count.ToString();
                }
                else
                {
                    //MessageBox.Show("Нема внесено термини");
                    dataGridView1.Visible = false;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Проверете ја конекцијата со интернет.");
            }
            finally
            {

                objCommand.Dispose();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            //UpdateControlPositions();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            lbDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        public void btnEmployees_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employees em = new Employees();
            em.ShowDialog();
            this.Close();
        }

        public void btnAppointments_Click(object sender, EventArgs e)
        {
            this.Hide();
            Appointments em = new Appointments();
            em.ShowDialog();
            this.Close();
        }

        public void btnCategories_Click(object sender, EventArgs e)
        {
            this.Hide();
            Categories categories = new Categories();
            categories.ShowDialog();
            this.Close();
        }

        public void btnClients_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customers categories = new Customers();
            categories.ShowDialog();
            this.Close();
        }

        public void btnAnalysis_Click(object sender, EventArgs e)
        {
            this.Hide();
            Analysis analysis = new Analysis();
            analysis.ShowDialog();
            this.Close();
        }

        private void Dashboard_Resize(object sender, EventArgs e)
        {
            //UpdateControlPositions();
        }

        public static void openNewTab(Form currentForm, Form desiredForm)
        {
            currentForm.Hide();
            desiredForm.ShowDialog();
            currentForm.Close();
        }

        private void btnComparativeAnalysis_Click(object sender, EventArgs e)
        {
            openNewTab(this, new ComparativeAnalysis());
        }

        //private void UpdateControlPositions()
        //{
        //    // Set the percentage position you want (50% from the left, 50% from the top).
        //    double positionPercentageX = 0.6; // 50%
        //    double positionPercentageY = 0.6; // 50%

        //    // Calculate the new position based on the form's size.
        //    int newX = (int)(this.ClientSize.Width * positionPercentageX) - dataGridView1.Width / 2;
        //    int newY = (int)(this.ClientSize.Height * positionPercentageY) - dataGridView1.Height / 2;

        //    // Set the new location for the control.
        //    dataGridView1.Location = new Point(newX, newY);
        //}


    }
}
