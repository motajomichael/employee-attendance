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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        BindingSource bs;
        public Form2(BindingSource bs)
            : this()
        {
            this.bs = bs;
            this.textBox1.DataBindings.Add("Text", bs, "Id");
            this.textBox2.DataBindings.Add("Text", bs, "Emp_Id");
            this.textBox3.DataBindings.Add("Text", bs, "Emp_Name");
            this.textBox4.DataBindings.Add("Text", bs, "Enroll_Date");
            this.textBox5.DataBindings.Add("Text", bs, "Emp_Dept");
            this.textBox6.DataBindings.Add("Text", bs, "Emp_Cont");
            
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            bs.EndEdit();
        }
    }
}
