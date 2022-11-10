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
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }
        string ConString = "USER ID=PMS;password=123;DATA SOURCE=DESKTOP-JB2AOUD:1521/XE";
        private void DB_Stocks_Click(object sender, EventArgs e)
        {
            Stocks f1 = new Stocks();
            f1.Show();
            this.Hide();
        }

        private void DB_Bills_Click(object sender, EventArgs e)
        {
            Bills f1 = new Bills();
            f1.Show();
            this.Hide();
        }

        private void DB_DSales_Click(object sender, EventArgs e)
        {
            DailySales f1 = new DailySales();
            f1.Show();
            this.Hide();
        }

        private void DB_Customers_Click(object sender, EventArgs e)
        {
            Customers f1 = new Customers();
            f1.Show();
            this.Hide();
        }

        private void DB_Pharmacist_Click(object sender, EventArgs e)
        {
            Pharmacist f1 = new Pharmacist();
            f1.Show();
            this.Hide();
        }

        private void DB_LogOut_Click(object sender, EventArgs e)
        {
            Welcome f1 = new Welcome();
            f1.Show();
            this.Close();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {

            OracleConnection con = new OracleConnection(ConString);
            con.Open();
            //to display to number of medicine in Dash Board.
            string query = "Select SUM(Quantity) from STOCK ";
            OracleCommand cmd = new OracleCommand(query, con);
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DB_stock.Text = dr.GetValue(0).ToString();
            }
            con.Close();
            //to display to Sales in RS from COUSTOMER Table.
            con.Open();
            string query2 = "Select SUM(PAYMENT) from COUSTOMER ";
            OracleCommand cmd2 = new OracleCommand(query2, con);
            OracleDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                DB_SALES.Text = dr2.GetValue(0).ToString();
            }
            con.Close();
            //To Display Total Number of Coustomers
            con.Open();
            string query3 = "Select count(C_Name) from COUSTOMER ";
            OracleCommand cmd3 = new OracleCommand(query3, con);
            OracleDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                DB_customer.Text = dr3.GetValue(0).ToString();
            }
            con.Close();
            // to display to total number of customers 
            con.Open();
            string query4 = "Select count(EMPNAME) from EMPLOYEE ";
            OracleCommand cmd4 = new OracleCommand(query4, con);
            OracleDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
                DB_EMPLOYEE.Text = dr4.GetValue(0).ToString();
            }
            con.Close();
        }

        private void DB_customer_Click(object sender, EventArgs e)
        {

        }
    }
}
