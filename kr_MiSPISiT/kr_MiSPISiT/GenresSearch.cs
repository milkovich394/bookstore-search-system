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
    public partial class GenresSearch : Form
    {
        Basa db;
        DataTable dt;
        MySqlDataAdapter msda;
        MySqlCommand command;
        DataSet ds;
        public GenresSearch()
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
            string sql = "SELECT DISTINCT `genre name` FROM kr_shops.genres;";

            command = new MySqlCommand(sql, db.GetConnection());
            msda.SelectCommand = command;
            msda.Fill(dt);

            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[1]);

                data[data.Count - 1][0] = reader[0].ToString();
            }

            reader.Close();

            db.CloseConnection();
            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Название жанра"; //текст в шапке
            column1.Width = 505; //ширина колонки
            column1.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки


            dataGridView1.Columns.Add(column1);

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }
        private void Поиск_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            db.OpenConnection();
            // запрос на поиск книг с жанром указанном в текстбоксе1
            string sql = "SELECT `work name`, `author name`, year, mark FROM kr_shops.books, kr_shops.authors, kr_shops.genres WHERE books.id = genres.books_id AND genres.`genre name` = '" + textBox1.Text + "' AND books.authors_id = authors.id;";
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
            c1.HeaderText = "Произведение"; //текст в шапке
            c1.Width = 170; //ширина колонки
            c1.ReadOnly = true; //значение в этой колонке нельзя править
            c1.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            var c2 = new DataGridViewColumn();
            c2.HeaderText = "Автор"; //текст в шапке
            c2.Width = 200; //ширина колонки
            c2.ReadOnly = true; //значение в этой колонке нельзя править
            c2.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки

            var c3 = new DataGridViewColumn();
            c3.HeaderText = "Год"; //текст в шапке
            c3.Width = 65; //ширина колонки
            c3.ReadOnly = true; //значение в этой колонке нельзя править
            c3.CellTemplate = new DataGridViewTextBoxCell(); //тип колонки
            
            var c4 = new DataGridViewColumn();
            c4.HeaderText = "Оценка"; //текст в шапке
            c4.Width = 70; //ширина колонки
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
            Form1 form = new Form1();
            this.Hide();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShopsFromGenresSearch shopsFromGenresSearch = new ShopsFromGenresSearch(textBox1.Text, this);
            shopsFromGenresSearch.Show();
        }
    }
}
