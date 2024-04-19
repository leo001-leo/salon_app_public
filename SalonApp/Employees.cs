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
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            btnEmployees.BackColor = Form1.foreColor;
            timer1.Start();
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
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            lbDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        public void fillDgv()
        {
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            //MessageBox.Show("Успешна конекција");

            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "[dbo].[SelectEmployees]";

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
                    dgvEmployees.DataSource = dataset.Tables[0];

                    dgvEmployees.Columns["Id"].Visible = false;




                    dgvEmployees.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    dgvEmployees.DefaultCellStyle.Font = new Font("Segoe UI", 12);

                    for (int i = 0; i < dgvEmployees.Rows.Count; i++)
                    {
                        dgvEmployees.Rows[i].Height = 35;
                    }

                    dgvEmployees.BackgroundColor = Form1.backColor;
                    dgvEmployees.Columns["Edit"].DisplayIndex = 10;
                    dgvEmployees.Columns["Delete"].DisplayIndex = 10;
                    dgvEmployees.DefaultCellStyle.BackColor = Form1.backColor;
                    dgvEmployees.DefaultCellStyle.ForeColor = Form1.whiteColor;
                    dgvEmployees.ColumnHeadersDefaultCellStyle.BackColor = Form1.backColor;
                    dgvEmployees.ColumnHeadersDefaultCellStyle.ForeColor = Form1.foreColor;
                    dgvEmployees.GridColor = Form1.whiteColor;
                    dgvEmployees.EnableHeadersVisualStyles = false;
                    dgvEmployees.Rows[0].Selected = false;
                    dgvEmployees.BorderStyle = BorderStyle.None;
                    //dgvEmployees.Columns[5].DefaultCellStyle.Format = "dd\\/MM\\/yyyy";


                    ////this.dgvVraboteni.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    foreach (DataGridViewRow myRow in dgvEmployees.Rows)
                    {
                        myRow.HeaderCell.Value = (myRow.Index + 1).ToString();
                        myRow.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9);
                        myRow.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        myRow.HeaderCell.Style.BackColor = Form1.backColor;
                        myRow.HeaderCell.Style.ForeColor = Form1.foreColor;
                    }
                    dgvEmployees.AllowUserToAddRows = false;
                    lbNumEmployees.Text = dgvEmployees.Rows.Count.ToString();
                }
                else
                {
                    dgvEmployees.Visible = false;
                    lbNumEmployees.Text = "0";
                    MessageBox.Show("Нема внесено вработени");
                    
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

        private void Employees_Load(object sender, EventArgs e)
        {
            flpLeftNav.BackColor = Form1.backColor;
            this.BackColor = Form1.backColor;
            flpStatusDown.BackColor = Form1.backColor;
            flpStatusDown.ForeColor = Form1.whiteColor;
            //DateTime date = DateTime.Now.Date.ToString();
            lbDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            tbSearch.BackColor = Form1.backColor;
            tbSearch.ForeColor = Form1.whiteColor;
            btnAdd.BackColor = Form1.foreColor;
            btnAdd.ForeColor = Form1.whiteColor;
            dgvEmployees.AllowUserToAddRows = false;
            lbTitle.ForeColor = Form1.foreColor;
            btnExportExcel.BackColor = Form1.backColor;
            btnExportExcel.ForeColor = Form1.whiteColor;
            fillDgv();
        }

        private void tbSearch_Click(object sender, EventArgs e)
        {
            tbSearch.Text = "";
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            this.Hide();
            Appointments dashboard = new Appointments();
            dashboard.ShowDialog();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddANewEmployee addANewEmployee = new AddANewEmployee();
            AddANewEmployee.FromBlank = true;
            AddANewEmployee.FromEdit = false;
            addANewEmployee.ShowDialog();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            this.Hide();
            Categories categories = new Categories();
            categories.ShowDialog();
            this.Close();
        }

        private void btnAnalysis_Click(object sender, EventArgs e)
        {
            this.Hide();
            Analysis categories = new Analysis();
            categories.ShowDialog();
            this.Close();
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customers categories = new Customers();
            categories.ShowDialog();
            this.Close();
        }

        private void pbRefresh_Click(object sender, EventArgs e)
        {
            fillDgv();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                //MessageBox.Show("Воспоставена конекција");
                string sqlQuery = "select e.Id,e.Name as Име,e.Surname as Презиме, e.City as [Град],e.YearOfBirth as [Година на раѓање],e.PhoneNumber as [Телефонски број], e.EmployedFrom as [Вработен од],e.NumberOfClients as [Број на клиенти]  from Employee e WHERE e.Name LIKE N'" + tbSearch.Text + "%' AND e.Status = 'A'";
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
                        dgvEmployees.DataSource = dataset.Tables[0];
                        int numRows = dgvEmployees.Rows.Count;
                        lbNumEmployees.Text = numRows.ToString();
                        dgvEmployees.Columns["Edit"].DisplayIndex = 9;
                        dgvEmployees.Columns["Delete"].DisplayIndex = 9;
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
                        for (int i = 0; i < dgvEmployees.Rows.Count; i++)
                        {
                            dgvEmployees.Rows[i].Height = 35;
                        }

                        foreach (DataGridViewRow myRow in dgvEmployees.Rows)
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

        private void dgvEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dgvEmployees.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                dgvEmployees.Rows[e.RowIndex].Selected = false;
                AddANewEmployee add = new AddANewEmployee(); 
                AddANewEmployee.id= dgvEmployees.Rows[e.RowIndex].Cells[2].Value.ToString();
                AddANewEmployee.name = dgvEmployees.Rows[e.RowIndex].Cells[3].Value.ToString();
                AddANewEmployee.nickname = dgvEmployees.Rows[e.RowIndex].Cells[4].Value.ToString();
                AddANewEmployee.surname = dgvEmployees.Rows[e.RowIndex].Cells[5].Value.ToString();
                AddANewEmployee.city = dgvEmployees.Rows[e.RowIndex].Cells[6].Value.ToString();
                AddANewEmployee.phoneNumber = dgvEmployees.Rows[e.RowIndex].Cells[8].Value.ToString();
                //String date= .ToString();
                DateTime dateTime = Convert.ToDateTime(dgvEmployees.Rows[e.RowIndex].Cells[9].Value);
                AddANewEmployee.employedFrom = dateTime.ToString("dd/MM/yyyy");
                AddANewEmployee.yearOfBirth = dgvEmployees.Rows[e.RowIndex].Cells[7].Value.ToString();
                AddANewEmployee.FromEdit = true;
                AddANewEmployee.FromBlank = false;
                add.lbTitle.Text = "АЖУРИРАЈ ВРАБОТЕН";
                add.groupBox1.Visible = false;
                add.ShowDialog();
                
                
            }
            else if (colName == "Delete")
            {
                //MessageBox.Show("Delete");
                dgvEmployees.Rows[e.RowIndex].Selected = false;
                int id = Convert.ToInt32(dgvEmployees.Rows[e.RowIndex].Cells[2].Value.ToString());
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Дали сте сигурни дека сакате да го избришете вработениот?", "Бришење вработен", buttons);
                if (result == DialogResult.Yes)
                {
                    String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

                    SqlConnection conn = new SqlConnection(connectionString);
                    conn.Open();
                    //MessageBox.Show("Воспоставена конекција");

                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "[dbo].[DeleteEmployee]";

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
                        MessageBox.Show("Успешно избришан вработен!");
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
                DataGridViewRow myRow = dgvEmployees.CurrentRow;
                String id = Convert.ToString(myRow.Cells["Id"].Value);
                String name = Convert.ToString(myRow.Cells["Име"].Value);
                String nickname = Convert.ToString(myRow.Cells["Прекар"].Value);
                String surname = Convert.ToString(myRow.Cells["Презиме"].Value);
                String totalSum = "";
                String connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                SqlConnection connection = new SqlConnection(connectionString);
                string sql = string.Format(@"select e.Id,
	                                            CASE WHEN(SUM(CONVERT(smallint,a.TotalPrice)))is not null then (CONCAT(SUM(CONVERT(smallint,a.TotalPrice)),' ',N'ден.')) else '0 ден.' end as Сума 
	                                            from Appointment a
	                                            full join Employee e
	                                            on e.Id=a.EmployeeId
	                                            where e.Status='A' and e.Id='"+Convert.ToInt16(id)+"' and a.Status='A' group by e.Id");
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    totalSum = reader["Сума"].ToString();
                }
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                String nameSurname = "";
                if (nickname != "")
                {
                    nameSurname = name + " " + nickname + " " + surname;
                }
                else
                {
                    nameSurname = name + " " + surname;
                }
                AppointmentsEmployee customer = new AppointmentsEmployee();
                AppointmentsEmployee.id = id;
                AppointmentsEmployee.nameSurname = nameSurname;
                AppointmentsEmployee.totalSum = totalSum;
                customer.Show();


            }
        }

        private void dgvEmployees_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                string colname = dgvEmployees.Columns[e.ColumnIndex].Name;
                if (colname == "Edit" || colname == "Delete")
                {
                    dgvEmployees.Cursor = Cursors.Hand;
                }
                else
                {
                    dgvEmployees.Cursor = Cursors.Default;
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string query = @"select e.Name as Име, e.Nickname as Прекар, e.Surname as Презиме, e.City as [Град],e.YearOfBirth as [Година на раѓање],
	                        e.PhoneNumber as [Телефонски број], e.EmployedFrom as [Вработен од],CONVERT(smallint,e.NumberOfClients) as [Број на клиенти]
	                        from Employee e
	                        where e.Status='A'
	                        group by e.Id,e.Name, e.Nickname, e.Surname, e.City, e.YearOfBirth, e.PhoneNumber, e.EmployedFrom, 
	                        CONVERT(smallint,e.NumberOfClients)";
            Appointments.exportToExcel(query, "Вработени");
        }

        private void btnComparativeAnalysis_Click(object sender, EventArgs e)
        {
            Dashboard.openNewTab(currentForm: this, desiredForm: new ComparativeAnalysis());
        }
    }
}
