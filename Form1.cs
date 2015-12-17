using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bugreal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadBugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadbug frmchild = new loadbug();
            frmchild.MdiParent = this;
            frmchild.Show();

        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
                
        }

        private void saveBugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report frmchild = new report();
            frmchild.MdiParent = this;
            frmchild.Show();

        }
    }
}
