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
    public partial class ShopsFromGenresSearch : Form
    {
        Basa db;
        DataTable dt;
        MySqlDataAdapter msda;
        MySqlCommand command;
        DataSet ds;

        string genre;
        Form form;
        public ShopsFromGenresSearch(string genre, Form form)
        {
            InitializeComponent();

            this.genre = genre;
            this.form = form;
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
            string sql = "SELECT DISTINCT shops.`shop name`, shops.address, shops.phone FROM shops, kr_shops.books, kr_shops.authors,  kr_shops.genres,  kr_shops.shops_has_books WHERE shops.id = shops_has_books.shops_id AND books.id = genres.books_id AND shops_has_books.books_id = books.id AND genres.`genre name`= '"+genre+"' AND books.authors_id = authors.id; ";

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

            db.OpenConnection();
            string sql1 = "UPDATE shop_info, shops, shops_has_books, books, genres, authors SET shet = shet + 1 WHERE shops.id = shops_has_books.shops_id AND shop_info.shops_id = shops.id AND books.id = genres.books_id AND shops_has_books.books_id = books.id AND genres.`genre name`= '"+genre+"' AND books.authors_id = authors.id; ";
            command = new MySqlCommand(sql1, db.GetConnection());
            msda.SelectCommand = command;
            msda.Fill(dt);
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
            c3.Width = 116; //ширина колонки
            c3.ReadOnly = true; //значение в этой колонке нельзя править
            c3.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            dataGridView1.Columns.Add(c1);
            dataGridView1.Columns.Add(c2);
            dataGridView1.Columns.Add(c3);
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form.Hide();
            form1.ShowDialog();
        }
    }
}
