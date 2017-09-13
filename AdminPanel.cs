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
    public partial class AdminPanel : Form
    {
        
        public AdminPanel()
        {
            InitializeComponent();
            
        }

        

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            
        }

       

        

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm4 = new PayForm();
            frm4.Show();
        }

        private void addEmployeeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new Enroll();
            frm.Show();
        }

        private void editEmployeeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm2 = new Edit_emp();
            frm2.Show();
        }

        private void deleteEmployeeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm3 = new Edit_emp();
            frm3.Show();
        }

        private void checkAttendanceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm4 = new Rep_Attend();
            frm4.Show();
        }

        private void markAttendanceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm5 = new Attendance();
            frm5.Show();
        }

        private void reportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form frm6 = new PayForm();
            frm6.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();

            Form log = new Login();
            log.Show();
        }

        private void leaveRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form lea = new leave();
            lea.Show();
        }



       

        
    }
     
}
