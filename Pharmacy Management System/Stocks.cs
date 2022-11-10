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
    public partial class Stocks : Form
    {
        public Stocks()
        {
            InitializeComponent();
        }
        string ConString = "USER ID=PMS;password=123;DATA SOURCE=DESKTOP-JB2AOUD:1521/XE";
        private void Stocks_Bills_Click(object sender, EventArgs e)
        {
            Bills f1 = new Bills();
            f1.Show();
            this.Hide();
        }

        private void Stocks_DSales_Click(object sender, EventArgs e)
        {
            DailySales f1 = new DailySales();
            f1.Show();
            this.Hide();
        }

        private void Stocks_Customers_Click(object sender, EventArgs e)
        {
            Customers f1 = new Customers();
            f1.Show();
            this.Hide();
        }

        private void Stocks_Pharmacist_Click(object sender, EventArgs e)
        {
            Pharmacist f1 = new Pharmacist();
            f1.Show();
            this.Hide();
        }

        private void Stocks_DBoard_Click(object sender, EventArgs e)
        {
            DashBoard f1 = new DashBoard();
            f1.Show();
            this.Hide();
        }

        private void Stocks_LogOut_Click(object sender, EventArgs e)
        {
            Welcome f1 = new Welcome();
            f1.Show();
            this.Close();
        }

        private void Add_Button_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);
           
               
                con.Open();
            
            string query = "select * from STOCK where MED_NAME='" + txt_name.Text + "' AND MED_Type ='"+combo_type.SelectedItem+"'";
            if (txt_name.Text == "" || combo_type.SelectedItem == null || txt_quantity.Text == "" || txt_price.Text == "")
            {
                MessageBox.Show("Some Values are missing please insert all values");
            }
            else
            {
                OracleDataAdapter adp = new OracleDataAdapter(query, con);
                DataTable dt2 = new DataTable();
                adp.Fill(dt2);
                string query4 = "Update STOCK SET QUANTITY=Quantity+'" + txt_quantity.Text + "',Price=Price+'" + txt_price.Text + "' where Med_Name='"+txt_name.Text+"' AND Med_Type='"+combo_type.SelectedItem+"'" ;
                OracleDataAdapter adp4 = new OracleDataAdapter(query4, con);
                adp4.SelectCommand.ExecuteNonQuery();
               
                if (dt2.Rows.Count == 0)
                {
                    string query3 = "insert into STOCK (MED_NAme,MED_TYPE,QUANTITY,PRICE) Values ('" + txt_name.Text + "','" + combo_type.SelectedItem + "','" + txt_quantity.Text + "','" + txt_price.Text + "')";
                    OracleDataAdapter adp3 = new OracleDataAdapter(query3, con);
                    adp3.SelectCommand.ExecuteNonQuery();
                    MessageBox.Show("Record Saved Successfully");
                }
                else
                {
                    MessageBox.Show("Record Saved Successfully");
                }
            }
           
             
             con.Close();
            string query2 = "select * from STOCK";
            OracleDataAdapter adp2 = new OracleDataAdapter(query2, con);
            DataTable dt = new DataTable();
            adp2.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void Stocks_Load(object sender, EventArgs e)
        {

            OracleConnection con = new OracleConnection(ConString);
            string query = "select * from STOCK";
            OracleDataAdapter adp = new OracleDataAdapter(query, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void Search_button_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);
            con.Open();

            string query = "select * from STOCK where MED_NAME='"+txt_name.Text+"'";
            if (txt_name.Text == "")
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex !=-1)
            {
                DataGridViewRow dgrow = dataGridView1.Rows[e.RowIndex];
                txt_name.Text = dgrow.Cells[0].Value.ToString();
                combo_type.SelectedItem= dgrow.Cells[1].Value.ToString();
                txt_quantity.Text= dgrow.Cells[2].Value.ToString();
                txt_price.Text= dgrow.Cells[3].Value.ToString();
            }
        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);
             con.Open();
            string query3 = "select * from STOCK where MED_NAME='" + txt_name.Text + "' AND Med_type='" + combo_type.SelectedItem + "' ";
            string query = "Delete From STOCK where Med_NAME='"+txt_name.Text+"' AND Med_type='"+combo_type.SelectedItem+"'";
            if (txt_name.Text == "" || combo_type.SelectedItem == null )
            {
                MessageBox.Show("Some Values are missing please Enter all values");
            }
            
           
            else
            {
                OracleDataAdapter adp3 = new OracleDataAdapter(query3, con);
                DataTable dt2 = new DataTable();
                adp3.Fill(dt2);


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
            con.Close();


            string query2 = "select * from STOCK";
            OracleDataAdapter adp2 = new OracleDataAdapter(query2, con);
            DataTable dt = new DataTable();
            adp2.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void Edit_btn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);


            con.Open();
            string query3 = "select * from STOCK where MED_NAME='" + txt_name.Text + "' AND Med_type='" + combo_type.SelectedItem + "' ";
            string query = "Update STOCK set Quantity='" + txt_quantity.Text+"',price='"+txt_price.Text+ "' where Med_type ='" + combo_type.SelectedItem + "' AND  Med_Name='" + txt_name.Text + "' ";
            if (txt_name.Text == "" || combo_type.SelectedItem == null || txt_quantity.Text == "" || txt_price.Text == "")
            {
                MessageBox.Show("Some Values are missing please enter all values");
            }
            else
            {
                OracleDataAdapter adp3 = new OracleDataAdapter(query3, con);
                DataTable dt2 = new DataTable();
                adp3.Fill(dt2);


                if (dt2.Rows.Count == 0)
                {
                    MessageBox.Show("Record Not Found");
                }
                else
                {
                    OracleDataAdapter adp = new OracleDataAdapter(query, con);

                    adp.SelectCommand.ExecuteNonQuery();
                    MessageBox.Show("Recorded Updated Sussessfully");
                }
            }
            con.Close();


            string query2 = "select * from STOCK";
            OracleDataAdapter adp2 = new OracleDataAdapter(query2, con);
            DataTable dt = new DataTable();
            adp2.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void Stocks_Bills_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
