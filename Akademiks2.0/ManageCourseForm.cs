using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TheArtOfDevHtmlRenderer.Adapters;

namespace Akademiks2._0
{
    public partial class ManageCourseForm : Form
    {
        private CourseClass course = new CourseClass();

        public ManageCourseForm()
        {
            InitializeComponent();
        }

        private void ManageCourseForm_Load(object sender, EventArgs e)
        {
            //show data of the course
            showTable();
        }

        public void showTable()
        {
            courseView.DataSource = course.getCourseList();
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            imageCol = (DataGridViewImageColumn)courseView.Columns[7];
            imageCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            cNameTextBox.Clear();
            descriptionTextBox.Clear();
            hourTextBox.Clear();
            idTextBox.Clear();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {

            if (cNameTextBox.Text == "" || hourTextBox.Text == "" || idTextBox.Text.Equals(""))
            {
                MessageBox.Show("Need course data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                int id = Convert.ToInt32(idTextBox);
                string cName = cNameTextBox.Text;
                int chr = Convert.ToInt32(hourTextBox.Text);
                string desc = descriptionTextBox.Text;

                if (course.updateCourse(id, cName, chr, desc))
                {
                    clearButton.PerformClick();
                    MessageBox.Show("Course updated successfully", "Update Course", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error-Course was not updated", "Update Course", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text.Equals(""))
            {
                MessageBox.Show("Need course ID", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    int id = Convert.ToInt32(idTextBox);
                    if (course.deleteCourse(id))
                    {
                        clearButton.PerformClick();
                        MessageBox.Show("Course deleted successfully", "Removed Course", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }catch(Exception ex)
                
                {
                    MessageBox.Show(ex.Message, "Removed Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void courseView_Click(object sender, EventArgs e)
        {
            idTextBox.Text = courseView.CurrentRow.Cells[0].Value.ToString();
            cNameTextBox.Text = courseView.CurrentRow.Cells[1].Value.ToString();
            hourTextBox.Text = courseView.CurrentRow.Cells[2].Value.ToString();
            descriptionTextBox.Text = courseView.CurrentRow.Cells[3].Value.ToString();

        }

        //change courselist 2 later
        private void searchButton_Click(object sender, EventArgs e)
        {

            courseView.DataSource = course.getCourseList2(new MySqlCommand("SELECT * FROM  `course` WHERE CONCAT(`CourseName`) Like '%"+ searchTextBox.Text +"% '"));
            searchTextBox.Clear();
        }

    }
}

