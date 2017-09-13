using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalpro
{
    public partial class PayForm : Form
    {

        public PayForm()
        {
            InitializeComponent();
            
        }

        private void PayForm_Load(object sender, EventArgs e)
        {
            
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = null;

             if (comboBox1.Text == "IT")
                {
                    sql = "select Emp_Id, Emp_Name from tbl_Emp where Dept_Id ='D1'";
                }
                else if (comboBox1.Text == "ENG")
                {
                    sql = "select Emp_Id, Emp_Name from tbl_Emp where Dept_Id ='D2'";
                }
                else if (comboBox1.Text == "BBA")
                {
                    sql = "select Emp_Id, Emp_Name from tbl_Emp where Dept_Id ='D3'";
                }

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Resources\FinalPro.mdf;Integrated Security=True");
                conn.Open();
                System.Data.SqlClient.SqlCommand sc = new System.Data.SqlClient.SqlCommand(sql, conn);
                System.Data.SqlClient.SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Emp_Id", typeof(string));
                dt.Columns.Add("Emp_Name", typeof(string));
                dt.Load(reader);

                comboBox2.ValueMember = "Emp_Id";
                comboBox2.DisplayMember = "Emp_Name";
                comboBox2.DataSource = dt;

                conn.Close();
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql1 = null;

            sql1 = "select Emp_Id, Emp_Name, Dept_Id, Emp_Cont, WPD, DOB from tbl_Emp where Emp_Name = '" + comboBox2.Text + "'";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Resources\FinalPro.mdf;Integrated Security=True");
            conn.Open();
            System.Data.SqlClient.SqlCommand sc = new System.Data.SqlClient.SqlCommand(sql1, conn);
            System.Data.SqlClient.SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Emp_Id", typeof(string));
            dt.Columns.Add("Emp_Name", typeof(string));
            dt.Columns.Add("Dept_Id", typeof(string));
            dt.Columns.Add("Emp_Cont", typeof(string));
            dt.Columns.Add("WPD", typeof(string));
            dt.Columns.Add("DOB", typeof(string));
            dt.Load(reader);

            comboBox3.ValueMember = "Emp_Id";
            comboBox3.DisplayMember = "Emp_Name";
            comboBox3.DataSource = dt;

            comboBox4.ValueMember = "Emp_Id";
            comboBox4.DisplayMember = "DOB";
            comboBox4.DataSource = dt;

            comboBox5.ValueMember = "Emp_Id";
            comboBox5.DisplayMember = "Dept_Id";
            comboBox5.DataSource = dt;

            comboBox6.ValueMember = "Emp_Id";
            comboBox6.DisplayMember = "Emp_Cont";
            comboBox6.DataSource = dt;

            comboBox10.ValueMember = "Emp_Id";
            comboBox10.DisplayMember = "WPD";
            comboBox10.DataSource = dt;

            comboBox14.ValueMember = "Emp_Id";
            comboBox14.DisplayMember = "Emp_Id";
            comboBox14.DataSource = dt;

            conn.Close();

            int result = 0;

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Resources\FinalPro.mdf;Integrated Security=True");
            connection.Open();
            System.Data.SqlClient.SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select count (Emp_Id) from tbl_Attend where Attend_Date like '%"+comboBox12.Text+"%' and Emp_Id= '" + comboBox14.Text + "'";

            result = ((int)cmd.ExecuteScalar());
            connection.Close();

            comboBox7.Text = result.ToString();

            int early = 0;
            connection.Open();
            System.Data.SqlClient.SqlCommand cmd2 = connection.CreateCommand();
            cmd2.CommandText = "select count (Emp_Id) from tbl_Attend where Attend_Date like '%" + comboBox12.Text + "%' and Emp_Id= '" + comboBox14.Text + "' and Attend_Time like '07%'";

            early = ((int)cmd2.ExecuteScalar());
            connection.Close();

            comboBox8.Text = early.ToString();

            int late = 0;
            connection.Open();
            System.Data.SqlClient.SqlCommand cmd3 = connection.CreateCommand();
            cmd3.CommandText = "select count (Emp_Id) from tbl_Attend where Attend_Date like '%" + comboBox12.Text + "%' and Emp_Id= '" + comboBox14.Text + "' and Attend_Time like '08%'";

            late = ((int)cmd3.ExecuteScalar());
            connection.Close();

            comboBox9.Text = late.ToString();

            int later = late / 4;

            int pay = 0;
            int a = 0;
            int b = 0;
            a = Convert.ToInt32(comboBox7.Text);
            b = Convert.ToInt32(comboBox10.Text);
            pay = a * b;

            comboBox11.Text = pay.ToString();


        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //label8.Text= comboBox3.Text  ;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
           // label14.Text = comboBox4.Text;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //label15.Text = comboBox5.Text;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
           // label16.Text = comboBox6.Text;
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
            {
            }

            else
            {

                
                 //string sql2 = null;
                 int result = 0;
                 //sql2 = "select count (tbl_Attend) where Emp_Id = EMP11";

                 //System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Resources\FinalPro.mdf;Integrated Security=True");
                 //conn.Open();
                 //System.Data.SqlClient.SqlCommand sc = new System.Data.SqlClient.SqlCommand(sql2, conn);
                 //System.Data.SqlClient.SqlDataReader reader;
                 System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Resources\FinalPro.mdf;Integrated Security=True");
                 connection.Open();
                 System.Data.SqlClient.SqlCommand cmd = connection.CreateCommand();
                 cmd.CommandText = "select count (Emp_Id) from tbl_Attend where Attend_Date like '%" + comboBox12.Text + "%' and Emp_Id= '" + comboBox14.Text + "'";

                 result = ((int)cmd.ExecuteScalar());
                 connection.Close();

                 comboBox7.Text = result.ToString();

                 int early = 0;
                 connection.Open();
                 System.Data.SqlClient.SqlCommand cmd2 = connection.CreateCommand();
                 cmd2.CommandText = "select count (Emp_Id) from tbl_Attend where Attend_Date like '%" + comboBox12.Text + "%' and Emp_Id= '" + comboBox14.Text + "' and Attend_Time like '07%'";

                 early = ((int)cmd2.ExecuteScalar());
                 connection.Close();

                 comboBox8.Text = early.ToString();

                 int late = 0;
                 connection.Open();
                 System.Data.SqlClient.SqlCommand cmd3 = connection.CreateCommand();
                 cmd3.CommandText = "select count (Emp_Id) from tbl_Attend where Attend_Date like '%" + comboBox12.Text + "%' and Emp_Id= '" + comboBox14.Text + "' and Attend_Time like '08%'";

                 late = ((int)cmd3.ExecuteScalar());
                 connection.Close();

                 comboBox9.Text = late.ToString();


                 int later = late / 4;

                 int pay = 0;
                 int a = 0;
                 int b = 0;
                 a = Convert.ToInt32(comboBox7.Text);
                 b = Convert.ToInt32(comboBox10.Text);
                 pay = a * b;

                 comboBox11.Text = pay.ToString();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog PrintDialog1 = new PrintPreviewDialog();
            PrintDialog1.ShowDialog();
        }
    }
}
