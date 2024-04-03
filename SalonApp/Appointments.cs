using OfficeOpenXml;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SalonApp
{
    public partial class Appointments : Form
    {
        public Appointments()
        {
            InitializeComponent();
            btnAppointments.BackColor = Form1.foreColor;
            timer1.Start();
        }
        public static int month, year;
        public bool paintWhiteFont = false;
        public static bool opened = true;
        public int count = 0;
        public static System.Windows.Forms.Label label;
        public static RichTextBox rich;
        public static int count2 = 0;
        public static DataGridView datagv;
        public static bool flag = false;
        public static Label lbTotAppoints;
        public void Appointments_Load(object sender, EventArgs e)
        {

            flpLeftNav.BackColor = Form1.backColor;
            this.BackColor = Form1.backColor;
            flpStatusDown.BackColor = Form1.backColor;
            flpStatusDown.ForeColor = Form1.whiteColor;
            lbDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            btnMonthly.BackColor = Form1.foreColor;
            btnMonthly.ForeColor = Form1.whiteColor;
            btnDaily.BackColor = Form1.backColor;
            btnDaily.ForeColor = Form1.foreColor;
            btnExportExcel.BackColor = Form1.backColor;
            btnExportExcel.ForeColor = Form1.whiteColor;
            rtbFreeTimeSlots.BackColor = Form1.backColor;
            lbMonth.Text = DateTime.Now.ToString("MMMM yyyy");
            cbEmployee.BackColor = Form1.backColor;
            cbEmployee.ForeColor = Form1.whiteColor;
            tbSearch.BackColor = Form1.backColor;
            tbSearch.ForeColor = Form1.whiteColor;
            lbTitle.ForeColor = Form1.foreColor;
            label = labelNum;
            list = lbFreeTimes;
            list2 = lbFreeTimeSlotsFinish;
            rich = rtbFreeTimeSlots;
            datagv = dataGridView1;
            lbTotAppoints = lbTotalAppointments;
            btnAdd.BackColor = Form1.foreColor;
            btnAdd.ForeColor = Form1.whiteColor;

            displayDays();

            string constr = ConfigurationManager.AppSettings["ConnectionString"];
            fillEmployeeComboBox(constr);
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            //MessageBox.Show("Успешна конекција");
            DateTime date = new DateTime(year, month, Convert.ToInt32(label.Text));


            SqlDataAdapter sqlData = new SqlDataAdapter("[FindAllAvailableTimeGaps]", conn);

            // Must specify 'SelectCommand' when using get queries
            sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlData.SelectCommand.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = date;
            sqlData.SelectCommand.Parameters.Add("@FinishTime", SqlDbType.DateTime).Value = date;
            DataTable table = new DataTable();
            sqlData.Fill(table);
            if (table.Rows.Count < 1)
            {
                MessageBox.Show("Нема слободен термин!");
            }


            lbFreeTimes.DisplayMember = "startavl";
            lbFreeTimes.DataSource = table;
            lbFreeTimeSlotsFinish.DisplayMember = "endavl";
            lbFreeTimeSlotsFinish.DataSource = table;
            displayAvailableTimeSlots();

        }
        public static string text;
        public static System.Windows.Forms.ListBox list = new ListBox();
        public static System.Windows.Forms.ListBox list2 = new ListBox();



        private void fillEmployeeComboBox(string constr)
        {
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

                    cbEmployee.ValueMember = "Id";
                    cbEmployee.DisplayMember = "Iminja";
                    cbEmployee.DataSource = dt;

                    /*for (int i=0;i<dt.Rows.Count;i++)
                    {
                        cmbVraboteni.Items.Add(dt.Rows[i][1]+" - "+dt.Rows[i][2]);
                        
                    }*/

                }
            }
        }

        public static void displayAvailableTimeSlots()
        {
            rich.Text = "";
            int i = 0;
            for (i = 0; i < list.Items.Count; i++)
            {
                string temp2 = ((DataRowView)list.Items[i])[0].ToString();
                DateTime dateTime1 = DateTime.Parse(temp2);
                string display = dateTime1.ToString("HH:mm");

                rich.Text += display + "-";
                doHere();
            }
            void doHere()
            {

                string temp2 = ((DataRowView)list2.Items[i])[1].ToString();
                DateTime dateTime1 = DateTime.Parse(temp2);
                string display = dateTime1.ToString("HH:mm");

                rich.Text += display + "\n";
            }
        }
        public static void changeNum(string num)
        {
            label.Text = num;
            datagv.Visible = true;
            AddNewAppointmentPage2.time = new DateTime(year, month, Convert.ToInt32(label.Text));
            EditAppointment.time = new DateTime(year, month, Convert.ToInt32(label.Text));
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            //MessageBox.Show("Успешна конекција");
            DateTime date = new DateTime(year, month, Convert.ToInt32(label.Text));
            displayAvailableTimeSlotsBySelectedDate(conn, date);

            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "[dbo].[SelectAppointmentsSpecificDate]";

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
                if (objCommand.CommandType == CommandType.StoredProcedure)
                {
                    objCommand.Parameters.Clear();
                    objCommand.Parameters.Add("@Datum", SqlDbType.Date).Value = date;
                }
                adapter = new SqlDataAdapter(objCommand);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                if (dataset.Tables[0].Rows.Count > 0)
                {
                    datagv.DataSource = dataset.Tables[0];
                    datagv.AllowUserToAddRows = false;
                    datagv.Columns["Id"].Visible = false;
                    datagv.Columns["Status"].Visible = false;

                    datagv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    datagv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                    datagv.Columns[4].DefaultCellStyle.Format = "HH:mm";
                    datagv.Columns[9].DefaultCellStyle.Format = "HH:mm";
                    datagv.Columns["Edit"].DisplayIndex = 11;
                    datagv.Columns["Delete"].DisplayIndex = 11;

                    datagv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    datagv.DefaultCellStyle.Font = new Font("Segoe UI", 12);

                    for (int i = 0; i < datagv.Rows.Count; i++)
                    {
                        datagv.Rows[i].Height = 30;
                    }



                    datagv.BackgroundColor = Form1.backColor;
                    datagv.Rows[0].Selected = false;
                    datagv.DefaultCellStyle.BackColor = Form1.backColor;
                    datagv.DefaultCellStyle.ForeColor = Form1.whiteColor;
                    datagv.ColumnHeadersDefaultCellStyle.BackColor = Form1.backColor;
                    datagv.ColumnHeadersDefaultCellStyle.ForeColor = Form1.foreColor;
                    datagv.GridColor = Form1.whiteColor;
                    datagv.EnableHeadersVisualStyles = false;
                    datagv.BorderStyle = BorderStyle.None;
                    datagv.Columns[1].Width = 100;
                    datagv.Columns[2].Width = 100;
                    datagv.Columns[4].Width = 150;
                    datagv.Columns[5].Width = 150;
                    //datagv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    // datagv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    foreach (DataGridViewRow myRow in datagv.Rows)
                    {
                        myRow.HeaderCell.Value = (myRow.Index + 1).ToString();
                        //myRow.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9);
                        myRow.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        myRow.HeaderCell.Style.BackColor = Form1.backColor;
                        myRow.HeaderCell.Style.ForeColor = Form1.foreColor;
                    }
                    lbTotAppoints.Text = Convert.ToString(datagv.Rows.Count);

                }
                else
                {
                    datagv.Visible = false;
                    MessageBox.Show("Слободен е денот! Нема закажано термини");
                    flag = true;

                    lbTotAppoints.Text = "0";
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
        public static void displayAvailableTimeSlotsBySelectedDate(SqlConnection conn, DateTime date)
        {


            SqlDataAdapter sqlData = new SqlDataAdapter("[FindAllAvailableTimeGaps]", conn);

            // Must specify 'SelectCommand' when using get queries
            sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlData.SelectCommand.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = date;
            sqlData.SelectCommand.Parameters.Add("@FinishTime", SqlDbType.DateTime).Value = date;
            DataTable table = new DataTable();

            // Store data in table
            sqlData.Fill(table);

            //cmbTime.ValueMember = "player_id";
            list.DisplayMember = "startavl";
            list.DataSource = table;
            list2.DisplayMember = "endavl";
            list2.DataSource = table;

            displayAvailableTimeSlots();
        }
        public void displayDays()
        {
            flpCalendar.Controls.Clear();
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            String monthName = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbMonth.Text = monthName + " " + year;
            // Get the first day of the month
            DateTime startOfTheMonth = new DateTime(year, month, 1);

            // Count of days of the month
            int days = DateTime.DaysInMonth(year, month);

            // Convert the startofthemonth to integer
            int dayOfTheWeek = Convert.ToInt32(startOfTheMonth.DayOfWeek.ToString("d")) + 1;

            // Create a blank usercontrol
            for (int i = 1; i < dayOfTheWeek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                flpCalendar.Controls.Add(ucblank);
            }
            UserControlDays userControlDays = new UserControlDays();

            // Create a usercontrol for days
            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                flpCalendar.Controls.Add(ucdays);
                if (i == now.Day && DateTime.Now.Month == month && DateTime.Now.Year == year)
                {
                    ucdays.BackColor = Form1.whiteColor;
                    changeNum(i.ToString());
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            lbDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
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

        }

        private void btnShowAll_MouseEnter(object sender, EventArgs e)
        {
            btnExportExcel.BackColor = Form1.foreColor;
        }

        private void btnShowAll_MouseLeave(object sender, EventArgs e)
        {
            btnExportExcel.BackColor = Form1.backColor;
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            // Clear container
            flpCalendar.Controls.Clear();

            // Increment month to go to previous month
            if (month != 12)
            {
                month++;
            }
            else
            {
                month = 1;
                year++;
            }

            String monthName = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbMonth.Text = monthName + " " + year;

            // Get the first day of the month
            DateTime startOfTheMonth = new DateTime(year, month, 1);

            // Count of days of the month
            int days = DateTime.DaysInMonth(year, month);

            // Convert the startofthemonth to integer
            int dayOfTheWeek = Convert.ToInt32(startOfTheMonth.DayOfWeek.ToString("d")) + 1;

            // Create a blank usercontrol
            for (int i = 1; i < dayOfTheWeek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                flpCalendar.Controls.Add(ucblank);
            }

            // Create a usercontrol for days
            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                flpCalendar.Controls.Add(ucdays);
                if (i == DateTime.Now.Day && DateTime.Now.Month == month && DateTime.Now.Year == year)
                {
                    ucdays.BackColor = Form1.whiteColor;
                    changeNum(i.ToString());
                }
            }
        }
        public void fillListBox()
        {
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            //MessageBox.Show("Успешна конекција");
            DateTime date = new DateTime(year, month, Convert.ToInt32(labelNum.Text));
            //DateTime date1 = new DateTime(year, month, Convert.ToInt32(label.Text));

            SqlDataAdapter sqlData = new SqlDataAdapter("[FindAllAvailableTimeGaps]", conn);

            // Must specify 'SelectCommand' when using get queries
            sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlData.SelectCommand.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = date;
            sqlData.SelectCommand.Parameters.Add("@FinishTime", SqlDbType.DateTime).Value = date;
            DataTable table = new DataTable();

            // Store data in table
            sqlData.Fill(table);
            if (table.Rows.Count < 1)
            {
                MessageBox.Show("Нема слободен термин!");
            }

            //cmbTime.ValueMember = "player_id";
            lbFreeTimes.DisplayMember = "startavl";
            lbFreeTimes.DataSource = table;
            lbFreeTimeSlotsFinish.DisplayMember = "endavl";
            lbFreeTimeSlotsFinish.DataSource = table;
        }
        private void btnDaily_Click(object sender, EventArgs e)
        {
            this.Hide();
            AppointmentsDaily appointments = new AppointmentsDaily();
            appointments.ShowDialog();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewAppointment add = new AddNewAppointment();
            this.Opacity = 70;
            add.ShowDialog();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            this.Hide();
            Categories categories = new Categories();
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

        private void btnAnalysis_Click(object sender, EventArgs e)
        {
            this.Hide();
            Analysis categories = new Analysis();
            categories.ShowDialog();
            this.Close();
        }


        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                ControlPaint.DrawCheckBox(e.Graphics, e.CellBounds.X + 1, e.CellBounds.Y + 1,
                    e.CellBounds.Width - 8, e.CellBounds.Height - 8,
                    ButtonState.Checked);
                e.Handled = true;
            }

        }

        private void cbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEmployee.Text != "Одбери вработен")
            {

                String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                DateTime date = new DateTime(year, month, Convert.ToInt32(label.Text));
                int id = Convert.ToInt32(cbEmployee.SelectedValue);
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "[dbo].[SelectAppointmentsSpecificDateEmployee]";

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
                    if (objCommand.CommandType == CommandType.StoredProcedure)
                    {
                        objCommand.Parameters.Clear();
                        objCommand.Parameters.Add("@Datum", SqlDbType.Date).Value = date;
                        objCommand.Parameters.Add("@Id", SqlDbType.SmallInt).Value = id;
                    }
                    adapter = new SqlDataAdapter(objCommand);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);

                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        datagv.DataSource = dataset.Tables[0];
                        datagv.AllowUserToAddRows = false;
                        datagv.Columns["Id"].Visible = false;
                        datagv.Columns["Status"].Visible = false;

                        datagv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        datagv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                        datagv.Columns[4].DefaultCellStyle.Format = "HH:mm";
                        datagv.Columns[9].DefaultCellStyle.Format = "HH:mm";
                        datagv.Columns["Edit"].DisplayIndex = 11;
                        datagv.Columns["Delete"].DisplayIndex = 11;

                        datagv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                        datagv.DefaultCellStyle.Font = new Font("Segoe UI", 12);

                        for (int i = 0; i < datagv.Rows.Count; i++)
                        {
                            datagv.Rows[i].Height = 30;
                        }

                        datagv.BackgroundColor = Form1.backColor;
                        datagv.Rows[0].Selected = false;
                        datagv.DefaultCellStyle.BackColor = Form1.backColor;
                        datagv.DefaultCellStyle.ForeColor = Form1.whiteColor;
                        datagv.ColumnHeadersDefaultCellStyle.BackColor = Form1.backColor;
                        datagv.ColumnHeadersDefaultCellStyle.ForeColor = Form1.foreColor;
                        datagv.GridColor = Form1.whiteColor;
                        datagv.EnableHeadersVisualStyles = false;
                        datagv.BorderStyle = BorderStyle.None;
                        datagv.Columns[1].Width = 100;
                        datagv.Columns[2].Width = 100;
                        datagv.Columns[4].Width = 150;
                        datagv.Columns[5].Width = 150;
                        //datagv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        // datagv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        foreach (DataGridViewRow myRow in datagv.Rows)
                        {
                            myRow.HeaderCell.Value = (myRow.Index + 1).ToString();
                            //myRow.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9);
                            myRow.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            myRow.HeaderCell.Style.BackColor = Form1.backColor;
                            myRow.HeaderCell.Style.ForeColor = Form1.foreColor;
                        }
                        datagv.Visible = true;
                        lbTotAppoints.Text = Convert.ToString(datagv.Rows.Count);
                        
                        displayAvailableTimeSlotsByEmployeeId(id);

                    }
                    else
                    {
                        datagv.Visible = false;
                        lbTotalAppointments.Text = "0";
                        displayAvailableTimeSlotsByEmployeeId(id);
                        MessageBox.Show("Слободен е денот! Нема закажано термини");
                        flag = true;
                        
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

        private void displayAvailableTimeSlotsByEmployeeId(int id)
        {
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            DateTime datum = new DateTime(year, month, Convert.ToInt32(label.Text));

            SqlDataAdapter sqlData = new SqlDataAdapter("[FindAllAvailableTimeGapsByEmployeeId]", conn);

            // Must specify 'SelectCommand' when using get queries
            sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlData.SelectCommand.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = datum;
            sqlData.SelectCommand.Parameters.Add("@FinishTime", SqlDbType.DateTime).Value = datum;
            sqlData.SelectCommand.Parameters.Add("@EmployeeId", SqlDbType.SmallInt).Value = id;
            DataTable table = new DataTable();
            sqlData.Fill(table);
            if (table.Rows.Count < 1)
            {
                MessageBox.Show("Нема слободен термин!");
            }


            lbFreeTimes.DisplayMember = "startavl";
            lbFreeTimes.DataSource = table;
            lbFreeTimeSlotsFinish.DisplayMember = "endavl";
            lbFreeTimeSlotsFinish.DataSource = table;
            displayAvailableTimeSlots();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                string searchText = tbSearch.Text.Trim();

                // Split the search text into first name and last name
                string[] names = searchText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string firstName = "";
                string lastName = "";
                string sqlQuery = "";
                String connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                if (names.Length >= 2)
                {
                    firstName = names[0];
                    lastName = names[1];
                    sqlQuery = @"select top(10) app.Id, app.StartTime as Почеток,cu.Name+' '+cu.Surname as Клиент,e.Name+' '+e.Surname as Вработен,
	                                STRING_AGG(concat(cat.Name,' ',cat.Description,' - ',cat.Price,N' ден. ',cat.Duration), ','+CHAR(13)) as [Услуги],
	                                app.Notes as Забелешка,app.FinishTime as Крај,app.TotalPrice as [Вкупна цена],app.TotalTime as Времетраење,app.Status
                                    from Appointment app
                                    cross apply string_split(RTRIM(REPLACE(app.CategoryIds,',',' ')),' ')
                                    join Category cat
                                    on cat.Id=value
                                    join Employee e
                                    on app.EmployeeId=e.Id
                                    join Customer cu
                                    on app.CustomerId=cu.Id
                                    where cu.Name LIKE N'%" + firstName + "%' and cu.Surname LIKE N'%" + lastName + "%' group by app.Id,app.StartTime,app.FinishTime,e.Name+' '+e.Surname,cu.Name+' '+cu.Surname,app.TotalTime,app.TotalPrice,app.Notes,app.Status order by app.StartTime desc";
                }
                else
                {
                    firstName = names[0];
                    sqlQuery = @"select top(10) app.Id, app.StartTime as Почеток,cu.Name+' '+cu.Surname as Клиент,e.Name+' '+e.Surname as Вработен,
	                                STRING_AGG(concat(cat.Name,' ',cat.Description,' - ',cat.Price,N' ден. ',cat.Duration), ','+CHAR(13)) as [Услуги],
	                                app.Notes as Забелешка,app.FinishTime as Крај,app.TotalPrice as [Вкупна цена],app.TotalTime as Времетраење,app.Status
                                    from Appointment app
                                    cross apply string_split(RTRIM(REPLACE(app.CategoryIds,',',' ')),' ')
                                    join Category cat
                                    on cat.Id=value
                                    join Employee e
                                    on app.EmployeeId=e.Id
                                    join Customer cu
                                    on app.CustomerId=cu.Id
                                    where cu.Name LIKE N'%" + firstName + "%' or cu.Nickname LIKE N'%" + firstName + "%' or cu.Surname LIKE N'%" + firstName + "%' group by app.Id,app.StartTime,app.FinishTime,e.Name+' '+e.Surname,cu.Name+' '+cu.Surname,app.TotalTime,app.TotalPrice,app.Notes,app.Status order by app.StartTime desc";
                }


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

                    adapter = new SqlDataAdapter(objCommand);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);

                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dataset.Tables[0];
                        dataGridView1.Visible = true;
                        dataGridView1.Columns["Id"].Visible = false;
                        dataGridView1.Columns["Status"].Visible = false;
                        //dataGridView1.Columns["Status"].Visible = false;
                        dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                        dataGridView1.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                        dataGridView1.AllowUserToAddRows = false;


                        dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                        dataGridView1.Columns["Edit"].DisplayIndex = 11;
                        dataGridView1.Columns["Delete"].DisplayIndex = 11;
                        //dgvVraboteni.Columns[11].HeaderText = "Преостанати денови";
                        //dgvVraboteni.Columns["IDChef"].Visible = false;
                        //dgvVraboteni.Columns["IDPosition"].Visible = false;
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
                        dataGridView1.Columns[1].Width = 100;
                        dataGridView1.Columns[2].Width = 100;
                        dataGridView1.Columns[4].Width = 150;
                        dataGridView1.Columns[5].Width = 150;
                        //datagv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        // datagv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        foreach (DataGridViewRow myRow in datagv.Rows)
                        {
                            myRow.HeaderCell.Value = (myRow.Index + 1).ToString();
                            //myRow.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9);
                            myRow.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            myRow.HeaderCell.Style.BackColor = Form1.backColor;
                            myRow.HeaderCell.Style.ForeColor = Form1.foreColor;
                        }
                        lbTotAppoints.Text = Convert.ToString(dataGridView1.Rows.Count);

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            dataGridView1.Rows[i].Height = 30;
                        }
                       
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

        private void tbSearch_Enter(object sender, EventArgs e)
        {
            tbSearch.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = datagv.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                //MessageBox.Show("Edit");
                datagv.Rows[e.RowIndex].Selected = false;
                EditAppointment edit = new EditAppointment(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString()));
                EditAppointment.employee = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                EditAppointment.customer = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                EditAppointment.services = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                EditAppointment.appId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                EditAppointment.notes = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Забелешка"].Value);
                EditAppointment.hourApp = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Почеток"].Value);
                edit.ShowDialog();
            }
            else if (colName == "Delete")
            {
                String connectionString = ConfigurationManager.AppSettings["ConnectionString"];

                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                //MessageBox.Show("Воспоставена конекција");


                //MessageBox.Show("Delete");
                dataGridView1.Rows[e.RowIndex].Selected = false;
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());

                SqlCommand cmd = new SqlCommand("select CustomerId, StartTime from Appointment where Id='" + id + "'", conn);
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                String custId = "";
                String dateApp = "";
                foreach (DataRow dr in dt.Rows)
                {
                    custId = dr["CustomerId"].ToString();
                    dateApp = dr["StartTime"].ToString();
                }
                DateTime date = Convert.ToDateTime(dateApp);
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Дали сте сигурни дека сакате да го избришете терминот?", "Бришење термин", buttons);
                if (result == DialogResult.Yes)
                {

                    //MessageBox.Show("Воспоставена конекција");

                    SqlCommand cmd1 = new SqlCommand("select PhoneNumber from Customer where Id='" + Convert.ToInt32(custId) + "'", conn);
                    cmd1.ExecuteNonQuery();
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    da1.Fill(dt1);
                    String phoneNumber = "";
                    foreach (DataRow dr in dt1.Rows)
                    {
                        phoneNumber = dr["PhoneNumber"].ToString();
                    }
                    phoneNumber = Regex.Replace(phoneNumber, "[^0-9]", "");
                    phoneNumber = "+389" + phoneNumber.Substring(1);

                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "[dbo].[DeleteAppointment]";

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
                        MessageBox.Show("Успешно избришан термин!");
                        //AddNewAppointmentPage2.sendSms(phoneNumber, date, date.ToString("HH:mm"), false, true);
                        AddNewAppointmentPage2.sendSmsTwillio(phoneNumber, date, date.ToString("HH:mm"), false, true);
                        displayDays();
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
                if (colname != "Edit" && colname != "Delete")
                {
                    dataGridView1.Cursor = Cursors.Default;
                }
                else
                {
                    dataGridView1.Cursor = Cursors.Hand;
                }
            }
        }

        public static DataTable GetAppointmentsData(string connectionString, string sqlQuery)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = sqlQuery;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public static void exportToExcel(string query, string nameSheet)
        {
            String connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            DataTable dataTable = Appointments.GetAppointmentsData(connectionString, query);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(nameSheet);

                // Style for header cells
                var headerStyle = worksheet.Cells[1, 1, 1, dataTable.Columns.Count].Style;
                headerStyle.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                headerStyle.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                headerStyle.Font.Color.SetColor(System.Drawing.Color.Black);
                headerStyle.Font.Bold = true;

                // Add headers
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    worksheet.Cells[1, col + 1].Value = dataTable.Columns[col].ColumnName;
                }

                // Add data
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        var value = dataTable.Rows[row][col];

                        // If the column type is DateTime, format it as a date
                        if (value is DateTime dateTimeValue)
                        {
                            worksheet.Cells[row + 2, col + 1].Value = dateTimeValue;
                            if (nameSheet == "Термини")
                            {
                                worksheet.Cells[row + 2, col + 1].Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                            }
                            else
                            {
                                worksheet.Cells[row + 2, col + 1].Style.Numberformat.Format = "yyyy-MM-dd";
                            }
                        }
                        else
                        {
                            worksheet.Cells[row + 2, col + 1].Value = value;

                        }
                    }
                }
                // Auto-fit column widths
                worksheet.Cells.AutoFitColumns();


                // Save the Excel file
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Save Excel File"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var excelFile = new FileInfo(saveFileDialog.FileName);
                    package.SaveAs(excelFile);
                    MessageBox.Show("Успешно е зачуван фајлот!");
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string query = @"select app.StartTime as Почеток,cu.Name+' '+cu.Surname as Клиент,e.Name+' '+e.Surname as Вработен,
	                            STRING_AGG(concat(cat.Name,' ',cat.Description,' - ',cat.Price,N' ден. ',cat.Duration), ','+CHAR(13)) as [Услуги],
	                            app.Notes as Забелешка,app.FinishTime as Крај,app.TotalPrice as [Вкупна цена],app.TotalTime as Времетраење
                                from Appointment app
                                cross apply string_split(RTRIM(REPLACE(app.CategoryIds,',',' ')),' ')
                                join Category cat
                                on cat.Id=value
                                join Employee e
                                on app.EmployeeId=e.Id
                                join Customer cu
                                on app.CustomerId=cu.Id
                                group by app.Id,app.StartTime,app.FinishTime,e.Name+' '+e.Surname,cu.Name+' '+cu.Surname,app.TotalTime,app.TotalPrice,app.Notes,app.Status
                                order by app.StartTime desc";
            exportToExcel(query, "Термини");
        }

        private void btnPrevMonth_Click(object sender, EventArgs e)
        {
            // Clear container
            flpCalendar.Controls.Clear();

            // Decrement month to go to previous month
            if (month != 1)
            {
                month--;
            }
            else
            {
                month = 12;
                year--;
            }
            String monthName = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbMonth.Text = monthName + " " + year;

            // Get the first day of the month
            DateTime startOfTheMonth = new DateTime(year, month, 1);

            // Count of days of the month
            int days = DateTime.DaysInMonth(year, month);

            // Convert the startofthemonth to integer
            int dayOfTheWeek = Convert.ToInt32(startOfTheMonth.DayOfWeek.ToString("d")) + 1;

            // Create a blank usercontrol
            for (int i = 1; i < dayOfTheWeek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                flpCalendar.Controls.Add(ucblank);
            }

            // Create a usercontrol for days
            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                flpCalendar.Controls.Add(ucdays);
                if (i == DateTime.Now.Day && DateTime.Now.Month == month && DateTime.Now.Year == year)
                {
                    ucdays.BackColor = Form1.whiteColor;
                    changeNum(i.ToString());
                }
            }
        }
    }
}
