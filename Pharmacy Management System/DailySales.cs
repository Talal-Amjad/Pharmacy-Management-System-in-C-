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
    public partial class DailySales : Form
    {
        public DailySales()
        {
            InitializeComponent();
        }
        
        string ConString = "USER ID=PMS;password=123;DATA SOURCE=DESKTOP-JB2AOUD:1521/XE";
        private void DS_Stocks_Click(object sender, EventArgs e)
        {
            Stocks f1 = new Stocks();
            f1.Show();
            this.Hide();
        }

        private void DS_Bills_Click(object sender, EventArgs e)
        {
            Bills f1 = new Bills();
            f1.Show();
            this.Hide();
        }

        private void DS_Customers_Click(object sender, EventArgs e)
        {
            Customers f1 = new Customers();
            f1.Show();
            this.Hide();
        }

        private void DS_Pharmacist_Click(object sender, EventArgs e)
        {
            Pharmacist f1 = new Pharmacist();
            f1.Show();
            this.Hide();
        }

        private void DS_DBoard_Click(object sender, EventArgs e)
        {

            DashBoard f1 = new DashBoard();
            f1.Show();
            this.Hide();
        }

        private void DS_LogOut_Click(object sender, EventArgs e)
        {
            Welcome f1 = new Welcome();
            f1.Show();
            this.Close();
        }

        private void DailySales_Load(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);
            string query = "select * from DSALES";
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
                DS_Name.Text = dgrow.Cells[0].Value.ToString();
                Ds_type.SelectedItem = dgrow.Cells[1].Value.ToString();
                ds_quantity.Text = dgrow.Cells[2].Value.ToString();
                DS_Customer.Text = dgrow.Cells[4].Value.ToString();
                ds_contact.Text = dgrow.Cells[5].Value.ToString();
                Ds_price.Text= dgrow.Cells[3].Value.ToString();
                //txt_price.Text = dgrow.Cells[3].Value.ToString();
            }
        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);
            con.Open();

            string query = "select * from DSALES where MEDICINE_NAME='" + DS_Name.Text + "'";
            if (DS_Name.Text == "")
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

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);


            con.Open();

            string query = "Delete From DSALES where Medicine_NAME='" + DS_Name.Text + "' AND Medicine_type='" + Ds_type.SelectedItem + "'AND Customer ='"+DS_Customer.Text+"'" ;
            if (DS_Name.Text == " " || Ds_type.SelectedItem == null || DS_Customer.Text==" ")
            {
                MessageBox.Show("Some Values are missing please Enter all values");
            }

            else
            {
                string query4 = "select * from STOCK where MED_NAME='" + DS_Name.Text + "' AND Med_type='" + Ds_type.SelectedItem + "' ";
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
                    MessageBox.Show("Record Deleted");
                }
            }
            string query3 = "select * from DSALES";
            OracleDataAdapter adp2 = new OracleDataAdapter(query3, con);
            DataTable dt = new DataTable();
            adp2.Fill(dt);
            dataGridView1.DataSource = dt;
          
            con.Close();
        }

        private void Edit_btn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);


            con.Open();
            string query = "Update DSALES set Medicine_Quantity='" + ds_quantity.Text + "',Medicine_price='" + Ds_price.Text + "' where Medicine_Type ='" + Ds_type.SelectedItem + "' AND  Medicine_Name='" + DS_Name.Text + "' AND CUSTOMER = '"+DS_Customer.Text+"' ";
            if (Ds_price.Text == ""|| Ds_type.SelectedItem == null || ds_quantity.Text == "" || DS_Customer.Text == "" ||DS_Name.Text==""||ds_contact.Text=="")
            {
                MessageBox.Show("Some Values are missing please enter all values");
            }
            else
            {
                string query4 = "select * from STOCK where MED_NAME='" + DS_Name.Text + "' AND Med_type='" + Ds_type.SelectedItem + "' ";
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
                    string query2 = "Update COUSTOMER set PaYMENT='" + Ds_price.Text + "' where C_Name = '" + DS_Customer.Text + "' AND MEDICINE = '" + DS_Name.Text + "'";
                    OracleDataAdapter adp2 = new OracleDataAdapter(query2, con);
                    adp2.SelectCommand.ExecuteNonQuery();
                    MessageBox.Show("Recorded Updated Sussessfully");
                }
            }
            string query3 = "select * from DSALES";
            OracleDataAdapter adp3 = new OracleDataAdapter(query3, con);
            DataTable dt = new DataTable();
            adp3.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }
    }
}
