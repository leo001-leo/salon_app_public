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
    public partial class AddANewEmployee : Form
    {
        public AddANewEmployee()
        {
            InitializeComponent();
        }
        public static String id, name, nickname, surname, yearOfBirth, city, phoneNumber, employedFrom;
        public int Id = -1;
        public static bool FromEdit = false;
        public static bool FromBlank = true;

        private void AddANewEmployee_Load(object sender, EventArgs e)
        {
            this.BackColor = Form1.backColor;
            lbTitle.ForeColor = Form1.foreColor;
            tbName.BackColor = Form1.backColor;
            tbName.ForeColor = Form1.foreColor;
            tbNickname.BackColor = Form1.backColor;
            tbNickname.ForeColor = Form1.foreColor;
            tbSurname.BackColor = Form1.backColor;
            tbSurname.ForeColor = Form1.foreColor;
            tbYearOfBirth.BackColor = Form1.backColor;
            tbYearOfBirth.ForeColor = Form1.foreColor;
            tbCity.BackColor = Form1.backColor;
            tbCity.ForeColor = Form1.foreColor;
            mtbPhoneNumber.BackColor = Form1.backColor;
            mtbPhoneNumber.ForeColor = Form1.foreColor;
            mtbEmployedFrom.BackColor = Form1.backColor;
            mtbEmployedFrom.ForeColor = Form1.foreColor;
            clearTextBoxes();
            if (FromEdit == true && FromBlank == false)
            {
                Id = Convert.ToInt32(id);
                tbName.Text = name;
                tbName.SelectionLength = 0;
                tbName.SelectionStart = tbName.Text.Length;
                tbNickname.Text = nickname;
                tbSurname.Text = surname;
                tbYearOfBirth.Text = yearOfBirth;
                tbCity.Text = city;
                mtbPhoneNumber.Text = phoneNumber;
                mtbEmployedFrom.Text = employedFrom;
            }
            
        }
        public void clearTextBoxes()
        {
            tbName.Text = "";
            tbNickname.Text = "";
            tbSurname.Text = "";
            tbYearOfBirth.Text = "";
            tbCity.Text = "";
            mtbPhoneNumber.Text = "07";
            if (rbAdmin.Checked)
            {
                rbAdmin.Checked = false;
            }
            else if (rbVisitor.Checked)
            {
                rbVisitor.Checked = false;
            }
            mtbEmployedFrom.Text = "";
            tbUsername.Text = "";
            tbPassword.Text = "";
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Успешно!");
            bool adminChecked = rbAdmin.Checked;
            DateTime employeeFrom = Convert.ToDateTime(mtbEmployedFrom.Text);
            
            

            //String[] words = mtbEmployedFrom.Text.ToString().Split('-');
            //int den = Convert.ToInt32(words[0]);
            //int mesec = Convert.ToInt32(words[1]);
            //int godina = Convert.ToInt32(words[2]);
            //DateTime datum = new DateTime(godina, mesec, den);
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            //MessageBox.Show("Воспоставена конекција");

            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "[dbo].[InsertUpdateEmployee]";

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
                    objCommand.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                    objCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = tbName.Text;
                    objCommand.Parameters.Add("@Nickname", SqlDbType.NVarChar, 50).Value = tbNickname.Text;
                    objCommand.Parameters.Add("@Surname", SqlDbType.NVarChar, 50).Value = tbSurname.Text;
                    objCommand.Parameters.Add("@YearOfBirth", SqlDbType.VarChar, 20).Value = tbYearOfBirth.Text;
                    objCommand.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = tbCity.Text;
                    objCommand.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 50).Value = mtbPhoneNumber.Text;
                    if (adminChecked)
                    {
                        objCommand.Parameters.Add("@RoleId", SqlDbType.SmallInt).Value = 1;
                    }
                    else
                    {
                        objCommand.Parameters.Add("@RoleId", SqlDbType.SmallInt).Value = 2;
                    }
                    objCommand.Parameters.Add("@EmployedFrom", SqlDbType.Date).Value = employeeFrom;
                    objCommand.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = tbUsername.Text;
                    objCommand.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = tbPassword.Text;
                }
                objCommand.ExecuteNonQuery();
                MessageBox.Show("Успешно внесен вработен!");
                id = null;
                name = "";
                surname = "";
                yearOfBirth = "";
                city = "";
                phoneNumber = "";
                employedFrom = "";
                clearTextBoxes();

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
