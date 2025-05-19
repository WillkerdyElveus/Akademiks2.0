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
    internal class CourseClass
    {
        Database connect = new Database();

        public bool insertCourse(string cName, int hr, string desc)
        {
            SqlCommand command = new SqlCommand("Insert INTO Course(CourseName,CourseHour,Description) VALUES (@cn, @ch, @desc)", connect.getConnection());
            command.Parameters.Add("@cn", SqlDbType.VarChar).Value= cName;
            command.Parameters.Add("@ch", SqlDbType.Int).Value= hr;
            command.Parameters.Add("@desc", SqlDbType.VarChar).Value= desc;
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

        //create a function to get course list
        public DataTable getCourseList()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Course", connect.getConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;

        }
        //change later to correct one
        public DataTable getCourseList2(SqlCommand command)
        {
            // Set the connection before using the command
            command.Connection = connect.getConnection();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }

        //create a update function for course edit
        public bool updateCourse(int id, string name, int hours, string description)
        {
            SqlCommand command = new SqlCommand("UPDATE Course SET CourseName = CourseName, CourseHour = CourseHour, Description = Description WHERE CourseId = CourseID", connect.getConnection());

            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
            command.Parameters.Add("@hours", SqlDbType.Int).Value = hours;
            command.Parameters.Add("@desc", SqlDbType.VarChar).Value = description;

            connect.openConnection();
            bool result = command.ExecuteNonQuery() == 1;
            connect.closeConnection();

            return result;
        }

        //create a function to delete a course
        public bool deleteCourse(int id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Course WHERE CourseId = @id", connect.getConnection());
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;

            connect.openConnection();
            bool result = command.ExecuteNonQuery() == 1;
            connect.closeConnection();

            return result;
        }

    }
}
