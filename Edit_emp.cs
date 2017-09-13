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
    public partial class Edit_emp : Form
    {
        string a;
            

        DatabaseConnection objConnect;
        string conString;
        DataSet ds;
        DataRow dRow;
        int MaxRows;
        int inc = 0;
        
        BindingSource bs;
        System.Data.SqlClient.SqlDataAdapter sqladapter;
        DataTable dataTable;
        System.Data.SqlClient.SqlConnection connection;
        
        

        public Edit_emp()
        {
            InitializeComponent();
            if (comboBox1.Text == "IT")
            {
                a = "D1";
            }
            else if (comboBox1.Text == "ENG")
            {
                a = "D2";
            }
            else if (comboBox1.Text == "BBA")
            {
                a = "D3";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            


            if (comboBox1.Text == "All Dept")
            {
                sql = "select * from tbl_Emp";
            }

            else sql = "select * from tbl_Emp where Dept_Id in (select Dept_Id from tbl_Dept where Dept_Name = '"+ comboBox1.Text +"')";
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(sql, conString);
            DataSet ds = new DataSet();
            ds.Tables.Add("tbl_Emp");
            adapter.Fill(ds, "tbl_Emp");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "tbl_Emp";

        }


       /* private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            new Form2(bs).ShowDialog();

            sqladapter.UpdateCommand = new System.Data.SqlClient.SqlCommand(
                "UPDATE tb_enrol SET I, = @Id, Emp_Id = @Emp_Id, Emp_Name = @Emp_Name, Enroll_Date = @Enroll_Date, Emp_Dept = @Emp_Dept, Emp_Cont = @Emp_Cont " +
                "WHERE Id = @oldId", connection);

            System.Data.SqlClient.SqlParameter param0 = new System.Data.SqlClient.SqlParameter("Id", dataTable.Rows[bs.Position]["Id"]);
            sqladapter.UpdateCommand.Parameters.Add(param0);
            System.Data.SqlClient.SqlParameter param1 = new System.Data.SqlClient.SqlParameter("Emp_Id", dataTable.Rows[bs.Position]["Emp_Id"]);
            sqladapter.UpdateCommand.Parameters.Add(param1);
            System.Data.SqlClient.SqlParameter param2 = new System.Data.SqlClient.SqlParameter("Emp_Name", dataTable.Rows[bs.Position]["Emp_Name"]);
            sqladapter.UpdateCommand.Parameters.Add(param2);
            System.Data.SqlClient.SqlParameter param3 = new System.Data.SqlClient.SqlParameter("Enroll_Date", dataTable.Rows[bs.Position]["Enroll_Date"]);
            sqladapter.UpdateCommand.Parameters.Add(param3);
            System.Data.SqlClient.SqlParameter param4 = new System.Data.SqlClient.SqlParameter("Emp_Dept", dataTable.Rows[bs.Position]["Emp_Dept"]);
            sqladapter.UpdateCommand.Parameters.Add(param4);
            System.Data.SqlClient.SqlParameter param5 = new System.Data.SqlClient.SqlParameter("Emp_Cont", dataTable.Rows[bs.Position]["Emp_Cont"]);
            sqladapter.UpdateCommand.Parameters.Add(param5);
            System.Data.SqlClient.SqlParameter param6 = new System.Data.SqlClient.SqlParameter("oldId", dataTable.Rows[bs.Position]["Id"]);
            sqladapter.UpdateCommand.Parameters.Add(param6);

            sqladapter.Update(dataTable);
        }*/

        private void Edit_emp_Load(object sender, EventArgs e)
        {
            try
            {
                objConnect = new DatabaseConnection();
                conString = Properties.Settings.Default.FinalProConnectionString;

                objConnect.connection_string = conString;
                objConnect.Sql = Properties.Settings.Default.SQL;

                ds = objConnect.GetConnection;
                MaxRows = ds.Tables[0].Rows.Count;

                //NavigateRecords();

                

            }
            catch (Exception err)
            {
                //MessageBox.Show(err.Message);
            }
        }

        
    }
}
