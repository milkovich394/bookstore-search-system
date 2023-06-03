using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kr_MiSPISiT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthorsSearch authorsSearch = new AuthorsSearch();
            this.Hide();
            authorsSearch.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            WorkSearchcs workSearchcs = new WorkSearchcs();
            this.Hide();
            workSearchcs.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenresSearch genresSearch = new GenresSearch();
            this.Hide();
            genresSearch.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Log log = new Log();
            this.Hide();
            log.Show();
        }
    }
}
