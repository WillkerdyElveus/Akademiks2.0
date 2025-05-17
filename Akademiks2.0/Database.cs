using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Akademiks2._0
{
     class Database
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=studentdb");

        public MySqlConnection getconnection
        {
            get
            {
                return connection;
            }
        }
        public void openConnection()
        {
            if(connection.State ==System.Data.ConnectionState.Closed)
            connection.Open();
        }
            public void closeConnection() 
        {
            if(connection.State==System.Data.ConnectionState.Open)
                connection.Close();
        } 
    }
}
