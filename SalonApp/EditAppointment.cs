using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace SalonApp
{
    public partial class EditAppointment : Form
    {
        public EditAppointment()
        {
            InitializeComponent();
            this.Tag = appId;
        }
        public EditAppointment(int appointmentId)
        {
            InitializeComponent();
            this.Tag = appointmentId;
        }
        public static string employee, customer, services, notes;
        public static DateTime time, hourApp;
        public int cost,year,month,day;
        public int casovi = 0;
        public int minutiTotal = 0;
        public String listOfServices = "";
        List<DateTime> slotAvailability = new List<DateTime>();
        List<string> slotAvailabilityString = new List<string>();
        List<string> availabilityStatus = new List<string>();
        List<string> displayList = new List<string>();
        List<string> categoryList = new List<string>();
        List<string> appointmentIdsList = new List<string>();
        List<Int32> appointmentIds = new List<int>();
        public static int employeeId, customerId;
        public static int appId;
       

        private void cmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            //employeeId = Convert.ToInt32(cmbEmployee.SelectedValue);
            //customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            if (cmbEmployee.Text != "Одбери вработен")
            {
                if (cmbEmployee.Text == vraboten)
                {
                    //MessageBox.Show("Ist");
                }
                else
                {
                    calculateShowTimeComboBox(constr, true);
                }
                //label12.Text = time.ToShortDateString();
                //cmbEmployee.Text = employee;
                //cmbCustomer.Text = customer;
                //customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                //employeeId = Convert.ToInt32(cmbEmployee.SelectedValue);
                //string constr = ConfigurationManager.AppSettings["ConnectionString"];
                //SqlConnection con2 = new SqlConnection(constr);
                //con2.Open();
                //SqlCommand objCommand = new SqlCommand();
                //SqlCommand cmd = new SqlCommand("select Frequency from Customer where Id = '" + customerId + "'", con2);
                //cmd.ExecuteNonQuery();
                //DataTable dt2 = new DataTable();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(dt2);
                //String frequency = "";
                //foreach (DataRow dr in dt2.Rows)
                //{
                //    frequency = dr["Frequency"].ToString();
                //}
                //int freq = Convert.ToInt32(frequency);
                //if (freq > 30)
                //{
                //    if (DialogResult.Yes == MessageBox.Show("Лојален клиент!\nДали сакате да додадете дополнителен попуст од 10%?", "Дополнителен попуст", MessageBoxButtons.YesNo))
                //    {
                //        lbDiscount.Text = "10";
                //    }
                //}
                //else
                //{
                //    lbDiscount.Text = "0";
                //}


                //String[] parts;
                //String service = "";
                //String serviceId = "";
                //DataTable dt3 = new DataTable();
                //SqlDataAdapter da3;
                //SqlCommand cmd2 = new SqlCommand();
                //String[] deloviPrazniMesta;

                //if (services.Contains(','))
                //{
                //    parts = services.Split(',');

                //    foreach (string p in parts)
                //    {
                //        deloviPrazniMesta = p.Split(' ');
                //        lbServices.Items.Add(p);
                //        if (con2.State == ConnectionState.Open)
                //        {
                //            con2.Close();
                //        }
                //        if (!deloviPrazniMesta[0].StartsWith("\r"))
                //        {
                //            string sql3 = string.Format("select Id from Category where Name = N'" + deloviPrazniMesta[0] + "' and Description=N'" + deloviPrazniMesta[1] + "'");
                //            con2.Open();
                //            SqlCommand cmd3 = new SqlCommand(sql3, con2);

                //            SqlDataReader reader3 = cmd3.ExecuteReader();
                //            while (reader3.Read())
                //            {
                //                serviceId = reader3["Id"].ToString();
                //            }
                //            if (con2.State == ConnectionState.Open)
                //            {
                //                con2.Close();
                //            }
                //        }
                //        else
                //        {
                //            deloviPrazniMesta[0] = deloviPrazniMesta[0].Substring(1);

                //            string sql3 = string.Format("select Id from Category where Name = N'" + deloviPrazniMesta[0] + "' and Description=N'" + deloviPrazniMesta[1] + "'");
                //            con2.Open();
                //            SqlCommand cmd3 = new SqlCommand(sql3, con2);

                //            SqlDataReader reader3 = cmd3.ExecuteReader();
                //            while (reader3.Read())
                //            {
                //                serviceId = reader3["Id"].ToString();
                //            }
                //            if (con2.State == ConnectionState.Open)
                //            {
                //                con2.Close();
                //            }
                //        }
                //        SqlConnection con = new SqlConnection(constr);
                //        con.Open();
                //        SqlCommand cmd1 = new SqlCommand("Select Price From Category where Id = '" + Convert.ToInt32(serviceId) + "'", con);
                //        SqlCommand cmd4 = new SqlCommand("select Duration from Category where Id = '" + Convert.ToInt32(serviceId) + "'", con);

                //        cmd1.ExecuteNonQuery();
                //        cmd4.ExecuteNonQuery();

                //        DataTable dt = new DataTable();
                //        SqlDataAdapter da4 = new SqlDataAdapter(cmd1);
                //        da4.Fill(dt);
                //        DataTable dt5 = new DataTable();
                //        SqlDataAdapter da5 = new SqlDataAdapter(cmd4);
                //        da5.Fill(dt5);

                //        String price = "";
                //        String duration = "";

                //        foreach (DataRow dr in dt.Rows)
                //        {
                //            price = dr["Price"].ToString();
                //        }
                //        foreach (DataRow dr in dt5.Rows)
                //        {
                //            duration = dr["Duration"].ToString();
                //        }

                //        int cena = Convert.ToInt32(price);
                //        cost += cena;
                //        lbTotalCost.Text = cost.ToString();
                //        int discount = Convert.ToInt32(lbDiscount.Text);
                //        int totalCost = cost - (cost * discount / 100);
                //        lbTotalCostDiscount.Text = totalCost.ToString();
                //        String[] words = duration.Split(':');
                //        int cas = Convert.ToInt32(words[0]);
                //        int minuti = Convert.ToInt32(words[1]);
                //        casovi += cas;
                //        minutiTotal += minuti;
                //        String temp = "";
                //        if (minutiTotal.ToString().Length == 1)
                //        {
                //            temp = casovi + ":" + "0" + minutiTotal;
                //            lbTimeRequired.Text = temp;
                //        }
                //        else
                //        {
                //            if (minutiTotal >= 60)
                //            {
                //                casovi += 1;
                //                minutiTotal = minutiTotal - 60;
                //                if (minutiTotal.ToString().Length == 1)
                //                {
                //                    temp = casovi + ":" + "0" + minutiTotal;
                //                    lbTimeRequired.Text = temp;
                //                }
                //                else
                //                {
                //                    lbTimeRequired.Text = casovi + ":" + minutiTotal;
                //                }
                //            }
                //            else
                //            {
                //                lbTimeRequired.Text = casovi + ":" + minutiTotal;
                //            }

                //        }

                //    }
                //}
                //else
                //{
                //    deloviPrazniMesta = services.Split(' ');
                //    //service = deloviPrazniMesta[0] + " " + deloviPrazniMesta[1];
                //    lbServices.Items.Add(services.ToString());
                //}
                //cmbTime.Items.Clear();
                //displayList.Clear();
                //slotAvailabilityString.Clear();
                //slotAvailability.Clear();
                //String[] temporary = lbTimeRequired.Text.Split(':');
                //int Chas = Convert.ToInt32(temporary[0]);
                //int Minuti = Convert.ToInt32(temporary[1]);
                //int total = Chas * 60 + Minuti;
                //if (con2.State == ConnectionState.Open)
                //{
                //    con2.Close();
                //}
                //con2.Open();
                //using (SqlConnection connection = con2)
                //{
                //    //connection.Open();
                //    string query = "Select Timeslot from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
                //    using (SqlCommand command = new SqlCommand(query, connection))
                //    {
                //        command.Parameters.AddWithValue("@Id", employeeId);
                //        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                //        using (SqlDataReader reader = command.ExecuteReader())
                //        {
                //            while (reader.Read())
                //            {
                //                slotAvailability.Add(reader.GetDateTime(0));
                //            }
                //        }
                //    }
                //    string query2 = "Select AvailabilityId from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
                //    using (SqlCommand command = new SqlCommand(query2, connection))
                //    {
                //        command.Parameters.AddWithValue("@Id", employeeId);
                //        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                //        using (SqlDataReader reader = command.ExecuteReader())
                //        {
                //            while (reader.Read())
                //            {
                //                availabilityStatus.Add(reader.GetString(0));
                //            }
                //        }
                //    }

                //    string query3 = "Select CategoryIds from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
                //    using (SqlCommand command = new SqlCommand(query3, connection))
                //    {
                //        command.Parameters.AddWithValue("@Id", employeeId);
                //        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                //        using (SqlDataReader reader = command.ExecuteReader())
                //        {
                //            while (reader.Read())
                //            {
                //                categoryList.Add(reader.GetString(0));
                //            }
                //        }
                //    }

                //    string query4 = "Select Id from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
                //    using (SqlCommand command = new SqlCommand(query4, connection))
                //    {
                //        command.Parameters.AddWithValue("@Id", employeeId);
                //        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                //        using (SqlDataReader reader = command.ExecuteReader())
                //        {
                //            while (reader.Read())
                //            {
                //                appointmentIds.Add(reader.GetInt32(0));
                //            }
                //        }
                //    }
                //    appointmentIdsList = appointmentIds.ConvertAll<string>(x => x.ToString());
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
                //    slotAvailabilityString.Add(d.value.ToString("yyyy/MM/dd HH:mm " + availabilityStatus[d.i] + " " + categoryList[d.i] + " " + appointmentIdsList[d.i])); //polnenje na listata
                //                                                                                                                                                           //so datum,vreme i status na termin(booked/free)
                //                                                                                                                                                           //                          .startsWith("Booke")/ree
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
                //    String appointment = zborovi2[4];
                //    DateTime privremena = d.value.AddMinutes(total);

                //    bool free = true;
                //    String[] tempZborovi;
                //    String tempWord2;
                //    String tempWord2AppId;

                //    for (DateTime j = d.value; j < privremena; j = j.AddMinutes(30))
                //    {
                //        if (count <= slotAvailabilityString.Count - 1)
                //        {
                //            zborovi2 = slotAvailabilityString[count].ToString().Split(' ');
                //        }
                //        word2 = zborovi2[2];

                //        if (j == dateTimeComp2)
                //        {
                //            if (word2.Contains("Booke") && !categories.Contains("10") && !categories.Contains("11") && Convert.ToInt32(appointment) != appId)
                //            {
                //                //found = true;
                //                if (count + 1 <= slotAvailabilityString.Count - 1)
                //                {
                //                    tempZborovi = slotAvailabilityString[count + 1].ToString().Split(' ');

                //                    tempWord2 = tempZborovi[2];
                //                    tempWord2AppId = tempZborovi[4];
                //                    if (tempWord2 != "ree" && Convert.ToInt32(tempWord2AppId) != appId)
                //                    {
                //                        free = false;
                //                    }

                //                }
                //            }



                //            dateTimeComp2 = dateTimeComp2.AddMinutes(30);
                //            if (count != slotAvailabilityString.Count)
                //            {
                //                count++;
                //            }
                //        }
                //    }
                //    if (free == true)
                //    {

                //        displayList.Add(d.value.ToString("yyyy/MM/dd HH:mm"));

                //    }



                //}
                //for (int i = 0; i < displayList.Count; i++)
                //{
                //    cmbTime.Items.Add(displayList[i]);
                //}
                //onLoadConfiguration();
                //slotAvailability.Clear();
                //slotAvailabilityString.Clear();
                //availabilityStatus.Clear();
                //displayList.Clear();
            }
        }
        public static bool first = true;
        public void onLoadConfiguration()
        {
            
            customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
            employeeId = Convert.ToInt32(cmbEmployee.SelectedValue);
            if (first == true)
            {
                first = false;
                displayList.Clear();
                lbServices.Items.Clear();
                cmbTime.Items.Clear();
                slotAvailability.Clear();
                slotAvailabilityString.Clear();
                availabilityStatus.Clear();
                categoryList.Clear();
                appointmentIdsList.Clear();
                appointmentIds.Clear();
                cmbEmployee.Text = employee;
                cmbCustomer.Text = customer;

                string constr = ConfigurationManager.AppSettings["ConnectionString"];
                if (cmbServices.Text == "Одбери подкатегорија")
                {
                    lbTotalCost.Text = "0";
                    lbTotalCostDiscount.Text = "0";
                }
                label12.Text = time.ToShortDateString();
                lbHour.Text = hourApp.ToShortTimeString();


                SqlConnection con2 = new SqlConnection(constr);
                con2.Open();
                SqlCommand objCommand = new SqlCommand();
                if (customerId != 0)
                {
                    SqlCommand cmd = new SqlCommand("select Frequency from Customer where Id = '" + customerId + "'", con2);
                    cmd.ExecuteNonQuery();
                    DataTable dt2 = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt2);
                    String frequency = "";
                    foreach (DataRow dr in dt2.Rows)
                    {
                        frequency = dr["Frequency"].ToString();
                    }
                    int freq = Convert.ToInt32(frequency);
                    if (freq > 30)
                    {
                        if (DialogResult.Yes == MessageBox.Show("Лојален клиент!\nДали сакате да додадете дополнителен попуст од 10%?", "Дополнителен попуст", MessageBoxButtons.YesNo))
                        {
                            lbDiscount.Text = "10";
                        }
                    }
                    else
                    {
                        lbDiscount.Text = "0";
                    }
                }

                String[] parts;
                String service = "";
                String serviceId = "";
                DataTable dt3 = new DataTable();
                SqlDataAdapter da3;
                SqlCommand cmd2 = new SqlCommand();
                String[] deloviPrazniMesta;

                if (services.Contains(','))
                {
                    parts = services.Split(',');

                    foreach (string p in parts)
                    {
                        deloviPrazniMesta = p.Split(' ');
                        if (lbServices.Items.Count < 1)
                        {
                            lbServices.Items.Add(p);
                        }
                        if (con2.State == ConnectionState.Open)
                        {
                            con2.Close();
                        }
                        if (!deloviPrazniMesta[0].StartsWith("\r"))
                        {
                            string sql3 = string.Format("select Id from Category where Name = N'" + deloviPrazniMesta[0] + "' and Description=N'" + deloviPrazniMesta[1] + "'");
                            con2.Open();
                            SqlCommand cmd3 = new SqlCommand(sql3, con2);

                            SqlDataReader reader3 = cmd3.ExecuteReader();
                            while (reader3.Read())
                            {
                                serviceId = reader3["Id"].ToString();
                            }
                            if (con2.State == ConnectionState.Open)
                            {
                                con2.Close();
                            }
                        }
                        else
                        {
                            deloviPrazniMesta[0] = deloviPrazniMesta[0].Substring(1);

                            string sql3 = string.Format("select Id from Category where Name = N'" + deloviPrazniMesta[0] + "' and Description=N'" + deloviPrazniMesta[1] + "'");
                            con2.Open();
                            SqlCommand cmd3 = new SqlCommand(sql3, con2);

                            SqlDataReader reader3 = cmd3.ExecuteReader();
                            while (reader3.Read())
                            {
                                serviceId = reader3["Id"].ToString();
                            }
                            if (con2.State == ConnectionState.Open)
                            {
                                con2.Close();
                            }
                        }
                        SqlConnection con = new SqlConnection(constr);
                        con.Open();
                        SqlCommand cmd1 = new SqlCommand("Select Price From Category where Id = '" + Convert.ToInt32(serviceId) + "'", con);
                        SqlCommand cmd4 = new SqlCommand("select Duration from Category where Id = '" + Convert.ToInt32(serviceId) + "'", con);

                        cmd1.ExecuteNonQuery();
                        cmd4.ExecuteNonQuery();

                        DataTable dt = new DataTable();
                        SqlDataAdapter da4 = new SqlDataAdapter(cmd1);
                        da4.Fill(dt);
                        DataTable dt5 = new DataTable();
                        SqlDataAdapter da5 = new SqlDataAdapter(cmd4);
                        da5.Fill(dt5);

                        String price = "";
                        String duration = "";

                        foreach (DataRow dr in dt.Rows)
                        {
                            price = dr["Price"].ToString();
                        }
                        foreach (DataRow dr in dt5.Rows)
                        {
                            duration = dr["Duration"].ToString();
                        }

                        int cena = Convert.ToInt32(price);
                        cost += cena;
                        lbTotalCost.Text = cost.ToString();
                        int discount = Convert.ToInt32(lbDiscount.Text);
                        int totalCost = cost - (cost * discount / 100);
                        lbTotalCostDiscount.Text = totalCost.ToString();
                        String[] words = duration.Split(':');
                        int cas = Convert.ToInt32(words[0]);
                        int minuti = Convert.ToInt32(words[1]);
                        casovi += cas;
                        minutiTotal += minuti;
                        String temp = "";
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
                }
                else
                {
                    deloviPrazniMesta = services.Split(' ');
                    //deloviPrazniMesta[0] = deloviPrazniMesta[0].Substring(1);

                    string sql3 = string.Format("select Id from Category where Name = N'" + deloviPrazniMesta[0] + "' and Description=N'" + deloviPrazniMesta[1] + "'");
                    //con2.Open();
                    SqlCommand cmd3 = new SqlCommand(sql3, con2);

                    SqlDataReader reader3 = cmd3.ExecuteReader();
                    while (reader3.Read())
                    {
                        serviceId = reader3["Id"].ToString();
                    }
                    if (con2.State == ConnectionState.Open)
                    {
                        con2.Close();
                    }
                    //service = deloviPrazniMesta[0] + " " + deloviPrazniMesta[1];
                    SqlConnection con = new SqlConnection(constr);
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Select Price From Category where Id = '" + Convert.ToInt32(serviceId) + "'", con);
                    SqlCommand cmd4 = new SqlCommand("select Duration from Category where Id = '" + Convert.ToInt32(serviceId) + "'", con);

                    cmd1.ExecuteNonQuery();
                    cmd4.ExecuteNonQuery();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da4 = new SqlDataAdapter(cmd1);
                    da4.Fill(dt);
                    DataTable dt5 = new DataTable();
                    SqlDataAdapter da5 = new SqlDataAdapter(cmd4);
                    da5.Fill(dt5);

                    String price = "";
                    String duration = "";

                    foreach (DataRow dr in dt.Rows)
                    {
                        price = dr["Price"].ToString();
                    }
                    foreach (DataRow dr in dt5.Rows)
                    {
                        duration = dr["Duration"].ToString();
                    }

                    int cena = Convert.ToInt32(price);
                    cost += cena;
                    lbTotalCost.Text = cost.ToString();
                    int discount = Convert.ToInt32(lbDiscount.Text);
                    int totalCost = cost - (cost * discount / 100);
                    lbTotalCostDiscount.Text = totalCost.ToString();
                    String[] words = duration.Split(':');
                    int cas = Convert.ToInt32(words[0]);
                    int minuti = Convert.ToInt32(words[1]);
                    casovi += cas;
                    minutiTotal += minuti;
                    String temp = "";
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
                    lbServices.Items.Add(services.ToString());
                }
                //cmbTime.Items.Clear();
                //displayList.Clear();
                //slotAvailabilityString.Clear();
                //slotAvailability.Clear();
                String[] temporary = lbTimeRequired.Text.Split(':');
                int Chas = Convert.ToInt32(temporary[0]);
                int Minuti = Convert.ToInt32(temporary[1]);
                int total = Chas * 60 + Minuti;
                if (con2.State == ConnectionState.Open)
                {
                    con2.Close();
                }
                con2.Open();
                using (SqlConnection connection = con2)
                {
                    //connection.Open();
                    string query = "Select Timeslot from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", employeeId);
                        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                slotAvailability.Add(reader.GetDateTime(0));
                            }
                        }
                    }
                    string query2 = "Select AvailabilityId from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
                    using (SqlCommand command = new SqlCommand(query2, connection))
                    {
                        command.Parameters.AddWithValue("@Id", employeeId);
                        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                availabilityStatus.Add(reader.GetString(0));
                            }
                        }
                    }

                    string query3 = "Select CategoryIds from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
                    using (SqlCommand command = new SqlCommand(query3, connection))
                    {
                        command.Parameters.AddWithValue("@Id", employeeId);
                        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                categoryList.Add(reader.GetString(0));
                            }
                        }
                    }

                    string query4 = "Select Id from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
                    using (SqlCommand command = new SqlCommand(query4, connection))
                    {
                        command.Parameters.AddWithValue("@Id", employeeId);
                        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                appointmentIds.Add(reader.GetInt32(0));
                            }
                        }
                    }
                    appointmentIdsList = appointmentIds.ConvertAll<string>(x => x.ToString());
                    //string query3 = "Select Timeslot, [availability] from SlotAvailability where [availability] = 'free' and CONVERT(DATE,TimeSlot)=@DateApp ORDER by TimeSlot";
                    //using (SqlCommand command = new SqlCommand(query3, connection))
                    //{
                    //    command.Parameters.Add("@DateApp", SqlDbType.Date).Value = DateTime.Parse(label12.Text);
                    //    using (SqlDataReader reader = command.ExecuteReader())
                    //    {
                    //        while (reader.Read())
                    //        {
                    //            availableTimes.Add(reader.GetDateTime(0));
                    //        }
                    //    }
                    //}
                }




                //List<string> list = new List<string>();
                //foreach (var d in slotAvailability.Select((value, i) => (value, i)))
                //{
                //    slotAvailabilityString.Add(d.value.ToString("yyyy/MM/dd HH:mm " + availabilityStatus[d.i] + " " + categoryList[d.i] + " " + 
                        
                //        //sList[d.i])); //polnenje na listata
                //                                                                                                                                                           //so datum,vreme i status na termin(booked/free)
                //                                                                                                                                                           //                          .startsWith("Booke")/ree
                //}
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
                    String appointment = zborovi2[4];
                    DateTime privremena = d.value.AddMinutes(total);

                    bool free = true;
                    String[] tempZborovi;
                    String tempWord2;
                    String tempWord2AppId;

                    for (DateTime j = d.value; j < privremena; j = j.AddMinutes(30))
                    {
                        if (count <= slotAvailabilityString.Count - 1)
                        {
                            zborovi2 = slotAvailabilityString[count].ToString().Split(' ');
                        }
                        word2 = zborovi2[2];

                        if (j == dateTimeComp2)
                        {
                            if (word2.Contains("Booke") && !categories.Contains("10") && !categories.Contains("11") && Convert.ToInt32(appointment) != appId)
                            {
                                //found = true;
                                if (count + 1 <= slotAvailabilityString.Count - 1)
                                {
                                    tempZborovi = slotAvailabilityString[count + 1].ToString().Split(' ');

                                    tempWord2 = tempZborovi[2];
                                    tempWord2AppId = tempZborovi[4];
                                    if (tempWord2 != "ree" && Convert.ToInt32(tempWord2AppId) != appId)
                                    {
                                        free = false;
                                    }

                                }
                            }



                            dateTimeComp2 = dateTimeComp2.AddMinutes(30);
                            if (count != slotAvailabilityString.Count)
                            {
                                count++;
                            }
                        }
                    }
                    if (free == true)
                    {

                        displayList.Add(d.value.ToString("yyyy/MM/dd HH:mm"));

                    }



                }
                for (int i = 0; i < displayList.Count; i++)
                {
                    cmbTime.Items.Add(displayList[i]);
                }
                first = false;
            }
        }
        
        private void btnBookAppointment_Click(object sender, EventArgs e)
        {
            if (lbServices.Items.Count > 0 && cmbTime.Text != "-- Одбери време --")
            {
                employeeId = Convert.ToInt32(cmbEmployee.SelectedValue);
                customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                //MessageBox.Show("custId e " + customerId);
                String startTimeString = cmbTime.SelectedItem.ToString();
                String[] vreme = startTimeString.Split(' ');
                String temp = String.Format("{0}", vreme[0]);
                TimeSpan startTime = TimeSpan.Parse(temp);
                String finishTimeString = lbTimeFinishes.Text;
                TimeSpan finishTime = TimeSpan.Parse(finishTimeString);
                DateTime startDate = DateTime.Parse(label12.Text);
                DateTime startDateTime = startDate.Add(startTime);
                DateTime finishDateTime = startDate.Add(finishTime);

                String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                //MessageBox.Show("Воспоставена конекција");

                SqlCommand cmd = new SqlCommand("select PhoneNumber from Customer where Id='" + customerId + "'", conn);
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                String phoneNumber = "";
                foreach (DataRow dr in dt.Rows)
                {
                    phoneNumber = dr["PhoneNumber"].ToString();
                }
                phoneNumber = Regex.Replace(phoneNumber, "[^0-9]", "");
                phoneNumber = "+389" + phoneNumber.Substring(1);
                

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

                        objCommand.Parameters.Add("@Id", SqlDbType.Int).Value = appId;
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



                    objCommand.ExecuteNonQuery();
                    MessageBox.Show("Успешно ажуриран термин!");
                    if(lbHour.Text != startTimeString)
                    {
                        //AddNewAppointmentPage2.sendSms(phoneNumber, startDate, startTimeString, true, false);
                        AddNewAppointmentPage2.sendSmsTwillio(phoneNumber, startDate, startTimeString, true, false);
                    }

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
                MessageBox.Show("Задолжително е бирање на услуга и време!");
            }
        }

        private void calculateShowTimeComboBox(string connectionString, bool isEdit = false)
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
            employeeId = Convert.ToInt32(cmbEmployee.SelectedValue);
            if (total != 0)
            {
                using (SqlConnection connection = con)
                {
                    connection.Open();
                    if (!isEdit)
                    {


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
                    else
                    {
                        string query = "Select Timeslot from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp, @AppointmentEditId) ORDER by TimeSlot";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Id", employeeId);
                            command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                            command.Parameters.AddWithValue("@AppointmentEditId", appId);
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
                            command.Parameters.AddWithValue("@AppointmentEditId", appId);
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
                            command.Parameters.AddWithValue("@AppointmentEditId", appId);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    categoryList.Add(reader.GetString(0));
                                }
                            }
                        }
                    }
                    //string query3 = "Select Timeslot, [availability] from SlotAvailability where [availability] = 'free' and CONVERT(DATE,TimeSlot)=@DateApp ORDER by TimeSlot";
                    //using (SqlCommand command = new SqlCommand(query3, connection))
                    //{
                    //    command.Parameters.Add("@DateApp", SqlDbType.Date).Value = DateTime.Parse(label12.Text);
                    //    using (SqlDataReader reader = command.ExecuteReader())
                    //    {
                    //        while (reader.Read())
                    //        {
                    //            availableTimes.Add(reader.GetDateTime(0));
                    //        }
                    //    }
                    //}
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


                    for (DateTime j = d.value; j < privremena && count < slotAvailability.Count - 1; j = slotAvailability[count])
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
                    if (word2.Contains("Booke"))
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
            if (cmbServices.SelectedValue != null && cmbServices.Text!="Одбери услуга")
            {

                

                // Retrieve the selected service ID from the services combobox
                string selectedValue = cmbServices.SelectedValue.ToString();
                //string[] part = selectedValue.Split(',');
                //int serviceId = int.Parse(part[0].Substring(1));
                int id = Convert.ToInt32(cmbServices.SelectedValue);
                listOfServices += id + ",";
                // Retrieve the duration and price of the selected service from the database
                string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                ServiceItem item = new ServiceItem
                {
                    Name = getServiceName(id.ToString()),
                    ID = id
                };
                calculateTimeAndPrice(connectionString, selectedValue, true, false, item);
                calculateShowTimeComboBox(connectionString,true);
                

                ///////////////////////////////////////////////////////////////
                //    string query = "SELECT Duration, Price FROM Category WHERE Id = @ServiceId";
                //    using (SqlConnection connection = new SqlConnection(connectionString))
                //    {
                //        SqlCommand command = new SqlCommand(query, connection);
                //        command.Parameters.AddWithValue("@ServiceId", serviceId);
                //        connection.Open();
                //        SqlDataReader reader = command.ExecuteReader();
                //        while (reader.Read())
                //        {
                //            string duration = reader.GetString(0);
                //            string price = reader.GetString(1);

                //            // Update the labels for the required time, total price, discount, price with calculated discount, and ending of appointment
                //            lbTimeRequired.Text = duration;
                //            lbTotalCost.Text = price;
                //            decimal discount = 0;
                //            int customerId = (int)cmbCustomer.SelectedValue;
                //            if (IsCustomerLoyal()) // replace with your own method for checking if a customer is loyal
                //            {
                //                discount = Convert.ToInt32(price) * 0.1m; // 10% discount for loyal customers
                //            }
                //            lbDiscount.Text = discount.ToString();
                //            lbTotalCostDiscount.Text = (Convert.ToInt32(price) - discount).ToString();
                //            DateTime startTime = DateTime.Parse(cmbTime.SelectedItem.ToString());
                //            lbTimeFinishes.Text = (startTime.AddMinutes(Convert.ToInt32(duration))).ToString("hh:mm tt");
                //        }
                //    }
            }
            else
            {
                //MessageBox.Show("Ne e selektirano");
            }
            /*RetrieveCategories();
            UpdateTotalDurationAndPrice();*/

            /*
            int brojac = 0;
            employeeId = Convert.ToInt32(cmbEmployee.SelectedValue); 
            customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
            if (cmbServices.Text != "Одбери подкатегорија")
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
                SqlCommand cmd = new SqlCommand("Select Price From Category where Id = '" + id + "'", con);
                SqlCommand cmd2 = new SqlCommand("select Duration from Category where Id = '" + id + "'", con);

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);

                String price = "";
                String duration = "";

                foreach (DataRow dr in dt.Rows)
                {
                    price = dr["Price"].ToString();
                }
                foreach (DataRow dr in dt2.Rows)
                {
                    duration = dr["Duration"].ToString();
                }

                int cena = Convert.ToInt32(price);
                cost += cena;
                lbTotalCost.Text = cost.ToString();
                int discount = Convert.ToInt32(lbDiscount.Text);
                int totalCost = cost - (cost * discount / 100);
                lbTotalCostDiscount.Text = totalCost.ToString();
                String[] words = duration.Split(':');
                int cas = Convert.ToInt32(words[0]);
                int minuti = Convert.ToInt32(words[1]);
                casovi += cas;
                minutiTotal += minuti;
                String temp = "";
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

                lbServices.Items.Add(cmbServices.Text + " - " + price + " ден. " + duration);



                String[] temporary = lbTimeRequired.Text.Split(':');
                int Chas = Convert.ToInt32(temporary[0]);
                int Minuti = Convert.ToInt32(temporary[1]);
                int total = Chas * 60 + Minuti;


                using (SqlConnection connection = con)
                {
                    //connection.Open();
                    string query = "Select Timeslot from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", employeeId);
                        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                slotAvailability.Add(reader.GetDateTime(0));
                            }
                        }
                    }
                    string query2 = "Select AvailabilityId from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
                    using (SqlCommand command = new SqlCommand(query2, connection))
                    {
                        command.Parameters.AddWithValue("@Id", employeeId);
                        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                availabilityStatus.Add(reader.GetString(0));
                            }
                        }
                    }

                    string query3 = "Select CategoryIds from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
                    using (SqlCommand command = new SqlCommand(query3, connection))
                    {
                        command.Parameters.AddWithValue("@Id", employeeId);
                        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                categoryList.Add(reader.GetString(0));
                            }
                        }
                    }
                    //string query3 = "Select Timeslot, [availability] from SlotAvailability where [availability] = 'free' and CONVERT(DATE,TimeSlot)=@DateApp ORDER by TimeSlot";
                    //using (SqlCommand command = new SqlCommand(query3, connection))
                    //{
                    //    command.Parameters.Add("@DateApp", SqlDbType.Date).Value = DateTime.Parse(label12.Text);
                    //    using (SqlDataReader reader = command.ExecuteReader())
                    //    {
                    //        while (reader.Read())
                    //        {
                    //            availableTimes.Add(reader.GetDateTime(0));
                    //        }
                    //    }
                    //}
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
                    String tempWord2;


                    for (DateTime j = d.value; j < privremena; j = j.AddMinutes(30))
                    {
                        if (count <= slotAvailabilityString.Count - 1)
                        {
                            zborovi2 = slotAvailabilityString[count].ToString().Split(' ');
                        }
                        word2 = zborovi2[2];

                        if (j == dateTimeComp2)
                        {
                            if (word2.Contains("Booke") && !categories.Contains("10") && !categories.Contains("11"))
                            {
                                //found = true;
                                if (count + 1 <= slotAvailabilityString.Count - 1)
                                {
                                    tempZborovi = slotAvailabilityString[count + 1].ToString().Split(' ');

                                    tempWord2 = tempZborovi[2];

                                    if (tempWord2 != "ree")
                                    {
                                        free = false;
                                    }

                                }
                            }



                            dateTimeComp2 = dateTimeComp2.AddMinutes(30);
                            if (count != slotAvailabilityString.Count)
                            {
                                count++;
                            }
                        }
                    }
                    if (free == true)
                    {

                        displayList.Add(d.value.ToString("yyyy/MM/dd HH:mm"));

                    }



                }
                for (int i = 0; i < displayList.Count; i++)
                {
                    cmbTime.Items.Add(displayList[i]);
                }





            }
            
            else
            {
                
                
                

                
                cmbTime.Items.Clear();
                displayList.Clear();
                slotAvailabilityString.Clear();
                slotAvailability.Clear();
                int number = 0;
                var selected = uslugiDelovi.Where(x => int.TryParse(x, out number));
                    foreach (var s in selected)
                    {
                        int id = Convert.ToInt32(s);
                        listOfServices += id + ",";
                        string constr = ConfigurationManager.AppSettings["ConnectionString"];
                        SqlConnection con = new SqlConnection(constr);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Select Price From Category where Id = '" + id + "'", con);
                        SqlCommand cmd2 = new SqlCommand("select Duration from Category where Id = '" + id + "'", con);

                        cmd.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();

                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        DataTable dt2 = new DataTable();
                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        da2.Fill(dt2);

                        String price = "";
                        String duration = "";

                        foreach (DataRow dr in dt.Rows)
                        {
                            price = dr["Price"].ToString();
                        }
                        foreach (DataRow dr in dt2.Rows)
                        {
                            duration = dr["Duration"].ToString();
                        }

                        int cena = Convert.ToInt32(price);
                        cost += cena;
                        lbTotalCost.Text = cost.ToString();
                        int discount = Convert.ToInt32(lbDiscount.Text);
                        int totalCost = cost - (cost * discount / 100);
                        lbTotalCostDiscount.Text = totalCost.ToString();
                        String[] words = duration.Split(':');
                        int cas = Convert.ToInt32(words[0]);
                        int minuti = Convert.ToInt32(words[1]);
                        casovi += cas;
                        minutiTotal += minuti;
                        String temp = "";
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
                        if (cmbServices.Text != "Одбери подкатегорија")
                        {
                            lbServices.Items.Add(cmbServices.Text + " - " + price + " ден. " + duration);
                        }


                        String[] temporary = lbTimeRequired.Text.Split(':');
                        int Chas = Convert.ToInt32(temporary[0]);
                        int Minuti = Convert.ToInt32(temporary[1]);
                        int total = Chas * 60 + Minuti;


                        using (SqlConnection connection = con)
                        {
                            //connection.Open();
                            string query = "Select Timeslot from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Id", employeeId);
                                command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        slotAvailability.Add(reader.GetDateTime(0));
                                    }
                                }
                            }
                            string query2 = "Select AvailabilityId from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
                            using (SqlCommand command = new SqlCommand(query2, connection))
                            {
                                command.Parameters.AddWithValue("@Id", employeeId);
                                command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        availabilityStatus.Add(reader.GetString(0));
                                    }
                                }
                            }

                            string query3 = "Select CategoryIds from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
                            using (SqlCommand command = new SqlCommand(query3, connection))
                            {
                                command.Parameters.AddWithValue("@Id", employeeId);
                                command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        categoryList.Add(reader.GetString(0));
                                    }
                                }
                            }
                            //string query3 = "Select Timeslot, [availability] from SlotAvailability where [availability] = 'free' and CONVERT(DATE,TimeSlot)=@DateApp ORDER by TimeSlot";
                            //using (SqlCommand command = new SqlCommand(query3, connection))
                            //{
                            //    command.Parameters.Add("@DateApp", SqlDbType.Date).Value = DateTime.Parse(label12.Text);
                            //    using (SqlDataReader reader = command.ExecuteReader())
                            //    {
                            //        while (reader.Read())
                            //        {
                            //            availableTimes.Add(reader.GetDateTime(0));
                            //        }
                            //    }
                            //}
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
                            String tempWord2;


                            for (DateTime j = d.value; j < privremena; j = j.AddMinutes(30))
                            {
                                if (count <= slotAvailabilityString.Count - 1)
                                {
                                    zborovi2 = slotAvailabilityString[count].ToString().Split(' ');
                                }
                                word2 = zborovi2[2];

                                if (j == dateTimeComp2)
                                {
                                    if (word2.Contains("Booke") && !categories.Contains("10") && !categories.Contains("11"))
                                    {
                                        //found = true;
                                        if (count + 1 <= slotAvailabilityString.Count - 1)
                                        {
                                            tempZborovi = slotAvailabilityString[count + 1].ToString().Split(' ');

                                            tempWord2 = tempZborovi[2];

                                            if (tempWord2 != "ree")
                                            {
                                                free = false;
                                            }

                                        }
                                    }



                                    dateTimeComp2 = dateTimeComp2.AddMinutes(30);
                                    if (count != slotAvailabilityString.Count)
                                    {
                                        count++;
                                    }
                                }
                            }
                            if (free == true)
                            {

                                displayList.Add(d.value.ToString("yyyy/MM/dd HH:mm"));

                            }

                        }
                        for (int i = 0; i < displayList.Count; i++)
                        {
                            cmbTime.Items.Add(displayList[i]);
                        }
                    
                }
            }*/
        }
        String uslugi,vraboten,klient,uslugiIds,vrabotenId = "";
        String[] delovi,uslugiDelovi;

        private DataRow RetrieveAppointmentDetails(int appointmentId)
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Appointment WHERE Id = @appointmentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@appointmentId", appointmentId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0];
                }
                else
                {
                    throw new ArgumentException("Invalid appointment ID.");
                }
            }
        }

        public DataTable RetrieveCategories()
        {
            DataTable categories = new DataTable();
            
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            string query = "SELECT Id, Name, Description, Price, Duration FROM Category";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(categories);
                    }
                }
            }

            return categories;
        }

        private DataTable RetrieveEmployees()
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query to retrieve all employees from the database
                string query = "SELECT * FROM Employee";

                // Create a new data adapter and execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                // Create a new data table to hold the results
                DataTable employeeTable = new DataTable();

                // Fill the data table with the results of the query
                adapter.Fill(employeeTable);

                // Return the data table
                return employeeTable;
            }
        }

        private DataTable RetrieveCustomers()
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Customer";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable customers = new DataTable();
                        adapter.Fill(customers);
                        return customers;
                    }
                }
            }
        }

        private DataTable RetrieveCategories(int appointmentId)
        {
            DataTable categories = new DataTable();
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            string query = "SELECT cat.* FROM Appointment app cross apply string_split(RTRIM(REPLACE(app.CategoryIds,',',' ')),' ') join Category cat on cat.Id=value WHERE app.Id = @AppointmentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@AppointmentId", appointmentId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(categories);
            }

            return categories;
        }

        private DataRow RetrieveCategory(int categoryId)
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            string query = "SELECT * FROM Category WHERE Id = @CategoryId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Return the first row found
                            return reader.GetSchemaTable().Rows[0];
                        }
                    }
                }
            }

            // Return null if no category was found
            return null;
        }

        private void cmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dateTime = Convert.ToDateTime(cmbTime.Text);
            int cas = Convert.ToInt32(dateTime.Hour);
            int min = Convert.ToInt32(dateTime.Minute);
            //String vreme = dateTime.ToString("HH:mm");
            DateTime dateTime1 = Convert.ToDateTime(lbTimeRequired.Text);
            int cas1 = Convert.ToInt32(dateTime1.Hour);
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
                lbTimeFinishes.Text = totCas + ":" + totMin + "0";
            }
            else
            {
                lbTimeFinishes.Text = totCas + ":" + totMin;
            }
        }

        public int RetrieveFrequencyForCustomer(int customerId)
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            int frequency = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT Frequency FROM Customer WHERE Id = @customerId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        con.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            frequency = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return frequency;
        }

        private void PopulateComboBox()
        {
            // Clear the combo box
            cmbServices.Items.Clear();

            // Retrieve the service information from the database
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            string query = "SELECT Id, Name FROM Category ORDER BY Name";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                // Create a dictionary to hold the service ID and name pairs
                Dictionary<int, string> services = new Dictionary<int, string>();

                while (reader.Read())
                {
                    int id = reader.GetInt16(0);
                    string name = reader.GetString(1);

                    // Add the service ID and name to the dictionary
                    services.Add(id, name);
                }

                reader.Close();
                connection.Close();

                // Bind the dictionary to the combo box
                cmbServices.DataSource = new BindingSource(services, null);
                cmbServices.DisplayMember = "Value";
                cmbServices.ValueMember = "Key";
            }
        }



        //private void PopulateListBox()
        //{
        //    // Clear the list box
        //    lbServices.Items.Clear();

        //    // Retrieve the service information from the database
        //    List<int> selectedCategoryIds = GetSelectedCategoryIds(lbServices);
        //    if (selectedCategoryIds.Count == 0) return;

        //    string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        //    string categoryIds = string.Join(",", selectedCategoryIds);
        //    string query = $"SELECT Id, Name, Description, Price, Duration FROM Category WHERE Id IN ({categoryIds}) ORDER BY Name";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        SqlCommand command = new SqlCommand(query, connection);
        //        SqlDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            int id = reader.GetInt32(0);
        //            string name = reader.GetString(1);
        //            string description = reader.GetString(2);
        //            string price = reader.GetString(3);
        //            string duration = reader.GetString(4);

        //            // Create a new Service object and add it to the list box
        //            lbServices.Items.Add(new { Id = id, Name = name, Description = description, Price = price, Duration = duration });
        //        }

        //        reader.Close();
        //        connection.Close();
        //    }

        //    // Update the total duration and price
        //   // UpdateTotalDurationAndPrice();
        //}

        //private List<int> GetSelectedCategoryIds(ListBox listBox)
        //{
        //    List<int> selectedCategoryIds = new List<int>();
        //    foreach (DataRowView item in listBox.SelectedItems)
        //    {
        //        DataRow row = item.Row;
        //        selectedCategoryIds.Add((int)row["CategoryId"]);
        //    }
        //    return selectedCategoryIds;
        //}


        private void UpdateTotalDurationAndPrice()
        {
            List<int> selectedCategoryIds = new List<int>();
            int totalDurationMinutes = 0;
            TimeSpan totalDuration = TimeSpan.Zero;
            decimal totalPrice = 0;

            foreach (DataRowView item in lbServices.Items)
            {
                DataRow row = item.Row;
                int id = Convert.ToInt32(row["Id"]);
                selectedCategoryIds.Add(id);

                string durationString = (string)row["Duration"];
                TimeSpan duration = TimeSpan.Parse(durationString);

                totalDuration += duration;
                totalPrice += decimal.Parse((string)row["Price"]);
            }

            int totalDurationHours = totalDurationMinutes / 60;
            int remainingMinutes = totalDurationMinutes % 60;
            string totalTime = $"{totalDurationHours}h {remainingMinutes}min";

            lbTimeRequired.Text = totalTime.ToString();
            lbTotalCost.Text = $"${totalPrice}";

            if (IsCustomerLoyal(Convert.ToInt32(cmbCustomer.SelectedValue)))
            {
                decimal discountedPrice = totalPrice * 0.9m;
                lbTotalCostDiscount.Text = $"${discountedPrice}";
                lbTotalCostDiscount.Visible = true;
                //btnConfirm.Enabled = true;
            }
            else
            {
                lbTotalCostDiscount.Visible = false;
                //btnConfirm.Enabled = false;
            }
        }

        public bool IsCustomerLoyal(int id)
        {
            //int selectedCustomerId = Convert.ToInt32(cmbCustomer.SelectedValue);
            int frequency = RetrieveFrequencyForCustomer(id);
            return frequency >= 15;
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

        public string GetServiceName(int id)
        {
            string serviceName = "";
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Name FROM Category WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            serviceName = reader.GetString(0);
                        }
                    }
                }
            }
            return serviceName;
        }

        private void fillSelectedEmployeeCombobox(string constr)
        {
            string employeeName = "";
            string employeeSurname = "";
            int vrabId = 0;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select e.Id,e.name, e.surname from Employee e join Appointment a on e.Id=a.EmployeeId where a.Id = @AppId", conn);
                cmd.Parameters.AddWithValue("@AppId", appId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    employeeName = reader.GetString(1);
                    employeeSurname = reader.GetString(2);
                    vrabId = reader.GetInt16(0);
                }
                reader.Close();
            }

            // Set the customer combo box to the customer name and surname
            cmbEmployee.Text = employeeName + " " + employeeSurname;
            cmbEmployee.SelectedValue = vrabId;
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            if (IsCustomerLoyal(Convert.ToInt32(cmbCustomer.SelectedValue)))
            {
                DialogResult result = MessageBox.Show("Лојален клиент!\nБројот на посети е поголем од 15!\nДали одобрувате 10% попуст?", "Попуст", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    lbDiscount.Text = "10";
                }
                else
                {
                    lbDiscount.Text = "0";
                }
            }
            else
            {
                lbDiscount.Text = "0";
            }
            calculateTimeAndPrice(constr, null, false);
        }

        private void cmbTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fillEmployeeCombobox(string constr)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Id, Name + ' ' + Surname as Iminja FROM Employee WHERE Status='A' ORDER BY Iminja", con))
                {
                    //Fill the DataTable with records from Table.
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    //Insert the Default Item to DataTable.
                    //DataRow row = dt.NewRow();
                    //row[0] = 0;
                    //row[1] = "Одбери вработен";
                    //dt.Rows.InsertAt(row, 0);

                    //Assign DataTable as DataSource.

                    cmbEmployee.ValueMember = "Id";
                    cmbEmployee.DisplayMember = "Iminja";
                    cmbEmployee.DataSource = dt;

                    con.Open();
                    SqlCommand cmd = new SqlCommand("select e.Id,e.name, e.surname from Employee e join Appointment a on e.Id=a.EmployeeId where a.Id = @AppId", con);
                    cmd.Parameters.AddWithValue("@AppId", appId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        cmbEmployee.Text = reader.GetString(1) + " " + reader.GetString(2);
                        cmbEmployee.SelectedValue = reader.GetInt16(0);
                    }
                    reader.Close();
                    con.Close();
                }
            }
        }

        private void btnPrevDay_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            DateTime dateTime = Convert.ToDateTime(label12.Text);
            day--;
            lbDateDay.Text = dateTime.AddDays(day).ToString("dd.MM.yyyy dddd");
            label12.Text = Convert.ToDateTime(lbDateDay.Text).ToShortDateString();
            calculateShowTimeComboBox(constr, true);
            Cursor = Cursors.Default;
            day = 0;
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            DateTime dateTime = Convert.ToDateTime(label12.Text);
            day++;
            lbDateDay.Text = dateTime.AddDays(day).ToString("dd.MM.yyyy dddd");
            label12.Text = Convert.ToDateTime(lbDateDay.Text).ToShortDateString();
            calculateShowTimeComboBox(constr, true);
            Cursor = Cursors.Default;
            day = 0;
        }

        private void fillSelectedCustomerCombobox(string constr)
        {
            string customerName = "";
            string customerSurname = "";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select c.Id,c.name, c.surname from Customer c join Appointment a on c.Id=a.CustomerId where a.Id = @AppId", conn);
                cmd.Parameters.AddWithValue("@AppId", appId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customerName = reader.GetString(1);
                    customerSurname = reader.GetString(2);
                    customerId = reader.GetInt16(0);
                }
                reader.Close();
            }

            // Set the customer combo box to the customer name and surname
            cmbCustomer.Text = customerName + " " + customerSurname;
            cmbCustomer.SelectedValue = customerId;
        }

        private void fillCustomerCombobox(string constr)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Id, Name + ' ' + Surname as Iminja FROM Customer WHERE Status='A' ORDER BY Iminja", con))
                {
                    //Fill the DataTable with records from Table.
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    

                    //Assign DataTable as DataSource.

                    cmbCustomer.ValueMember = "Id";
                    cmbCustomer.DisplayMember = "Iminja";
                    cmbCustomer.DataSource = dt;

                    con.Open();
                    SqlCommand cmd = new SqlCommand("select c.Id,c.name, c.surname from Customer c join Appointment a on c.Id=a.CustomerId where a.Id = @AppId", con);
                    cmd.Parameters.AddWithValue("@AppId", appId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        cmbCustomer.Text = reader.GetString(1) + " " + reader.GetString(2);
                        cmbCustomer.SelectedValue = reader.GetInt16(0);
                    }
                    reader.Close();
                }
            }
        }

        private void fillServicesCombobox(string constr)
        {
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
                    row[1] = "Одбери услуга";
                    dt.Rows.InsertAt(row, 0);

                    //Assign DataTable as DataSource.

                    cmbServices.ValueMember = "Id";
                    cmbServices.DisplayMember = "Iminja";
                    cmbServices.DataSource = dt;
                }
            }
        }
        
        public string getServiceName(string id)
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            string query = "select STRING_AGG(concat(cat.Name,' ',cat.Description,' - ',cat.Price,N' ден. ',cat.Duration), ','+CHAR(13)) as [Услуги] from Category cat WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", Convert.ToInt16(id));

                string serviceName = command.ExecuteScalar()?.ToString();

                connection.Close();

                return serviceName;
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

        private void fillServicesListBox(string constr)
        {
            Cursor = Cursors.WaitCursor;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql3 = string.Format(@"select e.Name+' '+e.Surname as Вработен, cu.Name+' '+cu.Surname as Клиент,
                                            STRING_AGG(concat(cat.Name,' ',cat.Description,' - ',cat.Price,N' ден. ',cat.Duration), ','+CHAR(13)) as [Услуги],
                                            CategoryIds,EmployeeId
                                            from Appointment app
                                            cross apply string_split(RTRIM(REPLACE(app.CategoryIds,',',' ')),' ')
                                            join Category cat
                                            on cat.Id=value
                                            join Employee e
                                            on app.EmployeeId=e.Id
                                            join Customer cu
                                            on app.CustomerId=cu.Id
                                            where app.Id='" + appId + "'group by e.Name+' '+e.Surname, cu.Name+' '+cu.Surname,CategoryIds,EmployeeId");
                con.Open();
                SqlCommand cmd3 = new SqlCommand(sql3, con);

                SqlDataReader reader3 = cmd3.ExecuteReader();
                while (reader3.Read())
                {

                    uslugi = reader3["Услуги"].ToString();
                    uslugiIds = reader3["CategoryIds"].ToString();
                }
                uslugiDelovi = uslugiIds.Split(',');
                List<string> modifiedIdsList = new List<string>(uslugiDelovi);

                modifiedIdsList.RemoveAll(string.IsNullOrEmpty);
                uslugiDelovi = modifiedIdsList.ToArray();
                //uslugiDelovi = uslugiDelovi.Take(uslugiDelovi.Length - 1).ToArray();
                reader3.Close();
            }

            foreach (string part in uslugiDelovi)
            {
                calculateTimeAndPrice(constr, part, false);
                listOfServices += Convert.ToInt16(part) + ",";
                ServiceItem item = new ServiceItem
                {
                    Name = getServiceName(part),
                    ID = Convert.ToInt16(part)
                };
                lbServices.Items.Add(item);
            }

            Cursor = Cursors.Default;
        }

        private void calculateTimeAndPrice(string constr, string id, bool isFromComboBox, bool isDeletion=false, ServiceItem item=null)
        {
            Cursor = Cursors.WaitCursor;
            string query = "SELECT COUNT(*) FROM Category WHERE Id = @ID";
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                int totalCost;
                string duration="";
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


        /*private void calculateTimeAndPrice(string constr, string id, bool isFromComboBox, bool isDeletion=false)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                if (id != null)
                {

                }
                SqlCommand cmd = new SqlCommand("Select Price From Category where Id = '" + Convert.ToInt16(id) + "'", con);
                SqlCommand cmd2 = new SqlCommand("select Duration from Category where Id = '" + Convert.ToInt16(id) + "'", con);

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);

                String price = "";
                String duration = "";

                foreach (DataRow dr in dt.Rows)
                {
                    price = dr["Price"].ToString();
                }
                foreach (DataRow dr in dt2.Rows)
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
                int totalCost = cost - (cost * discount / 100);
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
                    
                
                String temp = "";
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
                if (isFromComboBox)
                {
                    lbServices.Items.Add(cmbServices.Text + " - " + price + " ден. " + duration);
                }
                con.Close();
            }
        }
        */

        private void fillLabelDateDay()
        {
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            DateTime dateTime = Convert.ToDateTime(label12.Text);
            lbDateDay.Text = dateTime.ToString("dd.MM.yyyy dddd");
            calculateShowTimeComboBox(constr, true);
        }

        private void EditAppointment_Load(object sender, EventArgs e)
        {
            this.BackColor = Form1.backColor;
            labelTitle.ForeColor = Form1.foreColor;
            lbServices.BackColor = Form1.backColor;
            cmbTime.BackColor = Form1.backColor;
            cmbTime.ForeColor = Form1.whiteColor;
            cmbEmployee.BackColor = Form1.backColor;
            cmbEmployee.ForeColor = Form1.whiteColor;
            cmbCustomer.BackColor = Form1.backColor;
            cmbCustomer.ForeColor = Form1.whiteColor;
            lbServices.ForeColor = Form1.whiteColor;
            tbNotes.BackColor = Form1.backColor;
            tbNotes.ForeColor = Form1.whiteColor;
            cmbServices.BackColor = Form1.backColor;
            cmbServices.ForeColor = Form1.whiteColor;
            label12.Text = time.ToShortDateString();
            lbHour.Text = hourApp.ToShortTimeString();
            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            fillEmployeeCombobox(constr);
            //fillSelectedEmployeeCombobox(constr);
            fillCustomerCombobox(constr);
            //fillSelectedCustomerCombobox(constr);
            fillServicesCombobox(constr);
            /*if (IsCustomerLoyal())
            {
                DialogResult result = MessageBox.Show("This customer is loyal. Do you want to offer a 10% discount?", "Discount", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    lbDiscount.Text = "10";
                }
                else
                {
                    lbDiscount.Text = "0";
                }
            }
            else
            {
                lbDiscount.Text = "0";
            }*/
            fillServicesListBox(constr);
            tbNotes.Text = notes;
            fillLabelDateDay();
            //calculateShowTimeComboBox(constr,true);
            //PopulateListBox();
            ////////////////////////////////////
            /*RetrieveCustomers();
            PopulateComboBox();
            PopulateListBox();
            UpdateTotalDurationAndPrice();*/

            //appId = (int)this.Tag;

            //// Retrieve the appointment details from the database
            //DataRow appointmentRow = RetrieveAppointmentDetails(appId);

            //// Retrieve the customers from the database
            //DataTable customersTable = RetrieveCustomers();

            //// Bind the customers to the ComboBox
            //cmbCustomer.DataSource = customersTable;
            //cmbCustomer.ValueMember = "Id";
            //cmbCustomer.DisplayMember = "Name";

            //// Set the selected customer in the customer combo-box
            //cmbCustomer.SelectedValue = appointmentRow["CustomerId"];

            //// Set the selected employee in the employee combo-box
            //cmbEmployee.SelectedValue = appointmentRow["EmployeeId"];

            //// Populate the combo box with the categories
            //DataTable categories = RetrieveCategories();
            //foreach (DataRow row in categories.Rows)
            //{
            //    string categoryName = row["Name"].ToString();
            //    int categoryId = Convert.ToInt32(row["Id"]);
            //    cmbServices.Items.Add(new KeyValuePair<int, string>(categoryId, categoryName));
            //}

            //// Set the ValueMember and DisplayMember properties of the combo box
            //cmbServices.DisplayMember = "Value";
            //cmbServices.ValueMember = "Key";

            //// Populate the employee combo box
            //DataTable employees = RetrieveEmployees();
            //cmbEmployee.DataSource = employees;
            //cmbEmployee.DisplayMember = "Name";
            //cmbEmployee.ValueMember = "Id";
            //cmbEmployee.SelectedValue = appointmentRow["EmployeeId"];

            //// Populate the category list box
            //DataTable categoriesListBox = RetrieveCategories(appId);
            //lbServices.DataSource = categoriesListBox;
            //lbServices.ValueMember = "Id";
            //lbServices.DisplayMember = "Name";


            //List<int> selectedCategoryIds = new List<int>();

            //int totalDurationMinutes = 0;
            //TimeSpan totalDuration = TimeSpan.Zero;
            //int totalPrice = 0;



            //foreach (DataRowView item in lbServices.Items)
            //{
            //    DataRow row = item.Row;
            //    int id = Convert.ToInt32(row["Id"]);
            //    selectedCategoryIds.Add(id);


            //    string durationString = (string)row["Duration"];
            //    TimeSpan duration = TimeSpan.Parse(durationString);

            //    totalDurationMinutes += duration.Minutes + (duration.Hours * 60);
            //    totalPrice += int.Parse((string)row["Price"]);
            //}
            //int customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
            //int frequency = RetrieveFrequencyForCustomer(customerId);
            //if (frequency > 2)
            //{
            //    DialogResult result = MessageBox.Show("This customer is loyal. Do you want to offer a 10% discount?", "Discount", MessageBoxButtons.YesNo);
            //    if (result == DialogResult.Yes)
            //    {
            //        // Apply discount to price
            //        lbDiscount.Text = "10";
            //        lbTotalCostDiscount.Text = (totalPrice * 0.9m).ToString();
            //    }
            //    else
            //    {
            //        lbDiscount.Text = "0";
            //        lbTotalCostDiscount.Text = totalPrice.ToString();
            //    }
            //}
            //int totalDurationHours = totalDurationMinutes / 60;
            //int remainingMinutes = totalDurationMinutes % 60;
            //string totalTime = $"{totalDurationHours}h {remainingMinutes}min";

            //lbTimeRequired.Text = totalTime.ToString();
            //lbTotalCost.Text = totalPrice.ToString();



            ////////////////////////////////////////////////////////////////////
            ///


            //this.BackColor = Form1.backColor;
            //labelTitle.ForeColor = Form1.foreColor;
            //lbServices.BackColor = Form1.backColor;
            //cmbTime.BackColor = Form1.backColor;
            //cmbTime.ForeColor = Form1.whiteColor;
            //cmbEmployee.BackColor = Form1.backColor;
            //cmbEmployee.ForeColor = Form1.whiteColor;
            //cmbCustomer.BackColor = Form1.backColor;
            //cmbCustomer.ForeColor = Form1.whiteColor;
            //lbServices.ForeColor = Form1.whiteColor;
            //tbNotes.BackColor = Form1.backColor;
            //tbNotes.ForeColor = Form1.whiteColor;
            //cmbServices.BackColor = Form1.backColor;
            //cmbServices.ForeColor = Form1.whiteColor;
            //label12.Text = time.ToShortDateString();
            //string constr = ConfigurationManager.AppSettings["ConnectionString"];
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            //    using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Id, Name + ' ' + Surname as Iminja FROM Employee WHERE Status='A' ORDER BY Iminja", con))
            //    {
            //        //Fill the DataTable with records from Table.
            //        DataTable dt = new DataTable();
            //        sda.Fill(dt);

            //        //Insert the Default Item to DataTable.
            //        DataRow row = dt.NewRow();
            //        row[0] = 0;
            //        row[1] = "Одбери вработен";
            //        dt.Rows.InsertAt(row, 0);

            //        //Assign DataTable as DataSource.

            //        cmbEmployee.ValueMember = "Id";
            //        cmbEmployee.DisplayMember = "Iminja";
            //        cmbEmployee.DataSource = dt;


            //    }
            //    using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Id, Name + ' ' + Surname as Iminja FROM Customer WHERE Status='A' ORDER BY Iminja", con))
            //    {
            //        //Fill the DataTable with records from Table.
            //        DataTable dt = new DataTable();
            //        sda.Fill(dt);

            //        //Insert the Default Item to DataTable.
            //        DataRow row = dt.NewRow();
            //        row[0] = 0;
            //        row[1] = "Одбери клиент";
            //        dt.Rows.InsertAt(row, 0);

            //        //Assign DataTable as DataSource.

            //        cmbCustomer.ValueMember = "Id";
            //        cmbCustomer.DisplayMember = "Iminja";
            //        cmbCustomer.DataSource = dt;



            //    }
            //    string sql3 = string.Format(@"select e.Name+' '+e.Surname as Вработен, cu.Name+' '+cu.Surname as Клиент,
            //                                STRING_AGG(concat(cat.Name,' ',cat.Description,' - ',cat.Price,N' ден. ',cat.Duration), ','+CHAR(13)) as [Услуги],
            //                                CategoryIds,EmployeeId
            //                                from Appointment app
            //                                cross apply string_split(RTRIM(REPLACE(app.CategoryIds,',',' ')),' ')
            //                                join Category cat
            //                                on cat.Id=value
            //                                join Employee e
            //                                on app.EmployeeId=e.Id
            //                                join Customer cu
            //                                on app.CustomerId=cu.Id
            //                                where app.Id='" + appId + "'group by e.Name+' '+e.Surname, cu.Name+' '+cu.Surname,CategoryIds,EmployeeId");
            //    con.Open();
            //    SqlCommand cmd3 = new SqlCommand(sql3, con);

            //    SqlDataReader reader3 = cmd3.ExecuteReader();
            //    while (reader3.Read())
            //    {
            //        vraboten = reader3["Вработен"].ToString();
            //        klient = reader3["Клиент"].ToString();
            //        uslugi = reader3["Услуги"].ToString();
            //        uslugiIds = reader3["CategoryIds"].ToString();
            //        vrabotenId = reader3["EmployeeId"].ToString();
            //    }
            //    uslugiDelovi = uslugiIds.Split(',');
            //    reader3.Close();
            //    using (SqlDataAdapter sda = new SqlDataAdapter("select c.Id, CONCAT(c.Name,' ',c.Description) as Iminja, c.Description From Category c", con))
            //    {
            //        //Fill the DataTable with records from Table.
            //        DataTable dt = new DataTable();
            //        sda.Fill(dt);

            //        //Insert the Default Item to DataTable.
            //        DataRow row = dt.NewRow();
            //        row[0] = 0;
            //        row[1] = "Одбери подкатегорија";
            //        dt.Rows.InsertAt(row, 0);

            //        //Assign DataTable as DataSource.

            //        cmbServices.ValueMember = "Id";
            //        cmbServices.DisplayMember = "Iminja";
            //        cmbServices.DataSource = dt;

            //    }




            //}
            ////MessageBox.Show("Pred_Selectedindex");
            //cmbEmployee.Text = vraboten;
            ////MessageBox.Show("Posle_Selectedindex");

            //cmbCustomer.Text = klient;
            //if (uslugi.Contains(","))
            //{
            //    delovi=uslugi.Split(',');
            //    foreach(string p in delovi)
            //    {
            //        lbServices.Items.Add(p);
            //    }
            //}
            //else
            //{
            //    lbServices.Items.Add(uslugi);
            //}
            ////if (cmbServices.Text == "Одбери подкатегорија")
            ////{
            ////    lbTotalCost.Text = "0";
            ////    lbTotalCostDiscount.Text = "0";
            ////}
            ////label12.Text = time.ToShortDateString();
            ////cmbEmployee.Text = employee;
            ////cmbCustomer.Text = customer;
            //customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
            //employeeId = Convert.ToInt32(cmbEmployee.SelectedValue);
            //SqlConnection con2 = new SqlConnection(constr);
            //con2.Open();
            //SqlCommand objCommand = new SqlCommand();
            //SqlCommand cmd = new SqlCommand("select Frequency from Customer where Id = '" + customerId + "'", con2);
            //cmd.ExecuteNonQuery();
            //DataTable dt2 = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(dt2);
            //String frequency = "";
            //foreach (DataRow dr in dt2.Rows)
            //{
            //    frequency = dr["Frequency"].ToString();
            //}
            //int freq = Convert.ToInt32(frequency);
            //if (freq > 30)
            //{
            //    if (DialogResult.Yes == MessageBox.Show("Лојален клиент!\nДали сакате да додадете дополнителен попуст од 10%?", "Дополнителен попуст", MessageBoxButtons.YesNo))
            //    {
            //        lbDiscount.Text = "10";
            //    }
            //}
            //else
            //{
            //    lbDiscount.Text = "0";
            //}


            //String[] parts;
            //String service="";
            //String serviceId="";
            //DataTable dt3 = new DataTable();
            //SqlDataAdapter da3;
            //SqlCommand cmd2 = new SqlCommand();
            //String[] deloviPrazniMesta;

            //if (services.Contains(','))
            //{
            //    parts = services.Split(',');

            //    foreach (string p in parts)
            //    {
            //        deloviPrazniMesta = p.Split(' ');
            //        lbServices.Items.Add(p);
            //        if (con2.State == ConnectionState.Open)
            //        {
            //            con2.Close();
            //        }
            //        if (!deloviPrazniMesta[0].StartsWith("\r"))
            //        {
            //            string sql3 = string.Format("select Id from Category where Name = N'" + deloviPrazniMesta[0] + "' and Description=N'" + deloviPrazniMesta[1] + "'");
            //            con2.Open();
            //            SqlCommand cmd3 = new SqlCommand(sql3, con2);

            //            SqlDataReader reader3 = cmd3.ExecuteReader();
            //            while (reader3.Read())
            //            {
            //                serviceId = reader3["Id"].ToString();
            //            }
            //            if (con2.State == ConnectionState.Open)
            //            {
            //                con2.Close();
            //            }
            //        }
            //        else
            //        {
            //            deloviPrazniMesta[0] = deloviPrazniMesta[0].Substring(1);

            //            string sql3 = string.Format("select Id from Category where Name = N'" + deloviPrazniMesta[0] + "' and Description=N'" + deloviPrazniMesta[1] + "'");
            //            con2.Open();
            //            SqlCommand cmd3 = new SqlCommand(sql3, con2);

            //            SqlDataReader reader3 = cmd3.ExecuteReader();
            //            while (reader3.Read())
            //            {
            //                serviceId = reader3["Id"].ToString();
            //            }
            //            if (con2.State == ConnectionState.Open)
            //            {
            //                con2.Close();
            //            }
            //        }
            //        SqlConnection con = new SqlConnection(constr);
            //        con.Open();
            //        SqlCommand cmd1 = new SqlCommand("Select Price From Category where Id = '" + Convert.ToInt32(serviceId) + "'", con);
            //        SqlCommand cmd4 = new SqlCommand("select Duration from Category where Id = '" + Convert.ToInt32(serviceId) + "'", con);

            //        cmd1.ExecuteNonQuery();
            //        cmd4.ExecuteNonQuery();

            //        DataTable dt = new DataTable();
            //        SqlDataAdapter da4 = new SqlDataAdapter(cmd1);
            //        da4.Fill(dt);
            //        DataTable dt5 = new DataTable();
            //        SqlDataAdapter da5 = new SqlDataAdapter(cmd4);
            //        da5.Fill(dt5);

            //        String price = "";
            //        String duration = "";

            //        foreach (DataRow dr in dt.Rows)
            //        {
            //            price = dr["Price"].ToString();
            //        }
            //        foreach (DataRow dr in dt5.Rows)
            //        {
            //            duration = dr["Duration"].ToString();
            //        }

            //        int cena = Convert.ToInt32(price);
            //        cost += cena;
            //        lbTotalCost.Text = cost.ToString();
            //        int discount = Convert.ToInt32(lbDiscount.Text);
            //        int totalCost = cost - (cost * discount / 100);
            //        lbTotalCostDiscount.Text = totalCost.ToString();
            //        String[] words = duration.Split(':');
            //        int cas = Convert.ToInt32(words[0]);
            //        int minuti = Convert.ToInt32(words[1]);
            //        casovi += cas;
            //        minutiTotal += minuti;
            //        String temp = "";
            //        if (minutiTotal.ToString().Length == 1)
            //        {
            //            temp = casovi + ":" + "0" + minutiTotal;
            //            lbTimeRequired.Text = temp;
            //        }
            //        else
            //        {
            //            if (minutiTotal >= 60)
            //            {
            //                casovi += 1;
            //                minutiTotal = minutiTotal - 60;
            //                if (minutiTotal.ToString().Length == 1)
            //                {
            //                    temp = casovi + ":" + "0" + minutiTotal;
            //                    lbTimeRequired.Text = temp;
            //                }
            //                else
            //                {
            //                    lbTimeRequired.Text = casovi + ":" + minutiTotal;
            //                }
            //            }
            //            else
            //            {
            //                lbTimeRequired.Text = casovi + ":" + minutiTotal;
            //            }

            //        }

            //    }
            //}
            //else
            //{
            //    deloviPrazniMesta = services.Split(' ');
            //    //service = deloviPrazniMesta[0] + " " + deloviPrazniMesta[1];
            //    lbServices.Items.Add(services.ToString());
            //}
            //cmbTime.Items.Clear();
            //displayList.Clear();
            //slotAvailabilityString.Clear();
            //slotAvailability.Clear();
            //String[] temporary = lbTimeRequired.Text.Split(':');
            //int Chas = Convert.ToInt32(temporary[0]);
            //int Minuti = Convert.ToInt32(temporary[1]);
            //int total = Chas * 60 + Minuti;
            //if (con2.State == ConnectionState.Open)
            //{
            //    con2.Close();
            //}
            //con2.Open();
            //using (SqlConnection connection = con2)
            //{
            //    //connection.Open();
            //    string query = "Select Timeslot from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
            //    using (SqlCommand command = new SqlCommand(query, connection))
            //    {
            //        command.Parameters.AddWithValue("@Id", employeeId);
            //        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                slotAvailability.Add(reader.GetDateTime(0));
            //            }
            //        }
            //    }
            //    string query2 = "Select AvailabilityId from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
            //    using (SqlCommand command = new SqlCommand(query2, connection))
            //    {
            //        command.Parameters.AddWithValue("@Id", employeeId);
            //        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                availabilityStatus.Add(reader.GetString(0));
            //            }
            //        }
            //    }

            //    string query3 = "Select CategoryIds from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
            //    using (SqlCommand command = new SqlCommand(query3, connection))
            //    {
            //        command.Parameters.AddWithValue("@Id", employeeId);
            //        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                categoryList.Add(reader.GetString(0));
            //            }
            //        }
            //    }

            //    string query4 = "Select Id from dbo.SlotAvailabilityGivenDateAndId(@Id, @DateApp) ORDER by TimeSlot";
            //    using (SqlCommand command = new SqlCommand(query4, connection))
            //    {
            //        command.Parameters.AddWithValue("@Id", employeeId);
            //        command.Parameters.AddWithValue("@DateApp", DateTime.Parse(label12.Text));
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                appointmentIds.Add(reader.GetInt32(0));
            //            }
            //        }
            //    }
            //    appointmentIdsList=appointmentIds.ConvertAll<string>(x => x.ToString());
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
            //    slotAvailabilityString.Add(d.value.ToString("yyyy/MM/dd HH:mm " + availabilityStatus[d.i] + " " + categoryList[d.i] + " " + appointmentIdsList[d.i])); //polnenje na listata
            //                                                                                                                           //so datum,vreme i status na termin(booked/free)
            //                                                                                                                           //                          .startsWith("Booke")/ree
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
            //    String appointment = zborovi2[4];
            //    DateTime privremena = d.value.AddMinutes(total);

            //    bool free = true;
            //    String[] tempZborovi;
            //    String tempWord2;
            //    String tempWord2AppId;

            //    for (DateTime j = d.value; j < privremena; j = j.AddMinutes(30))
            //    {
            //        if (count <= slotAvailabilityString.Count - 1)
            //        {
            //            zborovi2 = slotAvailabilityString[count].ToString().Split(' ');
            //        }
            //        word2 = zborovi2[2];

            //        if (j == dateTimeComp2)
            //        {
            //            if (word2.Contains("Booke") && !categories.Contains("10") && !categories.Contains("11") && Convert.ToInt32(appointment) != appId)
            //            {
            //                //found = true;
            //                if (count + 1 <= slotAvailabilityString.Count - 1)
            //                {
            //                    tempZborovi = slotAvailabilityString[count + 1].ToString().Split(' ');

            //                    tempWord2 = tempZborovi[2];
            //                    tempWord2AppId = tempZborovi[4];
            //                    if (tempWord2 != "ree" && Convert.ToInt32(tempWord2AppId)!=appId)
            //                    {
            //                        free = false;
            //                    }

            //                }
            //            }



            //            dateTimeComp2 = dateTimeComp2.AddMinutes(30);
            //            if (count != slotAvailabilityString.Count)
            //            {
            //                count++;
            //            }
            //        }
            //    }
            //    if (free == true)
            //    {

            //        displayList.Add(d.value.ToString("yyyy/MM/dd HH:mm"));

            //    }



            //}
            //for (int i = 0; i < displayList.Count; i++)
            //{
            //    cmbTime.Items.Add(displayList[i]);
            //}
            //onLoadConfiguration();

        }
    }
}
