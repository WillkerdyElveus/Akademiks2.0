using Akademiks2._0;
using System;
using System.Data;
using System.Data.SqlClient;

class Student
{
    Database connect = new Database();

    public bool insertStudent(string fname, string lname, DateTime bdate, string phone, string gender, string address)
    {
        SqlCommand command = new SqlCommand("INSERT INTO Student ([First Name], [Last Name], [Date Of Birth], [Phone], [Address], [Gender]) " +
               "VALUES (@FirstName, @LastName, @DateOfBirth, @Phone, @Address, @Gender)", connect.getConnection());

        command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = fname;
        command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lname;
        command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = bdate;
        command.Parameters.Add("@Phone", SqlDbType.VarChar).Value = phone;
        command.Parameters.Add("@Address", SqlDbType.VarChar).Value = address;
        command.Parameters.Add("@Gender", SqlDbType.VarChar).Value = gender;

        connect.openConnection();
        int result = command.ExecuteNonQuery();
        connect.closeConnection();

        return result == 1;
    }

    public DataTable getStudentList()
    {
        SqlCommand command = new SqlCommand("SELECT * FROM Student", connect.getConnection());
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dataTable = new DataTable();
        adapter.Fill(dataTable);
        return dataTable;
    }
}