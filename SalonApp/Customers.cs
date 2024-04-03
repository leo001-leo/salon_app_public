using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalonApp
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
        }

        private void btnPocetna_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();
            this.Close();
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employees dashboard = new Employees();
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

        private void btnCategories_Click(object sender, EventArgs e)
        {
            this.Hide();
            Categories dashboard = new Categories();
            dashboard.ShowDialog();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewCustomer add = new AddNewCustomer();
            AddNewCustomer.FromBlank = true;
            AddNewCustomer.FromEdit = false;
            add.ShowDialog();
        }

        private void btnAnalysis_Click(object sender, EventArgs e)
        {
            this.Hide();
            Analysis categories = new Analysis();
            categories.ShowDialog();
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
            objCommand.CommandText = "[dbo].[SelectCustomers]";

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
                    dgvCustomers.DataSource = dataset.Tables[0];

                    dgvCustomers.Columns["Id"].Visible = false;

                    dgvCustomers.Columns["Edit"].DisplayIndex = 10;
                    dgvCustomers.Columns["Delete"].DisplayIndex = 10;


                    dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    dgvCustomers.DefaultCellStyle.Font = new Font("Segoe UI", 12);

                    for (int i = 0; i < dgvCustomers.Rows.Count; i++)
                    {
                        dgvCustomers.Rows[i].Height = 35;
                    }

                    dgvCustomers.BackgroundColor = Form1.backColor;

                    dgvCustomers.DefaultCellStyle.BackColor = Form1.backColor;
                    dgvCustomers.DefaultCellStyle.ForeColor = Form1.whiteColor;
                    dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = Form1.backColor;
                    dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Form1.foreColor;
                    dgvCustomers.GridColor = Form1.whiteColor;
                    dgvCustomers.EnableHeadersVisualStyles = false;
                    dgvCustomers.BorderStyle = BorderStyle.None;
                    
                    dgvCustomers.Rows[0].Selected = false;
                    
                    //dgvEmployees.Columns[5].DefaultCellStyle.Format = "dd\\/MM\\/yyyy";


                    ////this.dgvVraboteni.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    foreach (DataGridViewRow myRow in dgvCustomers.Rows)
                    {
                        myRow.HeaderCell.Value = (myRow.Index + 1).ToString();
                        myRow.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9);
                        myRow.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        myRow.HeaderCell.Style.BackColor = Form1.backColor;
                        myRow.HeaderCell.Style.ForeColor = Form1.foreColor;
                    }
                    dgvCustomers.AllowUserToAddRows = false;
                    lbNumClients.Text = dgvCustomers.Rows.Count.ToString();
                   

                    //dgvVraboteni.BackgroundColor = OdmorVraboteniPrikaz.color;
                    //dgvVraboteni.GridColor = OdmorVraboteniPrikaz.color;
                    //dgvVraboteni.DefaultCellStyle.BackColor = OdmorVraboteniPrikaz.color;
                    //dgvVraboteni.DefaultCellStyle.ForeColor = OdmorVraboteniPrikaz.color2;
                    //dgvVraboteni.ColumnHeadersDefaultCellStyle.BackColor = OdmorVraboteniPrikaz.color;
                    //dgvVraboteni.ColumnHeadersDefaultCellStyle.ForeColor = OdmorVraboteniPrikaz.color2;
                    //dgvVraboteni.GridColor = OdmorVraboteniPrikaz.color2;
                    //dgvVraboteni.EnableHeadersVisualStyles = false;
                    //dgvVraboteni.BorderStyle = BorderStyle.None;

                    ////this.dgvVraboteni.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    //foreach (DataGridViewRow myRow in dgvVraboteni.Rows)
                    //{
                    //    myRow.HeaderCell.Value = (myRow.Index + 1).ToString();
                    //    myRow.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9);
                    //    myRow.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //    myRow.HeaderCell.Style.BackColor = OdmorVraboteniPrikaz.color;
                    //    myRow.HeaderCell.Style.ForeColor = OdmorVraboteniPrikaz.color2;
                    //}

                }
                else
                {
                    lbNumClients.Text="0";
                    dgvCustomers.Visible = false;
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

        private void Customers_Load(object sender, EventArgs e)
        {
            this.BackColor = Form1.backColor;
            btnClients.BackColor = Form1.foreColor;
            flpLeftNav.BackColor = Form1.backColor;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.ForeColor = Color.Black;
            lbTitle.ForeColor = Form1.foreColor;
            tbSearch.BackColor = Form1.backColor;
            tbSearch.ForeColor = Form1.whiteColor;
            btnExportExcel.BackColor = Form1.backColor;
            btnExportExcel.ForeColor = Form1.whiteColor;
            lbNumClients.Text = getCount().ToString();
            //fillDgv();
            fillGrid();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            fillGrid();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                //MessageBox.Show("Воспоставена конекција");
                string sqlQuery = "SELECT c.Id, c.Name as Име, c.Nickname as Прекар, c.Surname as Презиме,c.City as [Град],c.CustomerFrom as [Клиент од], c.PhoneNumber as [Мобилен телефон],c.Frequency as [Фреквенција],c.Notes as Забелешка FROM [dbo].Customer c WHERE c.Status='A' AND c.Name LIKE N'" + tbSearch.Text + "%'";
                SqlCommand objCommand = new SqlCommand(sqlQuery, conn);


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


                    //objCommand.Parameters.Clear();

                    /*objCommand.Parameters.Add("@KlucenZbor", SqlDbType.NVarChar).Value = tbSearch.Text;
                    objCommand.Parameters.Add("@DatumOd", SqlDbType.DateTime).Value = dtpVrabotenOd.Value;
                    objCommand.Parameters.Add("@DatumDo", SqlDbType.DateTime).Value = dtpVrabotenDo.Value;
                    /* DateTime od = dtpVrabotenOd.Value.Date;
                     DateTime doKraj = dtpVrabotenDo.Value.Date;
                     tbDenovi.Text = Convert.ToInt32(doKraj.Subtract(od).Days).ToString();*/

                    adapter = new SqlDataAdapter(objCommand);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);

                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        dgvCustomers.DataSource = dataset.Tables[0];
                        int numRows = dgvCustomers.Rows.Count;
                        lbNumClients.Text = numRows.ToString();
                        dgvCustomers.Columns["Id"].Visible = false;

                        dgvCustomers.Columns["Edit"].DisplayIndex = 10;
                        dgvCustomers.Columns["Delete"].DisplayIndex = 10;

                        //dgvSearch.Columns["IDEmployee"].Visible = false;
                        //dgvSearch.Columns["Status"].Visible = false;
                        /*dgvSearch.Columns["Име"].HeaderText = "Name";
                        dgvSearch.Columns["Презиме"].HeaderText = "Surname";
                        dgvSearch.Columns["Мобилен телефон"].HeaderText = "Mobile number";
                        dgvSearch.Columns["Град"].HeaderText = "City";
                        dgvSearch.Columns["Вкупно денови"].HeaderText = "Total days";
                        dgvSearch.Columns["Вработен од"].HeaderText = "Employed from";
                        dgvSearch.Columns["Вработен до"].HeaderText = "Employed to";*/
                        //dgvSearch.Columns["Возраст"].HeaderText = "Година на раѓање";
                        //dgvSearch.Columns[12].HeaderText = "Преостанати денови";
                        //dgvSearch.Columns["Адреса"].Visible = false;
                        //dgvSearch.Columns["Плата"].Visible = false;
                        //dgvSearch.Columns["Email"].Visible = false;
                        //dgvSearch.Columns["МатиченБрој"].Visible = false;
                        //dgvSearch.Columns["ТрансакцискаСметка"].Visible = false;
                        //dgvSearch.Columns[14].DefaultCellStyle.Format = "dd/MM/yyyy";
                        //dgvSearch.Columns[15].DefaultCellStyle.Format = "dd/MM/yyyy";
                        //dgvSearch.Columns[11].Visible = false;

                        //dgvSearch.Columns["IDPosition"].Visible = false;
                        //dgvSearch.Columns["IDChef"].Visible = false;
                        //dgvSearch.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                        //dgvSearch.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
                        //dgvSearch.AllowUserToAddRows = false;
                        for (int i = 0; i < dgvCustomers.Rows.Count; i++)
                        {
                            dgvCustomers.Rows[i].Height = 35;
                        }

                        foreach (DataGridViewRow myRow in dgvCustomers.Rows)
                        {
                            myRow.HeaderCell.Value = (myRow.Index + 1).ToString();
                            myRow.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9);
                            myRow.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            myRow.HeaderCell.Style.BackColor = Form1.backColor;
                            myRow.HeaderCell.Style.ForeColor = Form1.foreColor;
                        }
                        //dgvSearch.BackgroundColor = OdmorVraboteniPrikaz.color;
                        //dgvSearch.GridColor = OdmorVraboteniPrikaz.color;
                        //dgvSearch.BorderStyle = BorderStyle.None;

                        //dgvSearch.DefaultCellStyle.BackColor = OdmorVraboteniPrikaz.color;
                        //dgvSearch.DefaultCellStyle.ForeColor = OdmorVraboteniPrikaz.color2;
                        //dgvSearch.ColumnHeadersDefaultCellStyle.BackColor = OdmorVraboteniPrikaz.color;
                        //dgvSearch.ColumnHeadersDefaultCellStyle.ForeColor = OdmorVraboteniPrikaz.color2;
                        //dgvSearch.GridColor = OdmorVraboteniPrikaz.color2;
                        //dgvSearch.EnableHeadersVisualStyles = false;

                        //this.dgvSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    }
                    else
                    {
                        MessageBox.Show("Нема податоци");
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
        }

        private void tbSearch_Click(object sender, EventArgs e)
        {
            tbSearch.Text = "";
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dgvCustomers.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                dgvCustomers.Rows[e.RowIndex].Selected = false;
                AddNewCustomer add = new AddNewCustomer();
                AddNewCustomer.id = dgvCustomers.Rows[e.RowIndex].Cells[2].Value.ToString();
                AddNewCustomer.name = dgvCustomers.Rows[e.RowIndex].Cells[3].Value.ToString();
                AddNewCustomer.nickname = dgvCustomers.Rows[e.RowIndex].Cells[4].Value.ToString();
                AddNewCustomer.surname = dgvCustomers.Rows[e.RowIndex].Cells[5].Value.ToString();
                AddNewCustomer.city = dgvCustomers.Rows[e.RowIndex].Cells[6].Value.ToString();
                AddNewCustomer.phoneNumber = dgvCustomers.Rows[e.RowIndex].Cells[8].Value.ToString();
                //String date= .ToString();
                DateTime dateTime = Convert.ToDateTime(dgvCustomers.Rows[e.RowIndex].Cells[7].Value);
                AddNewCustomer.customerFrom = dateTime.ToString("dd/MM/yyyy");
                AddNewCustomer.notes = dgvCustomers.Rows[e.RowIndex].Cells[10].Value.ToString();
                AddNewCustomer.FromEdit = true;
                AddNewCustomer.FromBlank = false;
                add.lbTitle.Text = "АЖУРИРАЈ КЛИЕНТ";
                add.ShowDialog();


            }
            else if (colName == "Delete")
            {
                //MessageBox.Show("Delete");
                dgvCustomers.Rows[e.RowIndex].Selected = false;
                int id = Convert.ToInt32(dgvCustomers.Rows[e.RowIndex].Cells[2].Value.ToString());
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Дали сте сигурни дека сакате да го избришете клиентот?", "Бришење клиент", buttons);
                if (result == DialogResult.Yes)
                {
                    String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

                    SqlConnection conn = new SqlConnection(connectionString);
                    conn.Open();
                    //MessageBox.Show("Воспоставена конекција");

                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "[dbo].[DeleteCustomer]";

                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                        objCommand.Connection = conn;
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    try
                    {
                        if (objCommand.Connection.State == ConnectionState.Closed)
                        {
                            objCommand.Connection.Open();
                        }


                        if (objCommand.CommandType == CommandType.StoredProcedure)
                        {
                            objCommand.Parameters.Clear();

                            objCommand.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                        }
                        objCommand.ExecuteNonQuery();
                        MessageBox.Show("Успешно избришан клиент!");
                        fillDgv();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Настана SQL грешка " + ex.Message);
                    }
                    finally
                    {
                        adapter.Dispose();
                        objCommand.Dispose();
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
            }
            else
            {
                DataGridViewRow myRow = dgvCustomers.CurrentRow;
                String id = Convert.ToString(myRow.Cells["Id"].Value);
                String name= Convert.ToString(myRow.Cells["Име"].Value);
                String nickname = Convert.ToString(myRow.Cells["Прекар"].Value);
                String surname = Convert.ToString(myRow.Cells["Презиме"].Value);
                String nameSurname = "";
                if (nickname != "")
                {
                    nameSurname = name + " " + nickname + " " + surname;
                }
                else
                {
                    nameSurname = name + " " + surname;
                }
                AppointmentsCustomer customer = new AppointmentsCustomer();
                AppointmentsCustomer.id = id;
                AppointmentsCustomer.nameSurname = nameSurname;
                customer.Show();
                
                
            }
        }

        private void dgvCustomers_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                string colname = dgvCustomers.Columns[e.ColumnIndex].Name;
                if (colname != "Edit" && colname != "Delete")
                {
                    dgvCustomers.Cursor = Cursors.Default;
                }
                else
                {
                    dgvCustomers.Cursor = Cursors.Hand;
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string query = @"SELECT c.Name as Име, c.Nickname as Прекар, c.Surname as Презиме,c.City as [Град],c.CustomerFrom as [Клиент од],
	                        c.PhoneNumber as [Мобилен телефон],c.Frequency as [Фреквенција],c.Notes as Забелешка
	                        FROM [dbo].Customer c
	                        WHERE c.Status='A'";
            Appointments.exportToExcel(query, "Клиенти");
        }

        // Page
        private int mintTotalRecords = 0;
        private int mintPageSize = 0;
        private int mintPageCount = 0;
        private int mintCurrentPage = 1;

        private void fillGrid()
        {
            // For Page view.
            this.mintPageSize = int.Parse(this.tbPageSize.Text);
            this.mintTotalRecords = getCount();
            this.mintPageCount = this.mintTotalRecords / this.mintPageSize;

            // Adjust page count if the last page contains partial page.
            if (this.mintTotalRecords % this.mintPageSize > 0)
                this.mintPageCount++;

            this.mintCurrentPage = 0;

            loadPage();
        }
        protected SqlConnection mcnSample;
        private int getCount()
        {
            // This select statement is very fast compare to SELECT COUNT(*)
            //string strSql = "SELECT Rows FROM SYSINDEXES WHERE Id = OBJECT_ID('Customer') AND IndId < 2";
            string strSql = "SELECT COUNT(*) from Customer where Status = 'A'";
            int intCount = 0;
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            mcnSample = new SqlConnection(connectionString);
            mcnSample.Open();
            SqlCommand cmd = this.mcnSample.CreateCommand();
            cmd.CommandText = strSql;

            intCount = (int)cmd.ExecuteScalar();
            cmd.Dispose();

            return intCount;
        }

        private void loadPage()
        {
            string strSql = "";
            int intSkip = 0;

            intSkip = (this.mintCurrentPage * this.mintPageSize);

            // Select only the n records.
            strSql = @"SELECT TOP " + this.mintPageSize +
                @"c.Id, c.Name AS Име,
                c.Nickname AS Прекар,
                c.Surname AS Презиме,
                c.City AS [Град],
                c.CustomerFrom AS [Клиент од],
                c.PhoneNumber AS [Мобилен телефон],
                c.Frequency AS [Фреквенција],
                c.Notes AS Забелешка FROM Customer c
                WHERE
                c.Status = 'A' and c.Id NOT IN 
                (SELECT TOP " + intSkip + @" Id FROM Customer where Status='A')";
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            mcnSample = new SqlConnection(connectionString);
            mcnSample.Open();
            SqlCommand cmd = this.mcnSample.CreateCommand();
            cmd.CommandText = strSql;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            // Populate Data Grid
            this.dgvCustomers.DataSource = ds.Tables[0].DefaultView;

            dgvCustomers.Columns["Id"].Visible = false;

            dgvCustomers.Columns["Edit"].DisplayIndex = 10;
            dgvCustomers.Columns["Delete"].DisplayIndex = 10;


            dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvCustomers.DefaultCellStyle.Font = new Font("Segoe UI", 12);

            for (int i = 0; i < dgvCustomers.Rows.Count; i++)
            {
                dgvCustomers.Rows[i].Height = 35;
            }

            dgvCustomers.BackgroundColor = Form1.backColor;

            dgvCustomers.DefaultCellStyle.BackColor = Form1.backColor;
            dgvCustomers.DefaultCellStyle.ForeColor = Form1.whiteColor;
            dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = Form1.backColor;
            dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Form1.foreColor;
            dgvCustomers.GridColor = Form1.whiteColor;
            dgvCustomers.EnableHeadersVisualStyles = false;
            dgvCustomers.BorderStyle = BorderStyle.None;
            if (dgvCustomers.RowCount > 0)
            {
                dgvCustomers.Rows[0].Selected = false;
            }

            //dgvEmployees.Columns[5].DefaultCellStyle.Format = "dd\\/MM\\/yyyy";


            ////this.dgvVraboteni.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //foreach (DataGridViewRow myRow in dgvCustomers.Rows)
            //{
            //    myRow.HeaderCell.Value = (myRow.Index + 1).ToString();
            //    myRow.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9);
            //    myRow.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //    myRow.HeaderCell.Style.BackColor = Form1.backColor;
            //    myRow.HeaderCell.Style.ForeColor = Form1.foreColor;
            //}
            dgvCustomers.AllowUserToAddRows = false;

            // Show Status
            this.lblStatus.Text = (this.mintCurrentPage + 1).ToString() +
              " / " + this.mintPageCount.ToString();

            cmd.Dispose();
            da.Dispose();
            ds.Dispose();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            this.mintCurrentPage++;

            if (this.mintCurrentPage > (this.mintPageCount - 1))
            {
                this.mintCurrentPage = this.mintPageCount - 1;
            }
            loadPage();
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (this.mintCurrentPage == this.mintPageCount)
            {
                this.mintCurrentPage = this.mintPageCount - 1;
            }
            this.mintCurrentPage--;

            if (this.mintCurrentPage < 1)
            {
                this.mintCurrentPage = 0;
            }
            loadPage();
        }
    }
}
