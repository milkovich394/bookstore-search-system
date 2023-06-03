using MySql.Data;
using MySql.Data.MySqlClient;

namespace kr_MiSPISiT
{
    class Basa
    {

        MySqlConnection connection = new MySqlConnection("server=localhost;username=root;password=root;database=kr_shops");

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}
