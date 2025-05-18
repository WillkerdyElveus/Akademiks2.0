using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
            MySqlCommand command = new MySqlCommand("Insert INTO `score`(`StudentId`,`CourseName`,`Score`) VALUES (@stid,@cn, @sco, @desc)", connect.getconnection);
            command.Parameters.Add("@stid", MySqlDbType.Int32).Value = stdid;
            command.Parameters.Add("@cn", MySqlDbType.Int32).Value = courseName;
            command.Parameters.Add("@sco", MySqlDbType.Double).Value = score;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;
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
        public DataTable getScoreList(MySqlCommand command)
        {
            command.Connection = connect.getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;

        }


        public bool checkScore(int stdId, string cName)
        {
            DataTable table = getScoreList(new MySqlCommand("SELECT * FROM `score` WHERE `StudentId`= " + stdId + " AND `CourseName` = '" + cName + "'"));
            if (table.Rows.Count > 0)
            {
                return false;
            }
            else { return true; }
        }

        public bool updateScore(int stdid, string scn, double score, string desc)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `score` SET `Score` = @sco,`Description` = @desc WHERE `StudentId` = @stid AND `CourseName` = @scn", connect.getconnection);
            command.Parameters.Add("@scn", MySqlDbType.VarChar).Value = scn;
            command.Parameters.Add("@stid", MySqlDbType.Int32).Value = stdid;
            command.Parameters.Add("@sco", MySqlDbType.Double).Value = score;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;
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
            MySqlCommand command = new MySqlCommand("DELETE FROM `score` WHERE `StudentId`=@id", connect.getconnection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
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
