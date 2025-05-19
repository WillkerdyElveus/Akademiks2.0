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
    public DataTable searchStudentList(string searchData)
    {
        SqlCommand command = new SqlCommand("SELECT * FROM Student WHERE CONCAT([First Name], [Last Name], [Address]) LIKE '%" + searchData + "%'", connect.getConnection());
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dataTable = new DataTable();
        adapter.Fill(dataTable);
        return dataTable;
    }

    public DataTable getStudentList()
    {
        SqlCommand command = new SqlCommand("SELECT * FROM Student", connect.getConnection());
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dataTable = new DataTable();
        adapter.Fill(dataTable);
        return dataTable;
    }
    public DataTable getStudentList2(SqlCommand command)
    {
        // Set the connection before using the command
        command.Connection = connect.getConnection();
            
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dataTable = new DataTable();
        adapter.Fill(dataTable);
        return dataTable;
    }
    public string exeCount(string query)
    {
        SqlCommand command = new SqlCommand(query, connect.getConnection());
        connect.openConnection();
        string count = command.ExecuteScalar().ToString();
        connect.closeConnection();
        return count;
    }
    public bool updateStudent(int studentId, string fname, string lname, DateTime bdate, string phone, string gender, string address)
    {
        SqlCommand command = new SqlCommand("UPDATE Student SET " + "[First Name] = @FirstName, " + "[Last Name] = @LastName, " +
                                            "[Date Of Birth] = @DateOfBirth, " + "[Phone] = @Phone, " + "[Address] = @Address, " +
                                            "[Gender] = @Gender " + "WHERE [StudentId] = @StudentId",
    connect.getConnection());
        command.Parameters.Add("@StudentId", SqlDbType.Int).Value = studentId;
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
    public string totalMaleStudent()
    {
        return exeCount("SELECT COUNT(*) FROM Student WHERE Gender ='Male'");
    }
    public string totalFemaleStudent()
    {
        return exeCount("SELECT COUNT(*) FROM Student WHERE Gender ='Female'");
    }
    public string totalStudent()
    {
        return exeCount("SELECT COUNT(*) FROM Student");
    }
}