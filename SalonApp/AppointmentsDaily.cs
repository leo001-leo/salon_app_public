using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalonApp
{
    public partial class AppointmentsDaily : Form
    {
        public AppointmentsDaily()
        {
            InitializeComponent();
        }
        int day, month, year;

        private void AppointmentsDaily_Load(object sender, EventArgs e)
        {
            btnDaily.BackColor = Form1.foreColor;
            btnDaily.ForeColor = Form1.whiteColor;
            btnAppointments.BackColor = Form1.foreColor;
            btnAppointments.ForeColor = Form1.whiteColor;
            this.BackColor = Form1.backColor;
            flpLeftNav.BackColor = Form1.backColor;
            lbDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            lbDay.Text = DateTime.Now.ToString("dddd, dd.MM.yyyy");
            lbTitle.ForeColor = Form1.foreColor;
            DateTime now = DateTime.Now;
            day = now.Day;
            month = now.Month;
            year = now.Year;
        }

        private void btnMonthly_Click(object sender, EventArgs e)
        {
            this.Hide();
            Appointments appointments = new Appointments();
            appointments.ShowDialog();
            this.Close();
        }

        private void btnShowAll_MouseEnter(object sender, EventArgs e)
        {
            btnShowAll.BackColor = Form1.foreColor;
        }

        private void btnShowAll_MouseLeave(object sender, EventArgs e)
        {
            btnShowAll.BackColor = Form1.backColor;
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            int daysTemp = DateTime.DaysInMonth(year, month);
            if (day != daysTemp)
            {
                day++;
            }
            else
            {
                if (month != 12)
                {
                    month++;
                    day = 1;
                }
                else
                {
                    day = 1;
                    month = 1;
                    year++;
                }


                //days = DateTime.DaysInMonth(year, month);
                //day = days;
            }
            DateTime startOfTheMonth = new DateTime(year, month, day);
            DayOfWeek dayOfWeek = startOfTheMonth.DayOfWeek;
            String dayName = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetDayName(dayOfWeek);
            lbDay.Text = dayName + ", " + day + "." + month + "." + year;

        }

        private void btnDashboard_Click(object sender, EventArgs e)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewAppointment add = new AddNewAppointment();
            add.ShowDialog();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            this.Hide();
            Categories categories = new Categories();
            categories.ShowDialog();
            this.Close();
        }

        int days;
        private void btnPrevDay_Click(object sender, EventArgs e)
        {
            if (day != 1)
            {
                day--;
            }
            else
            {
                if(month!=1)
                {
                    month--;
                }
                else
                {
                    month = 12;
                    year--;
                }
                
                
                days = DateTime.DaysInMonth(year, month);
                day = days;
            }
            DateTime startOfTheMonth = new DateTime(year, month, day);
            DayOfWeek dayOfWeek = startOfTheMonth.DayOfWeek;
            String dayName = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetDayName(dayOfWeek);
            lbDay.Text = dayName + ", " + day + "." + month + "." + year;

        }
    }
}
