using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalonApp
{
    public partial class UserControlDays : UserControl
    {
        public UserControlDays()
        {
            InitializeComponent();
        }
        //Appointments appointments = new Appointments();
        private void UserControlDays_Load(object sender, EventArgs e)
        {
            
        }

        public void days(int numday)
        {
            lbDays.Text = numday + "";
            
        }
        public static string num = "";
      
        private void lbDays_Click(object sender, EventArgs e)
        {
            //white = true;
            //Appointments appointments = new Appointments();
            //Appointments.text = lbDays.Text;
            //appointments.displayDays();
            //Appointments.opened = false;
            //appointments.ShowDialog();
            //lbDays.BackColor = Form1.whiteColor;
            foreach (Control y in this.Parent.Controls)
            {
                if (y is UserControlDays && y != this)
                {
                    y.BackColor = Form1.backColor;
                }
            }
            this.BackColor = Form1.whiteColor;
            num = lbDays.Text;
            Appointments ap = new Appointments();
            
            Appointments.changeNum(num);
            
        }
    }
}
