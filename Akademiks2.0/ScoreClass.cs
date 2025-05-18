using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Akademiks2._0
{
    internal class ScoreClass
    {
        Database connect = new Database();

        public bool insertScore(int stdid, string courseName, double score, string desc)
        {
            SqlCommand command = new SqlCommand("Insert INTO Scores (StudentId,CourseName,Score) VALUES (@stid,@cn, @sco, @desc)", connect.getConnection());
            command.Parameters.Add("@stid", SqlDbType.Int).Value = stdid;
            command.Parameters.Add("@cn", SqlDbType.Int).Value = courseName;
            command.Parameters.Add("@sco", SqlDbType.Decimal).Value = score;
            command.Parameters.Add("@desc", SqlDbType.VarChar).Value = desc;
            connect.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnection();
                return true;
            }
            else
            {
                connect.closeConnection();
                return false;
            }
        }
        public DataTable getScoreList()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Scores", connect.getConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }


        public bool checkScore(int stdId, string cName)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Scores WHERE StudentId = @stdId AND CourseName = @cName");
            command.Parameters.Add("@stdId", SqlDbType.Int).Value = stdId;
            command.Parameters.Add("@cName", SqlDbType.VarChar).Value = cName;

            DataTable table = getScoreList();
            return table.Rows.Count == 0;
        }

        public bool updateScore(int stdid, string scn, double score, string desc)
        {
            SqlCommand command = new SqlCommand("UPDATE score SET Scores = @sco,Description = @desc WHERE StudentId = @stid AND CourseName = @scn", connect.getConnection());
            command.Parameters.Add("@scn", SqlDbType.VarChar).Value = scn;
            command.Parameters.Add("@stid", SqlDbType.Int).Value = stdid;
            command.Parameters.Add("@sco", SqlDbType.Decimal).Value = score;
            command.Parameters.Add("@desc", SqlDbType.VarChar).Value = desc;
            connect.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnection();
                return true;
            }
            else
            {
                connect.closeConnection();
                return false;
            }
        }

        public bool deleteScore(int id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Score WHERE StudentId=@id", connect.getConnection());
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            connect.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnection();
                return true;
            }
            else
            {
                connect.closeConnection();
                return false;
            }
        }

    }
}
