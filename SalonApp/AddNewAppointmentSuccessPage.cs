using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalonApp
{
    public partial class AddNewAppointmentSuccessPage : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public AddNewAppointmentSuccessPage()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height+40, 20, 20));
        }

        private void AddNewAppointmentSuccessPage_Load(object sender, EventArgs e)
        {
            this.BackColor = Form1.backColor;
            btnSuccess.BackColor = Form1.backColor;
            lbServices.BackColor = Form1.backColor;
            lbServices.ForeColor = Form1.whiteColor;
            timer1.Start();
        }
        internal struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        //public static uint GetIdleTime()
        //{
        //    LASTINPUTINFO LastUserAction = new LASTINPUTINFO();
        //    LastUserAction.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(LastUserAction);
        //    GetLastInputInfo(ref LastUserAction);
        //    return ((uint)Environment.TickCount - LastUserAction.dwTime);
        //}
        //public AddNewAppointmentPage2 add=new AddNewAppointmentPage2();
        public List<Form> forms = new List<Form>();

  
        private void timer1_Tick(object sender, EventArgs e)
        {
            
                this.Close();
                // All opened myForm instances
                foreach (Form f in Application.OpenForms)
                    if (f.Name == "AddNewAppointmentPage2")
                        forms.Add(f);
                    else if(f.Name == "AddNewAppointment")
                    {
                        forms.Add(f);
                    }

                // Now let's close opened myForm instances
                foreach (Form f in forms)
                    f.Close();
                timer1.Stop();
            
        }
    }
}
