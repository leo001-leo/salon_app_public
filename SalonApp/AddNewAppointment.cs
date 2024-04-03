using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SalonApp
{
    public partial class AddNewAppointment : Form
    {

        public AddNewAppointment()
        {
            InitializeComponent();
            

        }
        
        private void AddNewAppointment_Load(object sender, EventArgs e)
        {
            this.BackColor = Form1.backColor;
            labelTitle.ForeColor = Form1.foreColor;
            cmbEmployee.BackColor = Form1.backColor;
            cmbCustomer.BackColor = Form1.backColor;
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Id, Name + ' ' + Surname as Iminja FROM Employee WHERE Status='A' ORDER BY Iminja", con))
                {
                    //Fill the DataTable with records from Table.
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    //Insert the Default Item to DataTable.
                    DataRow row = dt.NewRow();
                    row[0] = 0;
                    row[1] = "Одбери вработен";
                    dt.Rows.InsertAt(row, 0);

                    //Assign DataTable as DataSource.

                    cmbEmployee.ValueMember = "Id";
                    cmbEmployee.DisplayMember = "Iminja";
                    cmbEmployee.DataSource = dt;
                    

                }
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Id, Name + ' ' + Surname as Iminja FROM Customer WHERE Status='A' ORDER BY Iminja", con))
                {
                    //Fill the DataTable with records from Table.
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    //Insert the Default Item to DataTable.
                    DataRow row = dt.NewRow();
                    row[0] = 0;
                    row[1] = "Одбери клиент";
                    dt.Rows.InsertAt(row, 0);

                    //Assign DataTable as DataSource.

                    cmbCustomer.ValueMember = "Id";
                    cmbCustomer.DisplayMember = "Iminja";
                    cmbCustomer.DataSource = dt;
                   
                    

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (cmbEmployee.Text != "Одбери вработен" &&  !String.IsNullOrWhiteSpace(cmbEmployee.Text) && cmbCustomer.Text != "Одбери клиент" && !String.IsNullOrWhiteSpace(cmbCustomer.Text))
            {
                int id = Convert.ToInt32(cmbCustomer.SelectedValue);
                int empId = Convert.ToInt32(cmbEmployee.SelectedValue);
                AddNewAppointmentPage2.customerId = id;
                AddNewAppointmentPage2.employeeId = empId;
                string constr = ConfigurationManager.AppSettings["ConnectionString"];
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand objCommand = new SqlCommand();
                SqlCommand cmd = new SqlCommand("select PhoneNumber from Customer where Id='" + id + "'", con);
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                String phoneNumber = "";
                foreach (DataRow dr in dt.Rows)
                {
                    phoneNumber = dr["PhoneNumber"].ToString();
                }
                AddNewAppointmentPage2.phoneNumber = phoneNumber;
                //int freq = Convert.ToInt32(frequency);
                //if (freq > 30)
                //{
                //    if (DialogResult.Yes == MessageBox.Show("Лојален клиент!\nДали сакате да додадете дополнителен попуст од 10%?", "Дополнителен попуст", MessageBoxButtons.YesNo))
                //    {
                //        AddNewAppointmentPage2.popust = true;
                //    }
                //}
                //else
                //{
                //    AddNewAppointmentPage2.popust = false;
                //}
                EditAppointment edit = new EditAppointment();
                if (edit.IsCustomerLoyal(Convert.ToInt32(cmbCustomer.SelectedValue)))
                {
                    DialogResult result = MessageBox.Show("Лојален клиент!\nБројот на посети е поголем од 15!\nДали одобрувате 10% попуст?", "Попуст", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        AddNewAppointmentPage2.popust = true;
                    }
                    else
                    {
                        AddNewAppointmentPage2.popust = false;
                    }
                }
                else
                {
                    AddNewAppointmentPage2.popust = false;
                }
                AddNewAppointmentPage2 add = new AddNewAppointmentPage2();
                add.ShowDialog();
            }
            else
            {
                MessageBox.Show("Задолжително е одбирање на вработен и клиент!");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewCustomer add = new AddNewCustomer();
            add.ShowDialog();
            
        }

        private void pbRefresh_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection con = new SqlConnection(constr))
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Id, Name + ' ' + Surname as Iminja FROM Customer WHERE Status='A' ORDER BY Iminja", con))
            {
                //Fill the DataTable with records from Table.
                DataTable dt = new DataTable();
                sda.Fill(dt);

                //Insert the Default Item to DataTable.
                DataRow row = dt.NewRow();
                row[0] = 0;
                row[1] = "Одбери клиент";
                dt.Rows.InsertAt(row, 0);

                //Assign DataTable as DataSource.

                cmbCustomer.ValueMember = "Id";
                cmbCustomer.DisplayMember = "Iminja";
                cmbCustomer.DataSource = dt;
                
            }
        }

        private void cmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddNewAppointmentPage2.employee = cmbEmployee.Text;
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddNewAppointmentPage2.customer = cmbCustomer.Text;
        }

        private void cmbCustomer_TextChanged(object sender, EventArgs e)
        {
            
            

        }
        
        private void cmbCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void cmbCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            cmbCustomer.DroppedDown = true;
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            string str = cmbCustomer.Text.Substring(0, cmbCustomer.SelectionStart) + e.KeyChar;
            Int32 index = cmbCustomer.FindStringExact(str);
            if (index == -1)
            {
                index = cmbCustomer.FindString(str);
            }
            this.cmbCustomer.SelectedIndex = index;
            this.cmbCustomer.SelectionStart = str.Length;
            this.cmbCustomer.SelectionLength = this.cmbCustomer.Text.Length - this.cmbCustomer.SelectionStart;
            e.Handled = true;

        }
        
        private void cmbCustomer_TextUpdate(object sender, EventArgs e)
        {
            
            
        }
    }
}
