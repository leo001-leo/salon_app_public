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
    public partial class Form1 : Form
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

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        public static Color backColor = ColorTranslator.FromHtml("#000814");
        public static Color foreColor = ColorTranslator.FromHtml("#f35b04");
        public static Color whiteColor = ColorTranslator.FromHtml("#edeceb");

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = backColor;
            this.ForeColor = foreColor;
            tbUsername.BackColor = backColor;
            tbPassword.BackColor = backColor;
            tbUsername.ForeColor = foreColor;
            tbPassword.ForeColor = foreColor;
            tbUsername.BorderStyle = BorderStyle.Fixed3D;
            btnLogin.BackColor = backColor;
            btnLogin.ForeColor = foreColor;
        }

        private void tbPassword_Enter(object sender, EventArgs e)
        {
            
            tbPassword.Text = "";
        }

        private void tbUsername_Enter(object sender, EventArgs e)
        {
            lbUsername.Visible = true;
            tbUsername.Text = "";
        }

        

        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.BackColor = foreColor;
            btnLogin.ForeColor = whiteColor;
            
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.BackColor = backColor;
            btnLogin.ForeColor = foreColor;
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            tbPassword.UseSystemPasswordChar = true;
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbRestoreDown_Click(object sender, EventArgs e)
        {
            
        }

        private void pbMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();
            this.Close();
        }
    }
}
