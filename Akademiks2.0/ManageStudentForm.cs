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
    public partial class ManageStudentForm : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lensky\OneDrive\Documents\GitHub\Akademiks2.0\Akademiks2.0\ConnectedDatabase.mdf;Integrated Security=True;Connect Timeout=30";
        Student students = new Student();
        public ManageStudentForm()
        {
            InitializeComponent();
            LoadStudents();
        }
        private void ManageStudentForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentDataSet.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.studentDataSet.Student);

        }
        private void LoadStudents()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Students";
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
                maleRadioButton.Checked=true;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            fNameTextBox.Clear();
            lNameTextBox.Clear();
            phoneNumTextBox.Clear();
            addressTextBox.Clear();
            studentIdTextBox.Clear();
            maleRadioButton.Checked = true;
            dateTimePicker1.Value =  DateTime.Now;

        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            //Add random photo
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
                string query = "SELECT * FROM Students";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    studentView.DataSource = dt;
                    students.searchStudentList(searchButton.Text);
                }
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            int studentID = Convert.ToInt32(studentIdTextBox.Text);
            string fname = fNameTextBox.Text;
            string lname = lNameTextBox.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phoneNum = phoneNumTextBox.Text;
            string address = addressTextBox.Text;
            string gender = maleRadioButton.Checked ? "Male" : "Female";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Students ([First Name], [Last Name], [Date Of Birth], [Phone], [Address], [Gender]) " +
               "VALUES (@FirstName, @LastName, @DateOfBirth, @Phone, @Address, @Gender)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FirstName", fname);
                    cmd.Parameters.AddWithValue("@LastName", lname);
                    cmd.Parameters.AddWithValue("@DateOfBirth", bdate);
                    cmd.Parameters.AddWithValue("@Phone", phoneNum);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Gender", gender);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            LoadStudents();

            int bornYear = dateTimePicker1.Value.Year;
            int thisYear = DateTime.Now.Year;
            if ((thisYear - bornYear) < 10 || (thisYear - bornYear) > 100)
            {
                MessageBox.Show("The Student age must be between 10 and 100", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                try
                {
                    if (students.updateStudent(studentID,fname, lname, bdate, phoneNum, address, gender))
                    {
                        clearFields();
                        MessageBox.Show("Student Data updated", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

       
    }
}
