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
    public partial class Pharmacist : Form
    {
        public Pharmacist()
        {
            InitializeComponent();
        }
        string ConString = "USER ID=PMS;password=123;DATA SOURCE=DESKTOP-JB2AOUD:1521/XE";
        private void Pharmacist_Stocks_Click(object sender, EventArgs e)
        {
            Stocks f1 = new Stocks();
            f1.Show();
            this.Hide();
        }

        private void Pharmacist_Bills_Click(object sender, EventArgs e)
        {
            Bills f1 = new Bills();
            f1.Show();
            this.Hide();
        }

        private void Pharmacist_DSales_Click(object sender, EventArgs e)
        {
            DailySales f1 = new DailySales();
            f1.Show();
            this.Hide();
        }

        private void Pharmacist_Customers_Click(object sender, EventArgs e)
        {
            Customers f1 = new Customers();
            f1.Show();
            this.Hide();
        }

        private void Pharmacist_DashBoard_Click(object sender, EventArgs e)
        {
            DashBoard f1 = new DashBoard();
            f1.Show();
            this.Hide();
        }

        private void Pharmacist_LogOut_Click(object sender, EventArgs e)
        {
            Welcome f1 = new Welcome();
            f1.Show();
            this.Close();
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);
          con.Open();
            if (emp_name.Text == " " || emp_design.SelectedItem == null || emp_contact.Text == " " || emp_salary.Text == " " || emp_age.Text == " " || emp_address.Text == " ")
            {
                MessageBox.Show("Some Fields are Empty please Enter all fileds Carefully");
            }
            else
            {
                string query = "insert into EMPLOYEE (EMPNAME,DESIGN,CONTACTNUM,SALARY,AGE,ADDRESS) Values ('" + emp_name.Text + "','" + emp_design.SelectedItem + "','" + emp_contact.Text + "','" + emp_salary.Text + "','" + emp_age.Text + "','" + emp_address.Text + "')";
                OracleDataAdapter adp = new OracleDataAdapter(query, con);
                adp.SelectCommand.ExecuteNonQuery();
                MessageBox.Show("Record Saved Successfully");

            }

            con.Close();
            string query2 = "select * from EMPLOYEE";
            OracleDataAdapter adp2 = new OracleDataAdapter(query2, con);
            DataTable dt = new DataTable();
            adp2.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void Pharmacist_Load(object sender, EventArgs e)
        {

            OracleConnection con = new OracleConnection(ConString);
            string query = "select * from EMPLOYEE";
            OracleDataAdapter adp = new OracleDataAdapter(query, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);
            con.Open();

            string query = "select * from EMPLOYEE where EMPNAME='" + emp_name.Text + "'";
            if (emp_name.Text == "")
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

        private void Editbtn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);


            con.Open();
            string query = "Update EMPLOYEE set AGE='" +emp_age.Text + "',SALARY='" + emp_salary.Text + "',CONTACTNUM='"+emp_contact.Text+"',ADDRESS='"+emp_address.Text+"' where DESIGN ='" + emp_design.SelectedItem + "' AND  EMPName='" + emp_name.Text + "' ";
            if (emp_name.Text == " " || emp_design.SelectedItem == null || emp_contact.Text == " " || emp_salary.Text == " " || emp_age.Text == " " || emp_address.Text == " ")
            {
                MessageBox.Show("Some Values are missing please enter all values");
            }
            else
            {
                string query4 = "Select * from  EMPLOYEE  where DESIGN ='" + emp_design.SelectedItem + "' AND  EMPName='" + emp_name.Text + "'AND CONTACTNUM='" + emp_contact.Text + "' ";
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
                    MessageBox.Show("Recorded Updated Sussessfully");
                }
            }
            con.Close();


            string query2 = "select * from EMPLOYEE";
            OracleDataAdapter adp2 = new OracleDataAdapter(query2, con);
            DataTable dt = new DataTable();
            adp2.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(ConString);


            con.Open();
            string query = "DELETE from  EMPLOYEE  where DESIGN ='" + emp_design.SelectedItem + "' AND  EMPName='" + emp_name.Text + "' ";
            if (emp_name.Text == " " || emp_design.SelectedItem == null )
            {
                MessageBox.Show("Some Values are missing please enter all values");
            }
            else
            {
                string query4 = "Select * from  EMPLOYEE  where DESIGN ='" + emp_design.SelectedItem + "' AND  EMPName='" + emp_name.Text + "'AND CONTACTNUM='"+emp_contact.Text+"' ";
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
                    MessageBox.Show("Record Deleted Successfully.......");
                }
            }
            con.Close();


            string query2 = "select * from EMPLOYEE";
            OracleDataAdapter adp2 = new OracleDataAdapter(query2, con);
            DataTable dt = new DataTable();
            adp2.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgrow = dataGridView1.Rows[e.RowIndex];
                emp_name.Text = dgrow.Cells[0].Value.ToString();
                emp_design.SelectedItem = dgrow.Cells[1].Value.ToString();
                emp_contact.Text = dgrow.Cells[2].Value.ToString();
                emp_salary.Text = dgrow.Cells[3].Value.ToString();
                emp_age.Text = dgrow.Cells[4].Value.ToString();
                emp_address.Text = dgrow.Cells[5].Value.ToString();
            }
        }
    }
}
