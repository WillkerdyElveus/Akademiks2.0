using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Akademiks2._0
{
    public partial class RegisterForm : Form
    {
        Student students = new Student();   
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            //Add random photo
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Select Photo (*.jpg;*.png;*.gif) | *.jpg;*.png;*.gif";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                studentPhotoBox.Image=Image.FromFile(ofd.FileName);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            //Adding Student
            string fname = fNameTextBox.Text;
            string lname  = lNameTextBox.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phoneNum = phoneNumTextBox.Text;
            string address =  addressTextBox.Text;
            string gender = maleRadioButton.Checked ? "Male" : "Female";
            MemoryStream ms = new MemoryStream();
            studentPhotoBox.Image.Save(ms, studentPhotoBox.Image.RawFormat);
            byte[] img = ms.ToArray();


            int bornYear = dateTimePicker1.Value.Year;
            int thisYear  = DateTime.Now.Year;
            if ((thisYear - bornYear) < 10 || (thisYear - bornYear) > 100)
            {
                MessageBox.Show("The Student age must be between 10 and 100", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                try
                {
                    if (students.insertStudent(fname, lname, bdate, phoneNum, address, gender, img))
                    {
                        showTable();
                        MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message ,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public void showTable()
        {
            studentView.DataSource = students.getStudentList();
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            imageCol = (DataGridViewImageColumn)studentView.Columns[7];
            imageCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            showTable();
        }
    }
}
