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
    internal class CourseClass
    {
        Database connect = new Database();

        public bool insertCourse(string cName, int hr, string desc)
        {
            MySqlCommand command = new MySqlCommand("Insert INTO `course`(`CourseName`,`CourseHour`,`Description`) VALUES (@cn, @ch, @desc)", connect.getconnection);
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value= cName;
            command.Parameters.Add("@ch", MySqlDbType.Int32).Value= hr;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value= desc;
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
            MySqlCommand command = new MySqlCommand("SELECT * FROM `course`", connect.getconnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;

        }
        //change later to correct one
        public DataTable getCourseList2(MySqlCommand command)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;

        }

        //create a update function for course edit
        public bool updateCourse(int id, string cName, int hr, string desc)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `course` SET `CourseName`=@cn,`CourseHour`=@ch,`Description`=@desc WHERE `CourseId`=@id", connect.getconnection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cName;
            command.Parameters.Add("@ch", MySqlDbType.Int32).Value = hr;
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

        //create a function to delete a course
        public bool deleteCourse(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE `course` WHERE `CourseId`=@id", connect.getconnection);
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
