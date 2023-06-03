using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace kr_MiSPISiT
{
    public partial class Log : Form
    {
        Basa db;
        DataTable dt;
        MySqlDataAdapter msda;
        MySqlCommand command;
        DataSet ds;

        string true_pass = "";
        string true_log = "";

        public Log()
        {
            InitializeComponent();

            db = new Basa();
            dt = new DataTable();
            msda = new MySqlDataAdapter();
            ds = new DataSet();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            db.OpenConnection();

            string sql = "SELECT login, password FROM kr_shops.users;";

            command = new MySqlCommand(sql, db.GetConnection());
            msda.SelectCommand = command;
            msda.Fill(dt);

            MySqlDataReader reader = command.ExecuteReader();

           /* while (reader.Read())
            {
                true_log = reader[0].ToString();
                true_pass = reader[1].ToString();
            }*/

            List<string[]> data = new List<string[]>();


            while (reader.Read())
            {
                data.Add(new string[2]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
            }


            reader.Close();

            db.CloseConnection();
            for(int i = 0; i < data.Count; i++)
            {
                    if ((textBox1.Text == data[i][0]) & (textBox2.Text == data[i][1]))
                    {
                        Admin admin = new Admin();
                        this.Hide();
                        admin.ShowDialog();
                    }
                    
            }
            MessageBox.Show("invalid username or password");
            
            
        }
    }
}
