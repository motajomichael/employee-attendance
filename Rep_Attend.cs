using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalpro
{
    public partial class Rep_Attend : Form
    {

        DatabaseConnection objConnect;
        string conString;
        DataSet ds;
        DataRow dRow;
        int MaxRows;
        int inc = 0;
        string a;

        public Rep_Attend()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;

            if (comboBox1.Text == "IT") 
            {
                a = "IT";
            }
            else if (comboBox1.Text == "ENG")
            {
                a = "ENG";
            }
            else if (comboBox1.Text == "BBA")
            {
                a = "BBA";
            }

            else a = "";

            if (comboBox2.Text == "All Present")
            {
                sql = "select * from tbl_Attend where Emp_Id in (select Emp_Id from tbl_Emp where Dept_Id in (select Dept_Id from tbl_Dept where Dept_Name = '"+comboBox1.Text+"')) and Attend_Date ='"+comboBox3.Text+ " "+comboBox4.Text+ " "+comboBox5.Text+"'";
                System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(sql, conString);
                DataSet ds = new DataSet();
                ds.Tables.Add("tbl_Attend");
                adapter.Fill(ds, "tbl_Attend");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "tbl_Attend";
            }
               
 
            
            else if (comboBox2.Text == "On Time")
            {
                sql = "select * from tbl_Attend where Emp_Id in (select Emp_Id from tbl_Emp where Dept_Id in (select Dept_Id from tbl_Dept where Dept_Name = '" + comboBox1.Text + "')) and Attend_Date ='" + comboBox3.Text + " " + comboBox4.Text + " " + comboBox5.Text + "' and Attend_Time like '07%' ";
                System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(sql, conString);
                DataSet ds = new DataSet();
                ds.Tables.Add("tbl_Attend");
                adapter.Fill(ds, "tbl_Attend");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "tbl_Attend";
 
            }
            else if (comboBox2.Text == "Late")
            {
                sql = "select * from tbl_Attend where Emp_Id in (select Emp_Id from tbl_Emp where Dept_Id in (select Dept_Id from tbl_Dept where Dept_Name = '" + comboBox1.Text + "')) and Attend_Date ='" + comboBox3.Text + " " + comboBox4.Text + " " + comboBox5.Text + "' and Attend_Time like '08%' ";
                System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(sql, conString);
                DataSet ds = new DataSet();
                ds.Tables.Add("tbl_Attend");
                adapter.Fill(ds, "tbl_Attend");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "tbl_Attend";
 
            }
            else if (comboBox2.Text == "Absent")
            {
                sql = "select Emp_Id from tbl_Emp where Emp_Id not in(select Emp_Id from tbl_Attend)";
                System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(sql, conString);
                DataSet ds = new DataSet();
                ds.Tables.Add("tbl_Emp");
                adapter.Fill(ds, "tbl_Emp");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "tbl_Emp";
            }
            

           
        }

        private void Rep_Attend_Load(object sender, EventArgs e)
        {
            try
            {
                objConnect = new DatabaseConnection();
                conString = Properties.Settings.Default.FinalProConnectionString;

                objConnect.connection_string = conString;
                objConnect.Sql = Properties.Settings.Default.SQL;

                ds = objConnect.GetConnection;
                MaxRows = ds.Tables[0].Rows.Count;

                // NavigateRecords();

                

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
