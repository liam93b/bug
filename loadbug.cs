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
    public partial class loadbug : Form
    {

        DataTable dt;
        SqlCeConnection mySqlConnection;

        public loadbug()
        {
            InitializeComponent();
            Connection();
        }

        private void loadbug_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
        private void Connection()
        {
            mySqlConnection = new SqlCeConnection(@"Data Source=C:\temp\Mydatabase#1.sdf ");

            String selcmd = "SELECT id, code, status FROM bug ORDER BY id";

            SqlCeCommand mySqlCommand = new SqlCeCommand(selcmd, mySqlConnection);

            try
            {
                mySqlConnection.Open();

                SqlCeDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                listView1.Items.Clear();

                while (mySqlDataReader.Read())
                {
                    // adds everything from database into a listview table, with sub boxes to space into columns
                    ListViewItem item1 = new ListViewItem(mySqlDataReader["id"] + ":");
                    item1.SubItems.Add(mySqlDataReader["code"] + "");
                    item1.SubItems.Add(mySqlDataReader["status"] + "");
                   


                    listView1.Items.AddRange(new ListViewItem[] { item1 });


                }

            }

            catch (SqlCeException ex)
            {

                MessageBox.Show("Failure" + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            edit ed = new edit();
            ed.Show();

        }
    }
}
