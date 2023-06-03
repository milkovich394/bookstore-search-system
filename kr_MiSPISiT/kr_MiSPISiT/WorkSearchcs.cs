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
    public partial class WorkSearchcs : Form
    {
        Basa db;
        DataTable dt;
        MySqlDataAdapter msda;
        MySqlCommand command;
        DataSet ds;
        public WorkSearchcs()
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
            //замена id на значение 
            string sql = "SELECT `work name`, `author name`, year FROM kr_shops.books, kr_shops.authors WHERE books.authors_id = authors.id;";

            command = new MySqlCommand(sql, db.GetConnection());
            msda.SelectCommand = command;
            msda.Fill(dt);

            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[3]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();

            }

            reader.Close();

            db.CloseConnection();
            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Произведение"; //текст в шапке
            column1.Width = 200; //ширина колонки
            column1.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            var column2 = new DataGridViewColumn();
            column2.HeaderText = "Автор"; //текст в шапке
            column2.Width = 200; //ширина колонки
            column2.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            var column3 = new DataGridViewColumn();
            column3.HeaderText = "Год"; //текст в шапке
            column3.Width = 105; //ширина колонки
            column3.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }


        private void Поиск_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            db.OpenConnection();
            // запрос на поиск магазинов с наличием произведений  указанных в текстбоксе1
            string sql = "SELECT `shop name`, address, phone FROM kr_shops.shops JOIN shops_has_books ON shops.id = shops_has_books.shops_id JOIN books ON shops_has_books.books_id = books.id WHERE books.`work name` = '" + textBox1.Text + "' GROUP BY shops.`shop name`, shops.address, shops.phone; ";
            command = new MySqlCommand(sql, db.GetConnection());
            msda.SelectCommand = command;
            msda.Fill(dt);

            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> data1 = new List<string[]>();

            while (reader.Read())
            {
                data1.Add(new string[3]);

                data1[data1.Count - 1][0] = reader[0].ToString();
                data1[data1.Count - 1][1] = reader[1].ToString();
                data1[data1.Count - 1][2] = reader[2].ToString();
            }

            reader.Close();

            db.CloseConnection();

            db.OpenConnection();
            string sql1 = "UPDATE shop_info, shops, shops_has_books, books SET shet = shet + 1 WHERE shops.id = shops_has_books.shops_id AND shops_has_books.books_id = books.id AND books.`work name` = '"+textBox1.Text+"' AND shop_info.shops_id = shops.id; ";
            command = new MySqlCommand(sql1, db.GetConnection());
            msda.SelectCommand = command;
            msda.Fill(dt);
            db.CloseConnection();


            dataGridView1.Columns.Clear();


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
            c3.Width = 120; //ширина колонки
            c3.ReadOnly = true; //значение в этой колонке нельзя править
            c3.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            dataGridView1.Columns.Add(c1);
            dataGridView1.Columns.Add(c2);
            dataGridView1.Columns.Add(c3);

            foreach (string[] s in data1)
                dataGridView1.Rows.Add(s);
        }
    

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }
    }
}
