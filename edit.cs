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
using ICSharpCode.TextEditor.Document;
using System.IO;

namespace bugreal
{
    public partial class edit : Form
    {
        SqlCeConnection mySqlConnection;
        
        public edit()
        {
            InitializeComponent();
            Connection();
            LoadSubjects();
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

        private void LoadSubjects()
        {
            //selects id from database and lists them in a combobox, so user can select
            using (SqlCeCommand cmd = new SqlCeCommand("SELECT id FROM bug", mySqlConnection))
            {
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cbid.Items.Add(dr["id"].ToString()); // adds list of id's to comboBox to select some code
                    }
                }
            }

        }
        private void Code()
        {
            int code = cbid.SelectedIndex + 1;
            // had to add plus one, as it was retriving id as the previous number, so if picked bug_id2, it would give me 1's code

            //selectes the code from the database, where = to what bug id selected
            string sqlquery = "SELECT code  FROM bug WHERE id = " + code;

            SqlCeCommand command = new SqlCeCommand(sqlquery, mySqlConnection);

            try
            {
                //reads the command and exports code from database to the textbox - txtCode
                SqlCeDataReader sdr = command.ExecuteReader();

                while (sdr.Read())
                {
                    txtcode.Text = (sdr["code"].ToString());
                   
                }

            }

            catch (SqlCeException ex)
            {

                MessageBox.Show("Failure with connection!");
            }



        }

        private void load_Click(object sender, EventArgs e)
        {
            if (cbid.SelectedIndex >= 0)
            {
                Code();//runs code method when button clicked
            }
            else
            {
                MessageBox.Show("Please select and ID then re-load the Code, Thanks!");
            }

        }

        private void update_Click(object sender, EventArgs e)
        {

            //when the update button is clicked, runs SQL update query and adds all new fields to the database
            SqlCeCommand cmd = new SqlCeCommand("UPDATE bug SET id = @id, code = @code, status = @status  WHERE id = @id", mySqlConnection);

            //adds the the text fields and combo fields to the query, so can add to database
            cmd.Parameters.AddWithValue("@id", cbid.Text);
            cmd.Parameters.AddWithValue("@code", txtcode.Text);
            cmd.Parameters.AddWithValue("@status", cbstatus.Text);
            cmd.ExecuteNonQuery();


            this.Close();
            
        }

        private void txtcode_Load(object sender, EventArgs e)
        {
            string dirc = Application.StartupPath;
            FileSyntaxModeProvider fsmp;
            if (Directory.Exists(dirc))
            {
                fsmp = new FileSyntaxModeProvider(dirc);
                HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmp);
                txtcode.SetHighlighting("C#");
            }
        }
    }
}
