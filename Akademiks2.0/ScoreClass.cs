using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Akademiks2._0
{
    internal class ScoreClass
    {
        Database connect = new Database();

        // Insert score
        // Insert new score into the database
        public bool insertScore(int studentId, string courseName, double scoreValue, string description)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Score (StudentID, CourseName, Score, Description) " +
                                                "VALUES (@sid, @cname, @score, @desc)", connect.getConnection());

            command.Parameters.AddWithValue("@sid", studentId);
            command.Parameters.AddWithValue("@cname", courseName);
            command.Parameters.AddWithValue("@score", scoreValue);
            command.Parameters.AddWithValue("@desc", description);

            connect.openConnection();

            bool success = command.ExecuteNonQuery() == 1;

            connect.closeConnection();
            return success;
        }

        // Check if score already exists for a student-course combo
        public bool checkScore(int studentId, string courseName)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Score WHERE StudentID = @sid AND CourseName = @cname", connect.getConnection());
            command.Parameters.AddWithValue("@sid", studentId);
            command.Parameters.AddWithValue("@cname", courseName);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table.Rows.Count > 0;
        }

        // Retrieve score list
            public DataTable getScoreList(SqlCommand command)
            {
            command.Connection = connect.getConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }

        // Duplicate method, kept for flexibility
        public DataTable getScoreList2(SqlCommand command)
        {
            command.Connection = connect.getConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }

        // Check if a score exists
       
        // Update score
        public bool updateScore(int stdid, string scn, double score, string desc)
        {
            SqlCommand command = new SqlCommand("UPDATE Score SET Score = @sco, Description = @desc WHERE StudentId = @stid AND CourseName = @scn", connect.getConnection());

            command.Parameters.Add("@scn", SqlDbType.VarChar).Value = scn;
            command.Parameters.Add("@stid", SqlDbType.Int).Value = stdid;
            command.Parameters.Add("@sco", SqlDbType.Decimal).Value = score;
            command.Parameters.Add("@desc", SqlDbType.VarChar).Value = desc;

            connect.openConnection();
            bool result = command.ExecuteNonQuery() == 1;
            connect.closeConnection();

            return result;
        }

        // Delete score
        public bool deleteScore(int id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Score WHERE StudentId = @id", connect.getConnection()); // Fix: Removed the asterisk (*)
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;

            connect.openConnection();
            bool result = command.ExecuteNonQuery() == 1;
            connect.closeConnection();

            return result;
        }
    }
}
