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
    public partial class Bills : Form
    {
        public Bills()
        {
            InitializeComponent();
        }
        string ConString = "USER ID=PMS;password=123;DATA SOURCE=DESKTOP-JB2AOUD:1521/XE";
        private void Bills_Stocks_Click(object sender, EventArgs e)
        {
            Stocks f1 = new Stocks();
            f1.Show();
            this.Hide();

        }

        private void Bills_DSales_Click(object sender, EventArgs e)
        {
            DailySales f1 = new DailySales();
            f1.Show();
            this.Hide();
        }

        private void Bills_Customers_Click(object sender, EventArgs e)
        {
            Customers f1 = new Customers();
            f1.Show();
            this.Hide();
        }

        private void Bills_Pharmacist_Click(object sender, EventArgs e)
        {
            Pharmacist f1 = new Pharmacist();
            f1.Show();
            this.Hide();
        }

        private void Bills_DBoard_Click(object sender, EventArgs e)
        {
            DashBoard f1 = new DashBoard();
            f1.Show();
            this.Hide();
        }

        private void Bills_LogOut_Click(object sender, EventArgs e)
        {
            Welcome f1 = new Welcome();
            f1.Show();
            this.Close();
        }

        private void AddBill_btn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);


            con.Open();
            string query3 = "select * from STOCK where MED_NAME='" + M_nametxt.Text + "' AND Med_type='" + m_type.SelectedItem + "' ";
            if (M_nametxt.Text == "" || m_quantity.Text == "" || m_price.Text == "" || m_type.SelectedItem == null || c_nametxt.Text == "" || c_contacttxt.Text == "")
            {
                MessageBox.Show("Some Values are missing please insert all values");
            }
            else
            {
                OracleDataAdapter adp3 = new OracleDataAdapter(query3, con);
                DataTable dt2 = new DataTable();
                adp3.Fill(dt2);


                if (dt2.Rows.Count == 0)
                {
                    MessageBox.Show("No Such Medicine Found");
                }
                else
                {
                    string query = "insert into DSALES (MEDICINE_NAME,MEDICINE_TYPE,MEDICINE_QUANTITY,MEDICINE_PRICE,CUSTOMER,CONTACT) Values ('" + M_nametxt.Text + "','" + m_type.SelectedItem + "','" + m_quantity.Text + "','" + m_price.Text + "','" + c_nametxt.Text + "','" + c_contacttxt.Text + "')";
                    OracleDataAdapter adp = new OracleDataAdapter(query, con);
                    adp.SelectCommand.ExecuteNonQuery();
                    string query1 = "insert into COUSTOMER (C_NAME,CONTACTNO,MEDICINE,PAYMENT) Values ( '" + c_nametxt.Text + "','" + c_contacttxt.Text + "','" + M_nametxt.Text + "','" + m_price.Text + "')";
                    OracleDataAdapter adp1 = new OracleDataAdapter(query1, con);

                    adp1.SelectCommand.ExecuteNonQuery();
                    MessageBox.Show("Data Saved Successfully");
                    string query2 = "Update STOCK set Quantity=Quantity - '" + m_quantity.Text + "' , Price = Price - '" + m_price.Text + "' where Med_type ='" + m_type.SelectedItem + "' AND  Med_Name='" + M_nametxt.Text + "' ";
                    OracleDataAdapter adp2 = new OracleDataAdapter(query2, con);
                    adp2.SelectCommand.ExecuteNonQuery();
                }
            }
            con.Close();



        }
    }
}
