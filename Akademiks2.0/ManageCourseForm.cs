using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Akademiks2._0
{
    public partial class ManageCourseForm : Form
    {
        private CourseClass course = new CourseClass();
        private BindingSource bs = new BindingSource();

        public ManageCourseForm()
        {
            InitializeComponent();
        }

        private void ManageCourseForm_Load(object sender, EventArgs e)
        {
            // Load data into dataset if needed
            this.courseTableAdapter.Fill(this.studentsDataSet.Course);

            // Show fresh data in the DataGridView
            showTable();
        }

        public void showTable()
        {
            // Get fresh data and bind via BindingSource
            bs.DataSource = course.getCourseList();
            courseView.DataSource = bs;
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
            if (string.IsNullOrWhiteSpace(idTextBox.Text) ||
                string.IsNullOrWhiteSpace(cNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(hourTextBox.Text))
            {
                MessageBox.Show("Please select a course and fill all required fields", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int id = Convert.ToInt32(idTextBox.Text);
                string cName = cNameTextBox.Text;
                int hours = Convert.ToInt32(hourTextBox.Text);
                string desc = descriptionTextBox.Text;

                bool updated = course.updateCourse(id, cName, hours, desc);

                if (updated)
                {
                    MessageBox.Show("Course updated successfully", "Update Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearButton.PerformClick();

                    // Refresh the DataGridView properly
                    showTable();
                    bs.ResetBindings(false); // Force refresh
                }
                else
                {
                    MessageBox.Show("Failed to update course. Please try again.", "Update Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid number format in ID or Hours field.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(idTextBox.Text))
            {
                MessageBox.Show("Need course ID", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int id = Convert.ToInt32(idTextBox.Text);
                if (course.deleteCourse(id))
                {
                    clearButton.PerformClick();
                    showTable();
                    MessageBox.Show("Course deleted successfully", "Removed Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Course not found or not deleted.", "Removed Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Removed Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void courseView_Click(object sender, EventArgs e)
        {
            if (courseView.CurrentRow != null)
            {
                idTextBox.Text = courseView.CurrentRow.Cells[0].Value.ToString();
                cNameTextBox.Text = courseView.CurrentRow.Cells[1].Value.ToString();
                hourTextBox.Text = courseView.CurrentRow.Cells[2].Value.ToString();
                descriptionTextBox.Text = courseView.CurrentRow.Cells[3].Value.ToString();
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchTerm = searchTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Course WHERE CourseName LIKE @search");
                cmd.Parameters.Add("@search", SqlDbType.VarChar).Value = "%" + searchTerm + "%";
                bs.DataSource = course.getCourseList2(cmd);
                courseView.DataSource = bs;
            }

            searchTextBox.Clear();
        }

        private void courseBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.courseBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.studentsDataSet);
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            string searchValue = searchTextBox.Text.Trim();
            SqlCommand command = new SqlCommand("SELECT * FROM Course WHERE CourseName LIKE @search");
            command.Parameters.Add("@search", SqlDbType.VarChar).Value = "%" + searchValue + "%";
            bs.DataSource = course.getCourseList2(command);
            courseView.DataSource = bs;
        }
    }
}
