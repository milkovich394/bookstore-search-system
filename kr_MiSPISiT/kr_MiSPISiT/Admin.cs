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
using System.IO;
using DataTable = System.Data.DataTable;

namespace kr_MiSPISiT
{
    public partial class Admin : Form
    {
        Basa db;
        DataTable dt;
        MySqlDataAdapter msda;
        MySqlCommand command;
        DataSet ds;


        public Admin()
        {
            InitializeComponent();

            db = new Basa();
            dt = new DataTable();
            msda = new MySqlDataAdapter();
            ds = new DataSet();
            Start();
        }

        private void Start()
        {
            dataGridView1.Rows.Clear();
            db.OpenConnection();

            string sql = "SELECT * FROM kr_shops.shops;";

            command = new MySqlCommand(sql, db.GetConnection());
            msda.SelectCommand = command;
            msda.Fill(dt);

            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();


            while (reader.Read())
            {
                data.Add(new string[3]);

                data[data.Count - 1][0] = reader[1].ToString();
                data[data.Count - 1][1] = reader[2].ToString();
                data[data.Count - 1][2] = reader[3].ToString();
            }

            reader.Close();

            db.CloseConnection();
            var c1 = new DataGridViewColumn();
            c1.HeaderText = "Название магазина"; //текст в шапке
            c1.Width = 200; //ширина колонки
            c1.ReadOnly = true; //значение в этой колонке нельзя править
            c1.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            var c2 = new DataGridViewColumn();
            c2.HeaderText = "Адрес"; //текст в шапке
            c2.Width = 200; //ширина колонки
            c2.ReadOnly = true; //значение в этой колонке нельзя править
            c2.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            var c3 = new DataGridViewColumn();
            c3.HeaderText = "Номер телефона"; //текст в шапке
            c3.Width = 119; //ширина колонки
            c3.ReadOnly = true; //значение в этой колонке нельзя править
            c3.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            dataGridView1.Columns.Add(c1);
            dataGridView1.Columns.Add(c2);
            dataGridView1.Columns.Add(c3);
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            db.OpenConnection();
            // запрос на поиск книг с жанром указанном в текстбоксе1
            string sql = "SELECT `shop name`,purchased_items,sum,sum_of_taxes FROM kr_shops.shops,kr_shops.shop_info WHERE `shop name`= '"+textBox1.Text+"' AND shop_info.shops_id = shops.id; ";
            
            command = new MySqlCommand(sql, db.GetConnection());
            msda.SelectCommand = command;
            msda.Fill(dt);

            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> data1 = new List<string[]>();

            while (reader.Read())
            {
                data1.Add(new string[4]);

                data1[data1.Count - 1][0] = reader[0].ToString();
                data1[data1.Count - 1][1] = reader[1].ToString();
                data1[data1.Count - 1][2] = reader[2].ToString();
                data1[data1.Count - 1][3] = reader[3].ToString();

            }

            reader.Close();

            db.CloseConnection();

            dataGridView1.Columns.Clear();


            var c1 = new DataGridViewColumn();
            c1.HeaderText = "Название магазина"; //текст в шапке
            c1.Width = 200; //ширина колонки
            c1.ReadOnly = true; //значение в этой колонке нельзя править
            c1.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            var c2 = new DataGridViewColumn();
            c2.HeaderText = "Продажи"; //текст в шапке
            c2.Width = 110; //ширина колонки
            c2.ReadOnly = true; //значение в этой колонке нельзя править
            c2.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            var c3 = new DataGridViewColumn();
            c3.HeaderText = "Сумма"; //текст в шапке
            c3.Width = 100; //ширина колонки
            c3.ReadOnly = true; //значение в этой колонке нельзя править
            c3.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            var c4 = new DataGridViewColumn();
            c4.HeaderText = "Налог"; //текст в шапке
            c4.Width = 110; //ширина колонки
            c4.ReadOnly = true; //значение в этой колонке нельзя править
            c4.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            dataGridView1.Columns.Add(c1);
            dataGridView1.Columns.Add(c2);
            dataGridView1.Columns.Add(c3);
            dataGridView1.Columns.Add(c4);

            foreach (string[] s in data1)
                dataGridView1.Rows.Add(s);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            db.OpenConnection();
            // запрос на поиск книг с жанром указанном в текстбоксе1

            string sql =
                "UPDATE kr_shops.shop_info, kr_shops.shops SET purchased_items = "+textBox2.Text+" WHERE shop_info.shops_id = shops.id AND shops.`shop name`= '"+textBox1.Text+ "'; UPDATE shop_info SET sum = purchased_items * 300; UPDATE shop_info SET sum_of_taxes = sum * 0.13; SELECT `shop name`,purchased_items,sum,sum_of_taxes FROM kr_shops.shops,kr_shops.shop_info WHERE `shop name`= '" + textBox1.Text + "' AND shop_info.shops_id = shops.id;  ";

            command = new MySqlCommand(sql, db.GetConnection());
            msda.SelectCommand = command;
            msda.Fill(dt);

            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> data1 = new List<string[]>();

            while (reader.Read())
            {
                data1.Add(new string[4]);

                data1[data1.Count - 1][0] = reader[0].ToString();
                data1[data1.Count - 1][1] = reader[1].ToString();
                data1[data1.Count - 1][2] = reader[2].ToString();
                data1[data1.Count - 1][3] = reader[3].ToString();

            }

            reader.Close();

            db.CloseConnection();

            dataGridView1.Columns.Clear();


            var c1 = new DataGridViewColumn();
            c1.HeaderText = "Название магазина"; //текст в шапке
            c1.Width = 200; //ширина колонки
            c1.ReadOnly = true; //значение в этой колонке нельзя править
            c1.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            var c2 = new DataGridViewColumn();
            c2.HeaderText = "Продажи"; //текст в шапке
            c2.Width = 110; //ширина колонки
            c2.ReadOnly = true; //значение в этой колонке нельзя править
            c2.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            var c3 = new DataGridViewColumn();
            c3.HeaderText = "Сумма"; //текст в шапке
            c3.Width = 100; //ширина колонки
            c3.ReadOnly = true; //значение в этой колонке нельзя править
            c3.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            var c4 = new DataGridViewColumn();
            c4.HeaderText = "Налог"; //текст в шапке
            c4.Width = 110; //ширина колонки
            c4.ReadOnly = true; //значение в этой колонке нельзя править
            c4.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            dataGridView1.Columns.Add(c1);
            dataGridView1.Columns.Add(c2);
            dataGridView1.Columns.Add(c3);
            dataGridView1.Columns.Add(c4);

            foreach (string[] s in data1)
                dataGridView1.Rows.Add(s);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            db.OpenConnection();
            // запрос на поиск книг с жанром указанном в текстбоксе1
            string sql = "SELECT `shop name`,purchased_items,sum,sum_of_taxes, shet FROM kr_shops.shops,kr_shops.shop_info WHERE  shop_info.shops_id = shops.id; ";

            command = new MySqlCommand(sql, db.GetConnection());
            msda.SelectCommand = command;
            msda.Fill(dt);

            MySqlDataReader reader = command.ExecuteReader();
            StreamWriter sr = new StreamWriter("/kr_MiSPISiT/otchet_shops"); // file path
            while (reader.Read())
            {
                sr.Write(reader[0].ToString() + "\t" + reader[1].ToString() + "\t" + reader[2].ToString()+"\t"+ reader[3].ToString() + "\t" + reader[4].ToString()+"\n");
            }
            sr.Close();
            MessageBox.Show("done");
        }
    }
}
