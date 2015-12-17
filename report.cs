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
    public partial class report : Form

    {
        SqlCeConnection mySqlConnection;

        public report()

        {
            InitializeComponent();
            Connection();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Connection()
        {
            mySqlConnection = new SqlCeConnection(@"Data Source=C:\temp\Mydatabase#1.sdf ");

            String selcmd = "SELECT * FROM bug ORDER BY id";

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
        public void insertRecord(String ID, String code, String status,  String commandString)
        {

            try
            {
                SqlCeCommand cmdInsert = new SqlCeCommand(commandString, mySqlConnection);

                //adds paprameters to textboxs to link with sql
                cmdInsert.Parameters.AddWithValue("@ID", ID);
                cmdInsert.Parameters.AddWithValue("@code", code);
                cmdInsert.Parameters.AddWithValue("@status", status);
                cmdInsert.ExecuteNonQuery();
                MessageBox.Show("Succesfully reported your bug to the database!");
            }
            catch (SqlCeException ex)
            {
                MessageBox.Show(ID + " .." + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

         public bool checkInputs()
    	{
        	bool rtnvalue = true;
        	//checks if empty
       	if (string.IsNullOrEmpty(txtid.Text)||
            string.IsNullOrEmpty(txtcode.Text) ||
            string.IsNullOrEmpty(cbstatus.Text)) 
        	{
            	MessageBox.Show("Error: Please check your inputs");
            	rtnvalue = false;
        	}
 
        	return (rtnvalue);
 
    	}
 

        private void submit_Click(object sender, EventArgs e)
        {
            if (checkInputs())
            {
                //writes an insert sql statement  to add all fields to databse
                String commandString = "INSERT INTO bug(id, code, status) VALUES (@ID, @code, @status)";

                insertRecord(txtid.Text, txtcode.Text, cbstatus.Text, commandString);
                Connection();
                

            }
        }

    }
}
