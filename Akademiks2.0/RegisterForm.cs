﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;



namespace Akademiks2._0
{
    public partial class RegisterForm : Form
    {

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lensky\OneDrive\Documents\GitHub\Akademiks2.0\Akademiks2.0\StudentsDatabase.mdf;Integrated Security=True;Connect Timeout=30";

        Student students = new Student();
        public RegisterForm()
        {
            InitializeComponent();
            LoadStudents();
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

        private void addButton_Click(object sender, EventArgs e)
        {
            // Gather input
            string fname = fNameTextBox.Text;
            string lname = lNameTextBox.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phoneNum = phoneNumTextBox.Text;
            string address = addressTextBox.Text;
            string gender = maleRadioButton.Checked ? "Male" : "Female";

            int age = DateTime.Now.Year - bdate.Year;
            if (age < 10 || age > 100)
            {
                MessageBox.Show("The Student age must be between 10 and 100", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!verify())
            {
                MessageBox.Show("Empty Field", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (students.insertStudent(fname, lname, bdate, phoneNum, address, gender))
                {
                    MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearFields();
                }
                else
                {
                    MessageBox.Show("Failed to Add Student", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearFields()
        {
            fNameTextBox.Clear();
            lNameTextBox.Clear();
            dateTimePicker1.Value = DateTime.Now;
            phoneNumTextBox.Clear();
            addressTextBox.Clear();
            maleRadioButton.Checked = true;
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
        private void clearButton_Click(object sender, EventArgs e)
        {
            fNameTextBox.Clear();
            lNameTextBox.Clear();
            phoneNumTextBox.Clear();
            addressTextBox.Clear();
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
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentsDatabaseDataSet.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.studentsDatabaseDataSet.Student);          
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.studentBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.studentsDatabaseDataSet);

        }

        private void goBackButton_Click(object sender, EventArgs e)
        {
            LoginForm cs = new LoginForm();
            cs.ShowDialog();
            this.Hide();
        }
    }
}
