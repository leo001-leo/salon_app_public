using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SalonApp
{
    public partial class AddNewAppointmentPage2 : Form
    {
        public AddNewAppointmentPage2()
        {
            InitializeComponent();
        }
        

        
    public static bool opened = true;
        public static String employee="";
        public static String customer = "";
        public static bool popust = false;
        public static DateTime time;
        private void AddNewAppointmentPage2_Load(object sender, EventArgs e)
        {
            this.BackColor = Form1.backColor;
            labelTitle.ForeColor = Form1.foreColor;
            lbServices.BackColor = Form1.backColor;
            cmbTime.BackColor = Form1.backColor;
            cmbTime.ForeColor = Form1.whiteColor;
            lbServices.ForeColor = Form1.whiteColor;
            tbNotes.BackColor = Form1.backColor;
            tbNotes.ForeColor = Form1.whiteColor;
            cmbServices.BackColor = Form1.backColor;
            cmbServices.ForeColor = Form1.whiteColor;
            lbEmployee.Text = employee;
            lbCustomer.Text = customer;
            if (popust == true)
            {
                lbDiscount.Text = "10";
            }
            else
            {
                lbDiscount.Text = "0";
            }
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("select c.Id, CONCAT(c.Name,' ',c.Description, ' - ', c.Price, N' ден.') as Iminja, c.Description From Category c group by c.Id,c.Name, c.Description, c.Price,c.Duration order by c.Name, c.Description, CONVERT(smallint,c.Price)", con))
                {
                    //Fill the DataTable with records from Table.
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    //Insert the Default Item to DataTable.
                    DataRow row = dt.NewRow();
                    row[0] = 0;
                    row[1] = "Одбери категорија";
                    dt.Rows.InsertAt(row, 0);

                    //Assign DataTable as DataSource.

                    cmbServices.ValueMember = "Id";
                    cmbServices.DisplayMember = "Iminja";
                    cmbServices.DataSource = dt;

                }
            }
            if(cmbServices.Text== "Одбери категорија")
            {
                lbTotalCost.Text = "0";
                lbTotalCostDiscount.Text = "0";
            }
            for (DateTime tm = time.AddHours(9); tm < time.AddHours(21); tm = tm.AddMinutes(30))
            {
                cmbTime.Items.Add(tm.ToShortTimeString());
            }
            label12.Text = time.ToShortDateString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            opened = false;
        }
        public static int customerId;
        public static int employeeId;
        public static string phoneNumber;

        public static void sendSms(string phoneNumber, DateTime date, string time, bool fromEdit, bool isDelete)
        {
            try
            {
                WebClient client = new WebClient();
                string to, message="";
                to = phoneNumber;
                if (!fromEdit && !isDelete)
                {
                    //message = "Вашиот термин во фризерскиот салон \"Fesh\" е закажан на: " + date.ToString("dd/MM/yyyy") + " во: " + time + " часот!\nВе очекуваме!";
                    message = "Вашиот термин во стоматолошката ординација \"Продента\" е закажан на: " + date.ToString("dd/MM/yyyy") + " во: " + time + " часот!\nВе очекуваме!";
                }
                else if(fromEdit && !isDelete)
                {
                    message = "Вашиот термин во стоматолошката ординација \"Продента\" е променет!\nНовиот термин е на: " + date.ToString("dd/MM/yyyy") + " во: " + time + " часот!\nВе очекуваме!";
                }
                else if(!fromEdit && isDelete)
                {
                    message = "Вашиот термин во стоматолошката ординација \"Продента\" е откажан!\nИмајте убав ден!";
                }
                //string baseURL = "http://api.clickatell.com/http/sendmsg?user=zisan94268&password=OYeNLVUHTNIHbD&api_id=3528011&to='" + to + "'&text='" + message + "'";
                string baseURL = "https://platform.clickatell.com/messages/http/send?apiKey=kHvtoNAfQYOLI7CNp8LAfg==&to='" + to + "'&content=" + message + "";
                client.OpenRead(baseURL);
                //MessageBox.Show("Successfully sent message");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        public static void sendSmsTwillio(string phoneNumber, DateTime date, string time, bool fromEdit, bool isDelete)
        {
            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
            Twilio.Types.PhoneNumber phoneNumberTitleName = "Info Termin";
            string poraka = "";

            TwilioClient.Init(accountSid, authToken);

            if (!fromEdit && !isDelete)
            {
                //message = "Вашиот термин во фризерскиот салон \"Fesh\" е закажан на: " + date.ToString("dd/MM/yyyy") + " во: " + time + " часот!\nВе очекуваме!";
                poraka = "Вашиот термин во стоматолошката ординација \"Продента\" е закажан на: " + date.ToString("dd/MM/yyyy") + " во: " + time + " часот!\nВе очекуваме!";
            }
            else if (fromEdit && !isDelete)
            {
                poraka = "Вашиот термин во стоматолошката ординација \"Продента\" е променет!\nНовиот термин е на: " + date.ToString("dd/MM/yyyy") + " во: " + time + " часот!\nВе очекуваме!";
            }
            else if (!fromEdit && isDelete)
            {
                poraka = "Вашиот термин во стоматолошката ординација \"Продента\" на: " + date.ToString("dd/MM/yyyy") + " во: " + time + " часот е откажан!\nИмајте убав ден!";
            }

            var message = MessageResource.Create(
                body: poraka,
                from: phoneNumberTitleName,
                to: phoneNumber
            );

            //MessageBox.Show("Success");
        }

        private void btnBookAppointment_Click(object sender, EventArgs e)
        {
            if (lbServices.Items.Count > 0 && cmbTime.Text != "-- Одбери време --")
            {
                String startTimeString = cmbTime.SelectedItem.ToString();
                TimeSpan startTime = TimeSpan.Parse(startTimeString);
                String finishTimeString = lbTimeFinishes.Text;
                TimeSpan finishTime = TimeSpan.Parse(finishTimeString);
                DateTime startDate = DateTime.Parse(label12.Text);
                DateTime startDateTime = startDate.Add(startTime);
                DateTime finishDateTime = startDate.Add(finishTime);
                phoneNumber=Regex.Replace(phoneNumber, "[^0-9]", "");
                phoneNumber = "+389" + phoneNumber.Substring(1);
                
                //String[] temporary = lbTimeRequired.Text.Split(':');
                //int Chas = Convert.ToInt32(temporary[0]);
                //int Minuti = Convert.ToInt32(temporary[1]);
                //int total = Chas * 60 + Minuti;
                String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                //MessageBox.Show("Воспоставена конекција");

                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "[dbo].[InsertUpdateAppointment]";

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

                        objCommand.Parameters.Add("@Id", SqlDbType.Int).Value = -1;
                        objCommand.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = startDateTime;
                        objCommand.Parameters.Add("@FinishTime", SqlDbType.DateTime).Value = finishDateTime;
                        objCommand.Parameters.Add("@TotalPrice", SqlDbType.VarChar, 10).Value = lbTotalCostDiscount.Text;
                        objCommand.Parameters.Add("@TotalTime", SqlDbType.VarChar, 10).Value = lbTimeRequired.Text;
                        objCommand.Parameters.Add("@CustomerId", SqlDbType.SmallInt).Value = customerId;
                        objCommand.Parameters.Add("@CategoryIds", SqlDbType.VarChar, 50).Value = listOfServices;
                        objCommand.Parameters.Add("@EmployeeId", SqlDbType.SmallInt).Value = employeeId;
                        objCommand.Parameters.Add("@ColourRatioIds", SqlDbType.VarChar, 50).Value = "";
                        objCommand.Parameters.Add("@EmployeeAddId", SqlDbType.SmallInt).Value = employeeId;
                        objCommand.Parameters.Add("@EmployeeEditId", SqlDbType.SmallInt).Value = employeeId;
                        objCommand.Parameters.Add("@Notes", SqlDbType.NVarChar, 250).Value = tbNotes.Text;
                    }
                    AddNewAppointmentSuccessPage add = new AddNewAppointmentSuccessPage();
                    add.lbEmployee.Text = lbEmployee.Text;
                    add.lbCustomer.Text = lbCustomer.Text;
                    foreach (var item in lbServices.Items)
                    {
                        add.lbServices.Items.Add(item.ToString());
                    }

                    objCommand.ExecuteNonQuery();
                    sendSmsTwillio(phoneNumber, startDate, startTimeString, false, false);
                    MessageBox.Show("Успешно внесен термин!");
                    //sendSms(phoneNumber, startDate, startTimeString, false, false);
                    
                    add.lbTime.Text = cmbTime.SelectedItem.ToString();
                    add.ShowDialog();

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
                MessageBox.Show("Задолжително е да внесете услуга и време!");
            }
            
        }
        public int cost;
        public int casovi = 0;
        public int minutiTotal = 0;
        public String listOfServices="";
        List<DateTime> slotAvailability = new List<DateTime>();
        List<string> slotAvailabilityString = new List<string>();
        List<string> availabilityStatus = new List<string>();
        List<string> displayList = new List<string>();
        List<string> categoryList = new List<string>();

        String word;

        private void calculateTimeAndPrice(string constr, string id, bool isFromComboBox, bool isDeletion = false, ServiceItem item=null)
        {
            Cursor = Cursors.WaitCursor;
            string query = "SELECT COUNT(*) FROM Category WHERE Id = @ID";
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                int totalCost;
                string duration = "";
                string price = "";
                if (!string.IsNullOrEmpty(id))
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@ID", Convert.ToInt32(id));

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            // ID exists in the table
                            Console.WriteLine("ID exists");
                            // Calculate price and duration when id is not null
                            SqlCommand cmd = new SqlCommand("SELECT Price, Duration FROM Category WHERE Id = '" + Convert.ToInt16(id) + "'", con);
                            cmd.ExecuteNonQuery();

                            DataTable dt = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt);

                            price = "";
                            duration = "";

                            foreach (DataRow dr in dt.Rows)
                            {
                                price = dr["Price"].ToString();
                            }
                            foreach (DataRow dr in dt.Rows)
                            {
                                duration = dr["Duration"].ToString();
                            }

                            int cena = Convert.ToInt32(price);
                            if (isDeletion)
                            {
                                cost -= cena;
                            }
                            else
                            {
                                cost += cena;
                            }

                            lbTotalCost.Text = cost.ToString();
                            int discount = Convert.ToInt32(lbDiscount.Text);
                            totalCost = cost - (cost * discount / 100);
                            lbTotalCostDiscount.Text = totalCost.ToString();
                            String[] words = duration.Split(':');
                            int cas = Convert.ToInt32(words[0]);
                            int minuti = Convert.ToInt32(words[1]);
                            if (isDeletion)
                            {
                                casovi -= cas;
                                minutiTotal -= minuti;
                            }
                            else
                            {
                                casovi += cas;
                                minutiTotal += minuti;
                            }
                            string temp = "";

                            if (minutiTotal.ToString().Length == 1)
                            {
                                temp = casovi + ":" + "0" + minutiTotal;
                                lbTimeRequired.Text = temp;
                            }
                            else
                            {
                                if (minutiTotal >= 60)
                                {
                                    casovi += 1;
                                    minutiTotal = minutiTotal - 60;

                                    if (minutiTotal.ToString().Length == 1)
                                    {
                                        temp = casovi + ":" + "0" + minutiTotal;
                                        lbTimeRequired.Text = temp;
                                    }
                                    else
                                    {
                                        lbTimeRequired.Text = casovi + ":" + minutiTotal;
                                    }
                                }
                                else
                                {
                                    lbTimeRequired.Text = casovi + ":" + minutiTotal;
                                }
                            }
                        }
                        else
                        {
                            // ID does not exist in the table
                            MessageBox.Show("Категоријата повеќе не постои или е избришана");
                            this.Close();

                        }
                    }

                }

                else
                {
                    // Calculate total cost with discount when id is null
                    int discount = Convert.ToInt32(lbDiscount.Text);
                    totalCost = cost - (cost * discount / 100);
                    lbTotalCostDiscount.Text = totalCost.ToString();
                }
                if (isFromComboBox)
                {
                    lbServices.Items.Add(item);
                }

                con.Close();
            }
            Cursor = Cursors.Default;
        }

        private void calculateShowTimeComboBox(string connectionString)
        {
            cmbTime.Items.Clear();
            displayList.Clear();
            slotAvailability.Clear();
            slotAvailabilityString.Clear();
            availabilityStatus.Clear();
            categoryList.Clear();
            SqlConnection con = new SqlConnection(connectionString);
            String[] temporary = lbTimeRequired.Text.Split(':');
            int Chas = Convert.ToInt32(temporary[0]);
            int Minuti = Convert.ToInt32(temporary[1]);
            int total = Chas * 60 + Minuti;

            using (SqlConnection connection = con)
            {
                connection.Open();
                
                    string query = "Select Timeslot from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp, @AppointmentEditId) ORDER by TimeSlot";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", employeeId);
                        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                        command.Parameters.AddWithValue("@AppointmentEditId", -1);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                slotAvailability.Add(reader.GetDateTime(0));
                            }
                        }
                    }
                    string query2 = "Select AvailabilityId from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp, @AppointmentEditId) ORDER by TimeSlot";
                    using (SqlCommand command = new SqlCommand(query2, connection))
                    {
                        command.Parameters.AddWithValue("@Id", employeeId);
                        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                        command.Parameters.AddWithValue("@AppointmentEditId", -1);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                availabilityStatus.Add(reader.GetString(0));
                            }
                        }
                    }

                    string query3 = "Select CategoryIds from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp, @AppointmentEditId) ORDER by TimeSlot";
                    using (SqlCommand command = new SqlCommand(query3, connection))
                    {
                        command.Parameters.AddWithValue("@Id", employeeId);
                        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                        command.Parameters.AddWithValue("@AppointmentEditId", -1);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                categoryList.Add(reader.GetString(0));
                            }
                        }
                    }
            }




            List<string> list = new List<string>();
            foreach (var d in slotAvailability.Select((value, i) => (value, i)))
            {
                slotAvailabilityString.Add(d.value.ToString("yyyy/MM/dd HH:mm " + availabilityStatus[d.i] + " " + categoryList[d.i])); //polnenje na listata
                                                                                                                                       //so datum,vreme i status na termin(booked/free)
                                                                                                                                       //                          .startsWith("Booke")/ree
            }
            int count = 0;
            
            foreach (var d in slotAvailability.Select((value, i) => (value, i)))
            {
                count = d.i;
                //String[] zborovi;
                //zborovi = slotAvailabilityString[d.i].ToString().Split(' ');
                //String datumVreme = zborovi[0] + " " + zborovi[1];
                //DateTime dateTimeComp = DateTime.Parse(datumVreme);
                //String word = zborovi[2];
                //dateTime = DateTime.Parse(d.value.ToString("yyyy/MM/dd HH:mm"));
                //timeSpan = TimeSpan.Parse(lbTimeRequired.Text);
                //DateTime temp1 = dateTime.AddMinutes(total);
                //for (DateTime i = availableTimes[0]; i <= availableTimes.Count; i=i.AddMinutes(30))
                //{
                //    if (word=="ree")
                //    {
                //        displayList.Add(d.value.ToString("yyyy/MM/dd HH:mm"));
                //    }
                //}
                //String temp12 = zborovi[0];
                //String temp13 = availableTimes[0].ToString("yyyy/MM/dd");


                String[] zborovi2;
                zborovi2 = slotAvailabilityString[d.i].ToString().Split(' ');
                String datumVreme2 = zborovi2[0] + " " + zborovi2[1];
                DateTime dateTimeComp2 = DateTime.Parse(datumVreme2);
                String word2 = zborovi2[2];
                String categories = zborovi2[3];
                DateTime privremena = d.value.AddMinutes(total);

                bool free = true;
                String[] tempZborovi;
                String tempWord2, tempWord2Datum;


                for (DateTime j = d.value; j < privremena && count < slotAvailability.Count-1; j = slotAvailability[count])
                {
                    if (count <= slotAvailabilityString.Count - 1)
                    {
                        zborovi2 = slotAvailabilityString[count].ToString().Split(' ');
                    }
                    word2 = zborovi2[2];
                    categories = zborovi2[3];
                    //if (j == dateTimeComp2)
                   // {
                        //if (word2.Contains("Booke") && (!categories.Contains("3,") || !categories.Contains(",3,")) &&
                        //                               (!categories.Contains("4,") || !categories.Contains(",4,")) &&
                        //                               (!categories.Contains("5,") || !categories.Contains(",5,")) &&
                        //                               (!categories.Contains("6,") || !categories.Contains(",6,")) &&
                        //                               (!categories.Contains("7,") || !categories.Contains(",7,")) &&
                        //                               (!categories.Contains("8,") || !categories.Contains(",8,")) &&
                        //                               !categories.Contains("9") && !categories.Contains("10") &&
                        //                               !categories.Contains("11") && !categories.Contains("12") &&
                        //                               !categories.Contains("13") && !categories.Contains("14") &&
                        //                               !categories.Contains("15") && !categories.Contains("16") &&
                        //                               !categories.Contains("17"))
                        if(word2.Contains("Booke"))
                        {
                            //found = true;
                            if (count + 1 <= slotAvailabilityString.Count - 1)
                            {
                                tempZborovi = slotAvailabilityString[count + 1].ToString().Split(' ');
                                tempWord2Datum = tempZborovi[0] + " " + tempZborovi[1];
                                DateTime dateTimeTempWord2Datum = DateTime.Parse(tempWord2Datum);
                                tempWord2 = tempZborovi[2];

                                if (tempWord2 != "ree")
                                {
                                    free = false;
                                }
                                else if (j == dateTimeTempWord2Datum)
                                {
                                    free = false;
                                }

                            }
                        }

                        //dateTimeComp2 = dateTimeComp2.AddMinutes(30);
                        if (count != slotAvailabilityString.Count)
                        {
                            count++;
                        }
                    //}
                }
                if (free == true)
                { 
                    displayList.Add(d.value.ToString("HH:mm"));
                    displayList = displayList.Distinct().ToList();
                }
            }
            for (int i = 0; i < displayList.Count; i++)
            {
                cmbTime.Items.Add(displayList[i]);
            }



        }

        private void cmbServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbServices.Text!= "Одбери категорија")
            {
                cmbTime.Items.Clear();
                displayList.Clear();
                slotAvailabilityString.Clear();
                slotAvailability.Clear();
                int id = Convert.ToInt32(cmbServices.SelectedValue);
                listOfServices += id + ",";
                string constr = ConfigurationManager.AppSettings["ConnectionString"];
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                EditAppointment edit = new EditAppointment();
                ServiceItem item = new ServiceItem
                {
                    Name = edit.getServiceName(id.ToString()),
                    ID = id
                };
                calculateTimeAndPrice(constr, id.ToString(), true, false, item);
                calculateShowTimeComboBox(constr);
                
                
                //String[] temporary = lbTimeRequired.Text.Split(':');
                //int Chas = Convert.ToInt32(temporary[0]);
                //int Minuti = Convert.ToInt32(temporary[1]);
                //int total = Chas * 60 + Minuti;


                //using (SqlConnection connection = con)
                //{
                //    //connection.Open();
                //    string query = "Select Timeslot from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp, @AppointmentEditId) ORDER by TimeSlot";
                //    using (SqlCommand command = new SqlCommand(query, connection))
                //    {
                //        command.Parameters.AddWithValue("@Id",employeeId);
                //        command.Parameters.AddWithValue("@DateApp",DateTime.Parse(label12.Text));
                //        command.Parameters.AddWithValue("@AppointmentEditId", -1);
                //        using (SqlDataReader reader = command.ExecuteReader())
                //        {
                //            while (reader.Read())
                //            {
                //                slotAvailability.Add(reader.GetDateTime(0));
                //            }
                //        }
                //    }
                //    string query2 = "Select AvailabilityId from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp, @AppointmentEditId) ORDER by TimeSlot";
                //    using (SqlCommand command = new SqlCommand(query2, connection))
                //    {
                //        command.Parameters.AddWithValue("@Id", employeeId);
                //        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                //        command.Parameters.AddWithValue("@AppointmentEditId", -1);
                //        using (SqlDataReader reader = command.ExecuteReader())
                //        {
                //            while (reader.Read())
                //            {
                //                availabilityStatus.Add(reader.GetString(0));
                //            }
                //        }
                //    }

                //    string query3 = "Select CategoryIds from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp, @AppointmentEditId) ORDER by TimeSlot";
                //    using (SqlCommand command = new SqlCommand(query3, connection))
                //    {
                //        command.Parameters.AddWithValue("@Id", employeeId);
                //        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                //        command.Parameters.AddWithValue("@AppointmentEditId", -1);
                //        using (SqlDataReader reader = command.ExecuteReader())
                //        {
                //            while (reader.Read())
                //            {
                //                categoryList.Add(reader.GetString(0));
                //            }
                //        }
                //    }
                //    //string query3 = "Select Timeslot, [availability] from SlotAvailability where [availability] = 'free' and CONVERT(DATE,TimeSlot)=@DateApp ORDER by TimeSlot";
                //    //using (SqlCommand command = new SqlCommand(query3, connection))
                //    //{
                //    //    command.Parameters.Add("@DateApp", SqlDbType.Date).Value = DateTime.Parse(label12.Text);
                //    //    using (SqlDataReader reader = command.ExecuteReader())
                //    //    {
                //    //        while (reader.Read())
                //    //        {
                //    //            availableTimes.Add(reader.GetDateTime(0));
                //    //        }
                //    //    }
                //    //}
                //}




                //List<string> list = new List<string>();
                //foreach (var d in slotAvailability.Select((value, i) => (value, i)))
                //{
                //    slotAvailabilityString.Add(d.value.ToString("yyyy/MM/dd HH:mm " + availabilityStatus[d.i] + " " + categoryList[d.i])); //polnenje na listata
                //                                                                                                       //so datum,vreme i status na termin(booked/free)
                //                                                                                                       //                          .startsWith("Booke")/ree
                //}
                //int count = 0;
                //foreach (var d in slotAvailability.Select((value, i) => (value, i)))
                //{
                //    count = d.i;
                //    //String[] zborovi;
                //    //zborovi = slotAvailabilityString[d.i].ToString().Split(' ');
                //    //String datumVreme = zborovi[0] + " " + zborovi[1];
                //    //DateTime dateTimeComp = DateTime.Parse(datumVreme);
                //    //String word = zborovi[2];
                //    //dateTime = DateTime.Parse(d.value.ToString("yyyy/MM/dd HH:mm"));
                //    //timeSpan = TimeSpan.Parse(lbTimeRequired.Text);
                //    //DateTime temp1 = dateTime.AddMinutes(total);
                //    //for (DateTime i = availableTimes[0]; i <= availableTimes.Count; i=i.AddMinutes(30))
                //    //{
                //    //    if (word=="ree")
                //    //    {
                //    //        displayList.Add(d.value.ToString("yyyy/MM/dd HH:mm"));
                //    //    }
                //    //}
                //    //String temp12 = zborovi[0];
                //    //String temp13 = availableTimes[0].ToString("yyyy/MM/dd");


                //    String[] zborovi2;
                //    zborovi2 = slotAvailabilityString[d.i].ToString().Split(' ');
                //    String datumVreme2 = zborovi2[0] + " " + zborovi2[1];
                //    DateTime dateTimeComp2 = DateTime.Parse(datumVreme2);
                //    String word2 = zborovi2[2];
                //    String categories = zborovi2[3];
                //    DateTime privremena = d.value.AddMinutes(total);

                //    bool free = true;
                //    String[] tempZborovi;
                //    String tempWord2;


                //    for (DateTime j = d.value; j < privremena; j = j.AddMinutes(30))
                //    {
                //        if (count <= slotAvailabilityString.Count - 1)
                //        {
                //            zborovi2 = slotAvailabilityString[count].ToString().Split(' ');
                //        }
                //        word2 = zborovi2[2];

                //        if (j == dateTimeComp2)
                //        {
                //                if (word2.Contains("Booke") && !categories.Contains("10") && !categories.Contains("15"))
                //                {
                //                    //found = true;
                //                    if (count + 1 <= slotAvailabilityString.Count - 1)
                //                    {
                //                        tempZborovi = slotAvailabilityString[count + 1].ToString().Split(' ');

                //                        tempWord2 = tempZborovi[2];

                //                        if (tempWord2 != "ree")
                //                        {
                //                            free = false;
                //                        }

                //                    }
                //                }



                //            dateTimeComp2 = dateTimeComp2.AddMinutes(30);
                //            if (count != slotAvailabilityString.Count)
                //            {
                //                count++;
                //            }
                //        }
                //    }
                //    if (free==true)
                //    {

                //        displayList.Add(d.value.ToString("yyyy/MM/dd HH:mm"));

                //    }



                //}
                //for(int i = 0; i < displayList.Count; i++)
                //{
                //    cmbTime.Items.Add(displayList[i]);
                //}





            }

        }
       
        private void cmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dateTime = Convert.ToDateTime(cmbTime.Text);
            int cas = Convert.ToInt32(dateTime.Hour);
            int min = Convert.ToInt32(dateTime.Minute);
            //String vreme = dateTime.ToString("HH:mm");
            DateTime dateTime1 = Convert.ToDateTime(lbTimeRequired.Text);
            int cas1= Convert.ToInt32(dateTime1.Hour);
            int min1 = Convert.ToInt32(dateTime1.Minute);
            //String vreme1 = dateTime.ToString("HH:mm");
            //dateTime.Add(new TimeSpan(dateTime1.Hour,dateTime1.Minute,dateTime1.Second));
            int totCas = cas + cas1;
            int totMin = min + min1;
            if (totMin >= 60)
            {
                totCas += 1;
                totMin = totMin - 60;
            }
            if (totMin == 0)
            {
                lbTimeFinishes.Text = totCas + ":" + totMin+"0";
            }
            else
            {
                lbTimeFinishes.Text = totCas + ":" + totMin;
            }
            
        }
        public class ServiceItem
        {
            public int ID { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name; // Return the custom name for the ListBoxItem
            }
        }
        private void lbServices_KeyDown(object sender, KeyEventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            if (e.KeyCode == Keys.Delete)
            {
                if (lbServices.SelectedItem != null)
                {
                    DialogResult result = MessageBox.Show("Дали сте сигурни дека сакате да ја избришете услугата?", "Бришење услуга", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        
                        ServiceItem selectedItem = (ServiceItem)lbServices.SelectedItem;

                        // Retrieve the ID from the Tag property of the selected item
                        int id = (int)selectedItem.ID;

                        // Perform the necessary actions with the ID (e.g., delete from database)

                        // Remove the selected item from the ListBox
                        lbServices.Items.Remove(selectedItem);

                        //int id = Convert.ToInt32(lbServices.SelectedValue);
                        //lbServices.Items.Remove(lbServices.SelectedItem);
                        calculateTimeAndPrice(connectionString, id.ToString(), false, true);

                        // Convert the string to an array of numbers
                        string[] numbersArray = listOfServices.Split(',');
                        List<string> modifiedIdsList = new List<string>(numbersArray);

                        modifiedIdsList.RemoveAll(string.IsNullOrEmpty);
                        numbersArray = modifiedIdsList.ToArray();
                        //numbersArray = numbersArray.Take(numbersArray.Length - 1).ToArray();

                        // Create a list to store the numbers
                        List<int> numbersList = new List<int>();

                        // Convert the array of strings to a list of integers
                        foreach (string number in numbersArray)
                        {
                            numbersList.Add(int.Parse(number));
                        }

                        // Find the index of the number to remove
                        int indexToRemove = numbersList.FindIndex(n => n == id);

                        // Remove the number from the list
                        if (indexToRemove != -1)
                        {
                            numbersList.RemoveAt(indexToRemove);
                        }
                        listOfServices = string.Join(",", numbersList);
                        calculateShowTimeComboBox(connectionString);
                    }
                }
            }
        }

        private void cmbServices_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbServices.SelectedIndexChanged -= cmbServices_SelectedIndexChanged;
            cmbServices.DroppedDown = true;
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            string str = cmbServices.Text.Substring(0, cmbServices.SelectionStart) + e.KeyChar;
            Int32 index = cmbServices.FindStringExact(str);
            if (index == -1)
            {
                index = cmbServices.FindString(str);
            }
            this.cmbServices.SelectedIndex = index;
            this.cmbServices.SelectionStart = str.Length;
            this.cmbServices.SelectionLength = this.cmbServices.Text.Length - this.cmbServices.SelectionStart;
            e.Handled = true;
            cmbServices.SelectedIndexChanged += cmbServices_SelectedIndexChanged;
        }

        private void cmbServices_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbServices_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                // Allow the ComboBox to process arrow keys without changing the selection
                e.IsInputKey = true;
            }
        }
    }
}
