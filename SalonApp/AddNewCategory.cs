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
    public partial class AddNewCategory : Form
    {
        public AddNewCategory()
        {
            InitializeComponent();
        }

        public static string id, name, description, price, durationVariable;
        public int Id = -1;
        public static bool FromEdit = false;

        private void tbPrice_TextChanged(object sender, EventArgs e)
        {
            if (tbPrice.Text != "")
            {
                if (!int.TryParse(tbPrice.Text, out _))
                {
                    // Invalid input, clear the textbox or show an error message
                    tbPrice.Text = string.Empty; // Clear the textbox
                                                 // Or show an error message, e.g., MessageBox.Show("Please enter a valid number");
                    MessageBox.Show("Букви не се дозволени!\nВнесете само бројки!");
                }
            }
        }

        public static bool FromBlank = true;

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Успешно!");
            if (tbName.Text != "" && tbPrice.Text != "" && tbDuration.Text != " :")
            {
                String duration = tbDuration.Text;
                String[] durat = duration.Split(':');
                String cas = durat[0];
                String minuti = durat[1];
                if (minuti.Length == 1)
                {
                    duration = cas + ":" + "0" + minuti;
                }
                if (minuti == "")
                {
                    duration = cas + ":" + "00";
                }
                if (cas == " ")
                {
                    duration = "0" + ":" + minuti;
                }
                String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                //MessageBox.Show("Воспоставена конекција");

                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "[dbo].[InsertUpdateCategory]";

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
                        objCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 50).Value = tbDescription.Text;
                        objCommand.Parameters.Add("@Price", SqlDbType.VarChar, 50).Value = tbPrice.Text;
                        objCommand.Parameters.Add("@Duration", SqlDbType.VarChar, 50).Value = duration;
                    }
                    objCommand.ExecuteNonQuery();
                    MessageBox.Show("Успешно внесена категорија!");
                    id = null;
                    name = "";
                    description = "";
                    price = "";
                    durationVariable = "";
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
            else
            {
                MessageBox.Show("Сите полиња се задолжителни!");
            }
        }

        private void clearTextBoxes()
        {
            tbName.Text = "";
            tbDescription.Text = "";
            tbPrice.Text = "";
            tbDuration.Text = "";
        }

        private void AddNewCategory_Load(object sender, EventArgs e)
        {
            this.BackColor = Form1.backColor;
            lbTitle.ForeColor = Form1.foreColor;
            tbDescription.BackColor = Form1.backColor;
            tbDuration.BackColor = Form1.backColor;
            tbName.BackColor = Form1.backColor;
            tbPrice.BackColor = Form1.backColor;
            clearTextBoxes();
            if (FromEdit == true && FromBlank == false)
            {
                Id = Convert.ToInt32(id);
                tbName.Text = name;
                tbName.SelectionLength = 0;
                tbName.SelectionStart = tbName.Text.Length;
                tbDescription.Text = description;
                tbPrice.Text = price;
                tbDuration.Text = durationVariable;
            }
        }
    }
}
