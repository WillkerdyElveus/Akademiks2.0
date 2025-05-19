using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Akademiks2._0
{
    class Database
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lensky\OneDrive\Documents\GitHub\Akademiks2.0\Akademiks2.0\StudentsDatabase.mdf;Integrated Security=True;Connect Timeout=30");

        public SqlConnection getConnection()
        {

            return connection;

        }
        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
    }
}