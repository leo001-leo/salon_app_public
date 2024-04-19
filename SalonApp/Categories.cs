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
    public partial class Categories : Form
    {
        public Categories()
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
        public void fillDgv()
        {
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            //MessageBox.Show("Успешна конекција");

            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "[dbo].[SelectCategories]";

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

                    dataGridView1.Columns["Id"].Visible = false;

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
                    dataGridView1.Columns["Edit"].DisplayIndex = 6;
                    dataGridView1.Columns["Delete"].DisplayIndex = 6;

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
                    MessageBox.Show("Нема внесено категории");
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
        private void Categories_Load(object sender, EventArgs e)
        {
            btnCategories.BackColor = Form1.foreColor;
            flpLeftNav.BackColor = Form1.backColor;
            this.BackColor = Form1.backColor;
            flpStatusDown.BackColor = Form1.backColor;
            flpStatusDown.ForeColor = Form1.whiteColor;
            lbDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            lbTitle.ForeColor = Form1.foreColor;
            btnAdd.BackColor = Form1.foreColor;
            btnAdd.ForeColor = Form1.whiteColor;
            fillDgv();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewCategory add = new AddNewCategory();
            AddNewCategory.FromBlank = true;
            AddNewCategory.FromEdit = false;
            add.ShowDialog();
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customers categories = new Customers();
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

        private void pbRefresh_Click(object sender, EventArgs e)
        {
            fillDgv();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                dataGridView1.Rows[e.RowIndex].Selected = false;
                AddNewCategory add = new AddNewCategory();
                AddNewCategory.id = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                AddNewCategory.name = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                AddNewCategory.description = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                AddNewCategory.price = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                AddNewCategory.durationVariable = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                AddNewCategory.FromEdit = true;
                AddNewCategory.FromBlank = false;
                add.lbTitle.Text = "АЖУРИРАЈ КАТЕГОРИЈА";
                add.ShowDialog();


            }
            else if (colName == "Delete")
            {
                //MessageBox.Show("Delete");
                dataGridView1.Rows[e.RowIndex].Selected = false;
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Дали сте сигурни дека сакате да ја избришете категоријата?", "Бришење категорија", buttons);
                if (result == DialogResult.Yes)
                {
                    String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

                    SqlConnection conn = new SqlConnection(connectionString);
                    conn.Open();
                    //MessageBox.Show("Воспоставена конекција");

                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "[dbo].[DeleteCategory]";

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
                        MessageBox.Show("Успешно избришана категорија!");
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
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                string colname = dataGridView1.Columns[e.ColumnIndex].Name;
                if (colname == "Edit" || colname == "Delete")
                {
                    dataGridView1.Cursor = Cursors.Hand;
                }
                else
                {
                    dataGridView1.Cursor = Cursors.Default;
                }
            }
        }

        private void btnComparativeAnalysis_Click(object sender, EventArgs e)
        {
            Dashboard.openNewTab(currentForm: this, desiredForm: new ComparativeAnalysis());
        }
    }
}
