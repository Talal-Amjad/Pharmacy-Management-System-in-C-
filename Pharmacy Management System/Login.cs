using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;


namespace Pharmacy_Management_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
         
        }
        string ConString = "USER ID=PMS;password=123;DATA SOURCE=DESKTOP-JB2AOUD:1521/XE";
        private void LoginBack_Click(object sender, EventArgs e)
        {
            Welcome f1 = new Welcome();
            f1.Show();
            this.Hide();
        } 

        private void LoginBtn_Click(object sender, EventArgs e)
        {
           
            string cmd = "select * from logininfo where username='" + textBox1.Text + "' and password='" + textBox2.Text + "'";
            OracleDataAdapter adp = new OracleDataAdapter(cmd, ConString);
            DataTable dtable = new DataTable();
            adp.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                
                Stocks f1 = new Stocks();
                f1.Show(); 
                this.Hide();
            }
            else
            {
                MessageBox.Show("Un-Athorized Access");
            }
            
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
