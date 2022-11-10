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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
        }
        string ConString = "USER ID=PMS;password=123;DATA SOURCE=DESKTOP-JB2AOUD:1521/XE";
        private void Customers_Stocks_Click(object sender, EventArgs e)
        {
            Stocks f1 = new Stocks();
            f1.Show();
            this.Hide();
        }

        private void Customers_Bills_Click(object sender, EventArgs e)
        {
            Bills f1 = new Bills();
            f1.Show();
            this.Hide();
        }

        private void Customers_DSales_Click(object sender, EventArgs e)
        {
            DailySales f1 = new DailySales();
            f1.Show();
            this.Hide();
        }

        private void Customers_Pharmacist_Click(object sender, EventArgs e)
        {
            Pharmacist f1 = new Pharmacist();
            f1.Show();
            this.Hide();
        }

        private void Customers_DBoard_Click(object sender, EventArgs e)
        {
            DashBoard f1 = new DashBoard();
            f1.Show();
            this.Hide();
        }

        private void Customers_LogOut_Click(object sender, EventArgs e)
        {
            Welcome f1 = new Welcome();
            f1.Show();
            this.Close();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);
            con.Open();

            string query = "select * from COUSTOMER where C_Name='" + C_Name.Text + "'";
            if (C_Name.Text == "")
            {
                MessageBox.Show("Please Enter the Name of medicine to See its details");
            }
            else
            {
                OracleDataAdapter adp = new OracleDataAdapter(query, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Record Not Found");
                }
            }

            con.Close();
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);
            string query = "select * from Coustomer";
            OracleDataAdapter adp = new OracleDataAdapter(query, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgrow = dataGridView1.Rows[e.RowIndex];
                C_Name.Text = dgrow.Cells[0].Value.ToString();
                C_Contact.Text = dgrow.Cells[1].Value.ToString();
                C_Medicine.Text = dgrow.Cells[2].Value.ToString();
                C_bill.Text = dgrow.Cells[3].Value.ToString();

                //txt_price.Text = dgrow.Cells[3].Value.ToString();
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);


            con.Open();
            string query = "Update DSALES set Medicine_Price='" + C_bill.Text + "',CONTACT='"+C_Contact.Text+"' where  Medicine_Name='" + C_Medicine.Text + "' AND CUSTOMER = '" + C_Name.Text + "' ";
            if (C_Medicine.Text == "" || C_Name.Text == "" || C_Contact.Text == "" || C_bill.Text == "")
            {
                MessageBox.Show("Some Values are missing please enter all values");
            }
            else
            {
                string query4 = "select * from Coustomer where C_Name='" + C_Name.Text + "' AND Medicine='" + C_Medicine.Text + "' ";
                OracleDataAdapter adp4 = new OracleDataAdapter(query4, con);
                DataTable dt2 = new DataTable();
                adp4.Fill(dt2);


                if (dt2.Rows.Count == 0)
                {
                    MessageBox.Show("Record Not Found");
                }
                else
                {
                    OracleDataAdapter adp = new OracleDataAdapter(query, con);
                    adp.SelectCommand.ExecuteNonQuery();
                    string query2 = "Update COUSTOMER set CONTACTNO='" + C_Contact.Text + "',PAYMENT='" + C_bill.Text + "' where C_Name = '" + C_Name.Text + "' AND MEDICINE = '" + C_Medicine.Text + "'";
                    OracleDataAdapter adp2 = new OracleDataAdapter(query2, con);
                    adp2.SelectCommand.ExecuteNonQuery();
                    MessageBox.Show("Recorded Updated Sussessfully");
                }
            }
            string query3 = "select * from COUSTOMER";
            OracleDataAdapter adp3 = new OracleDataAdapter(query3, con);
            DataTable dt = new DataTable();
            adp3.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);


            con.Open();
            string query2 = "Delete From DSALES where CONTACT='" + C_Contact.Text + "' AND  Medicine_Name='" + C_Medicine.Text + "' AND CUSTOMER = '" + C_Name.Text + "' ";
            string query = "Delete From COUSTOMER where C_NAME='" + C_Name.Text + "' AND Medicine='" +C_Medicine.Text+ "'AND ContactNO ='" + C_Contact.Text + "'";
            if (C_Name.Text == " " || C_Medicine.Text ==" "  || C_Contact.Text == " "||C_bill.Text=="")
            {
                MessageBox.Show("Some Values are missing please Enter all values");
            }

            else
            {
                string query4 = "select * from Coustomer where C_Name='" + C_Name.Text + "' AND Medicine='" + C_Medicine.Text + "' ";
                OracleDataAdapter adp4 = new OracleDataAdapter(query4, con);
                DataTable dt2 = new DataTable();
                adp4.Fill(dt2);


                if (dt2.Rows.Count == 0)
                {
                    MessageBox.Show("Record Not Found");
                }
                else
                {
                    OracleDataAdapter adp = new OracleDataAdapter(query, con);
                    adp.SelectCommand.ExecuteNonQuery();
                    OracleDataAdapter adp2 = new OracleDataAdapter(query2, con);
                    adp2.SelectCommand.ExecuteNonQuery();
                    MessageBox.Show("Record Deleted");
                }
            }
            string query3 = "select * from COUSTOMER";
            OracleDataAdapter adp3 = new OracleDataAdapter(query3, con);
            DataTable dt = new DataTable();
            adp3.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }
    }
}
