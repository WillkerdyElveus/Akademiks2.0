using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akademiks2._0
{
    public partial class ManageStudentsForm : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lensky\OneDrive\Documents\GitHub\Akademiks2.0\Akademiks2.0\StudentsDatabase.mdf;Integrated Security=True;Connect Timeout=30";
        Student students = new Student();
        public ManageStudentsForm()
        {
            InitializeComponent();
            LoadStudents();
        }
        private void ManageStudentsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentsDatabaseDataSet.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.studentsDatabaseDataSet.Student);

        }
        private void LoadStudents()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Student";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    studentView.DataSource = dt;
                }
            }
        }

        private void studentView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            studentIdTextBox.Text = studentView.CurrentRow.Cells[0].Value.ToString();
            fNameTextBox.Text = studentView.CurrentRow.Cells[1].Value.ToString();
            lNameTextBox.Text = studentView.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Value = (DateTime)studentView.CurrentRow.Cells[3].Value;
            phoneNumTextBox.Text = studentView.CurrentRow.Cells[4].Value.ToString();
            addressTextBox.Text = studentView.CurrentRow.Cells[5].Value.ToString();
            if (studentView.CurrentRow.Cells[6].Value.ToString() == "Male")
                maleRadioButton.Checked = true;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            fNameTextBox.Clear();
            lNameTextBox.Clear();
            phoneNumTextBox.Clear();
            addressTextBox.Clear();
            studentIdTextBox.Clear();
            maleRadioButton.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
        }
        private void uploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Select Photo (*.jpg;*.png;*.gif) | *.jpg;*.png;*.gif";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                studentPhotoBox.Image = Image.FromFile(ofd.FileName);
            }
        }
 

        private void searchButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Student";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    studentView.DataSource = dt;
                    students.searchStudentList(searchButton.Text);
                }
            }
        }
        
        bool verify()
        {
            if ((fNameTextBox.Text == "") || (lNameTextBox.Text == "") ||
                (phoneNumTextBox.Text == "") || (addressTextBox.Text == "") ||
                (studentPhotoBox.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void clearFields()
        {
            fNameTextBox.Clear();
            lNameTextBox.Clear();
            phoneNumTextBox.Clear();
            addressTextBox.Clear();
            maleRadioButton.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void studentBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.studentBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.studentsDatabaseDataSet);

        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (!verify())
            {
                MessageBox.Show("Empty Field", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int studentID = Convert.ToInt32(studentIdTextBox.Text);
            string fname = fNameTextBox.Text;
            string lname = lNameTextBox.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phoneNum = phoneNumTextBox.Text;
            string address = addressTextBox.Text;
            string gender = maleRadioButton.Checked ? "Male" : "Female";

            int bornYear = bdate.Year;
            int thisYear = DateTime.Now.Year;
            if ((thisYear - bornYear) < 10 || (thisYear - bornYear) > 100)
            {
                MessageBox.Show("The Student age must be between 10 and 100", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Student SET [First Name] = @FirstName, [Last Name] = @LastName, [Date Of Birth] = @DateOfBirth, [Phone] = @Phone, [Address] = @Address, [Gender] = @Gender WHERE StudentId = @StudentID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FirstName", fname);
                    cmd.Parameters.AddWithValue("@LastName", lname);
                    cmd.Parameters.AddWithValue("@DateOfBirth", bdate);
                    cmd.Parameters.AddWithValue("@Phone", phoneNum);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@StudentID", studentID);

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Student Data updated", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clearFields();
                            LoadStudents();
                        }
                        else
                        {
                            MessageBox.Show("Update failed. Student not found.", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(studentIdTextBox.Text))
            {
                MessageBox.Show("Please enter a valid Student ID.", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int studentID = Convert.ToInt32(studentIdTextBox.Text);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Student WHERE StudentId = @StudentID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentID);

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Student deleted successfully.", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clearFields();
                            LoadStudents();
                        }
                        else
                        {
                            MessageBox.Show("Delete failed. Student not found.", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        
    }
}
