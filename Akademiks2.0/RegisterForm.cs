using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;



namespace Akademiks2._0
{
    public partial class RegisterForm : Form
    {

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lensky\OneDrive\Documents\GitHub\Akademiks2.0\Akademiks2.0\StudentDatabase.mdf;Integrated Security=True;Connect Timeout=30";

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
            //Adding Student
            string fname = fNameTextBox.Text;
            string lname = lNameTextBox.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phoneNum = phoneNumTextBox.Text;
            string address = addressTextBox.Text;
            string gender = maleRadioButton.Checked ? "Male" : "Female";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Student ([First Name], [Last Name], [Date Of Birth], [Phone], [Address], [Gender]) " +
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
                    if (students.insertStudent(fname, lname, bdate, phoneNum, address, gender))
                    {

                        MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            this.studentTableAdapter.Fill(this.studentDataSet.Student);

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.studentBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.studentDataSet);

        }

        private void goBackButton_Click(object sender, EventArgs e)
        {
            LoginForm cs = new LoginForm();
            cs.ShowDialog();
            this.Hide();
        }
    }
}
