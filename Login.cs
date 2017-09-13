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
    

    public partial class Login : Form
    {
       

        public Login()
        {
            InitializeComponent();
            

        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            
            if ((textBox1.Text == "root") && (textBox2.Text == "pass"))
            {
                Form frm = new Panel();
                frm.Show();

                
                textBox2.Text = "";

                Visible = false;

            }
            
            else
            { 
                MessageBox.Show("Incorrect Username/Paswword");

                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();


            }

            
            


        }

        

    }
     
}
