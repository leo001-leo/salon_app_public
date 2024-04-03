using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SalonApp
{
    public partial class AppointmentsCustomer : Form
    {
        public AppointmentsCustomer()
        {
            InitializeComponent();
        }
        public static String id, nameSurname;
        int currentPage = 1;
        int itemsPerPage = 25;
        DataTable customerAppointments = new DataTable();
        private void AppointmentsCustomer_Load(object sender, EventArgs e)
        {
            int customerId = Convert.ToInt16(id); // Replace with the specific customer ID you want to display appointments for.

            this.BackColor = Form1.backColor;
            lbCustomer.ForeColor = Form1.foreColor;

            customerId = Convert.ToInt16(id); // Replace id with the customer ID.
            LoadAppointmentsForCustomer(customerId);

            // Now, call a method to bind the data to the DataGridView with pagination.
            BindDataToDataGridView();
        }

        private void LoadAppointmentsForCustomer(int customerId)
        {
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand objCommand = new SqlCommand("[dbo].[SelectAppointmentsSpecificCustomer]", conn))
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
                        adapter.Fill(customerAppointments);

                        if (customerAppointments.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = customerAppointments;

                            dataGridView1.AllowUserToAddRows = false;
                            dataGridView1.Columns["Id"].Visible = false;
                            dataGridView1.Columns["Status"].Visible = false;
                            lbCustomer.Text = nameSurname;

                            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
                            //dataGridView1.Columns[1].Width = 100;
                            //dataGridView1.Columns[2].Width = 100;
                            //dataGridView1.Columns[4].Width = 150;
                            //dataGridView1.Columns[5].Width = 150;
                            //dataGridView1.Columns[6].Width = 40;
                        }
                        else
                        {
                            this.Close();
                            MessageBox.Show("Нема внесено термини за одбраниот клиент!");
                        }
                    }
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

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            btnPreviousPage.Visible = true;
            if (currentPage < Math.Ceiling((double)customerAppointments.Rows.Count / itemsPerPage))
            {
                currentPage++; // Increment the current page.
                BindDataToDataGridView();
                if (currentPage == Math.Ceiling((double)customerAppointments.Rows.Count / itemsPerPage))
                {
                    btnNextPage.Visible = false;
                }
            }
        }

        private void BindDataToDataGridView()
        {
            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, customerAppointments.Rows.Count);

            if (startIndex >= 0 && endIndex > startIndex)
            {
                DataTable dataToShow = customerAppointments.AsEnumerable()
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
