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
    public partial class AddNewCustomer : Form
    {
        public AddNewCustomer()
        {
            InitializeComponent();
        }
        public static String id, name, nickname, surname, city, phoneNumber, customerFrom, notes;
        public int Id = -1;
        public static bool FromEdit = false;
        public static bool FromBlank = true;
        private void AddNewCustomer_Load(object sender, EventArgs e)
        {
            this.BackColor = Form1.backColor;
            lbTitle.ForeColor = Form1.foreColor;
            tbName.BackColor = Form1.backColor;
            tbNickname.BackColor = Form1.backColor;
            tbSurname.BackColor = Form1.backColor;
            tbCity.BackColor = Form1.backColor;
            mtbPhoneNumber.BackColor = Form1.backColor;
            mtbClientFrom.BackColor = Form1.backColor;
            tbNotes.BackColor = Form1.backColor;
            if (FromEdit == true && FromBlank == false)
            {
                Id = Convert.ToInt32(id);
                tbName.Text = name;
                tbName.SelectionLength = 0;
                tbName.SelectionStart = tbName.Text.Length;
                tbNickname.Text = nickname;
                tbSurname.Text = surname;
                tbCity.Text = city;
                mtbPhoneNumber.Text = phoneNumber;
                mtbClientFrom.Text = customerFrom;
                tbNotes.Text = notes;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DateTime clientFrom = Convert.ToDateTime(mtbClientFrom.Text);
            //String[] words = mtbClientFrom.Text.ToString().Split('-');
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
            objCommand.CommandText = "[dbo].[InsertUpdateCustomer]";

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

                    objCommand.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                    objCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = tbName.Text;
                    objCommand.Parameters.Add("@Nickname", SqlDbType.NVarChar, 50).Value = tbNickname.Text;
                    objCommand.Parameters.Add("@Surname", SqlDbType.NVarChar, 50).Value = tbSurname.Text;
                    objCommand.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = tbCity.Text;
                    objCommand.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 50).Value = mtbPhoneNumber.Text;
                    objCommand.Parameters.Add("@CustomerFrom", SqlDbType.Date).Value = clientFrom;
                    objCommand.Parameters.Add("@Notes", SqlDbType.NVarChar, 100).Value = tbNotes.Text;
                }
                objCommand.ExecuteNonQuery();
                MessageBox.Show("Успешно внесен клиент!");
                id = null;
                name = "";
                nickname = "";
                surname = "";
                city = "";
                phoneNumber = "";
                customerFrom = "";
                notes = "";
                tbName.Text = "";
                tbNickname.Text = "";
                tbSurname.Text = "";
                tbCity.Text = "";
                mtbPhoneNumber.Text = "07";
                mtbClientFrom.Text = "";
                tbNotes.Text = "";

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
