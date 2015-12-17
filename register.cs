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
    public partial class register : Form
    {
        SqlCeConnection mySqlConnection;
        public register()
        {
            InitializeComponent();
            Connection();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void Connection()
        {
            mySqlConnection = new SqlCeConnection(@"Data Source=C:\temp\Mydatabase#1.sdf ");

            String selcmd = "SELECT * FROM users ORDER BY username";

            SqlCeCommand mySqlCommand = new SqlCeCommand(selcmd, mySqlConnection);

            try
            {
                mySqlConnection.Open();

                SqlCeDataReader mySqlDataReader = mySqlCommand.ExecuteReader();


            }

            catch (SqlCeException ex)
            {

                MessageBox.Show("Failure" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool checkInputs()
        {
            bool rtnvalue = true;

            if (string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Error: Please check your inputs");
                rtnvalue = false;
            }

            return (rtnvalue);

        }

        public void insertRecord(String user, String pass, String commandString)
        {

            try
            {
                SqlCeCommand cmdInsert = new SqlCeCommand(commandString, mySqlConnection);

                cmdInsert.Parameters.AddWithValue("@user", user);
                cmdInsert.Parameters.AddWithValue("@pass", pass);
                cmdInsert.ExecuteNonQuery();
                MessageBox.Show("Succesfully you have registered you can now sign in!");
            }
            catch (SqlCeException ex)
            {
                MessageBox.Show(user + " error duplicate username" );
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkInputs())
            {

                String commandString = "INSERT INTO users(username, password) VALUES (@user, @pass)";

                insertRecord(textBox1.Text, textBox2.Text, commandString);
                Connection();
               
            }
        }
    }
}


 