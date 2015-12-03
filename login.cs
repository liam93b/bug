using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace bugreal
{
    public partial class login : Form
    {

        public login()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" & textBox2.Text != "")
            {
                string queryText = @"SELECT Count(*) FROM users 
                             WHERE username = @user_name AND password = @password";
                using (SqlCeConnection cn = new SqlCeConnection(@"Data Source=C:\temp\Mydatabase#1.sdf "))
                using (SqlCeCommand cmd = new SqlCeCommand(queryText, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@user_name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@password", textBox2.Text);
                    int result = (int)cmd.ExecuteScalar();
                    if (result > 0)
                    {
                        this.Hide();
                        Form1 frmchild = new Form1();
                        frmchild.Show();
                    }
                    else
                    {
                        MessageBox.Show("User Not Found!");
                    }

                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            register reg = new register();
            reg.Show();
        }
    }
}
