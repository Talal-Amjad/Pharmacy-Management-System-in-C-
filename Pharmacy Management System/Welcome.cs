using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacy_Management_System
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void WelcomeAdminBtn_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            f1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
