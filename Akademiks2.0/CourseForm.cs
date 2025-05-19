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

namespace Akademiks2._0
{
    public partial class CourseForm : Form
    {
        private CourseClass course = new CourseClass();
        public CourseForm()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (cNameTextBox.Text == "" || hourTextBox.Text == "")
            {
                MessageBox.Show("Need course data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                string cName = cNameTextBox.Text;
                int chr = Convert.ToInt32(hourTextBox.Text);
                string desc = descriptionTextBox.Text;

                if (course.insertCourse(cName, chr, desc))
                {
                    clearButton.PerformClick();
                    MessageBox.Show("New course inserted", "Add Course", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.courseTableAdapter.Fill(this.studentsDatabaseDataSet.Course);

                }
                else
                {
                    MessageBox.Show("Course not inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            cNameTextBox.Clear();
            descriptionTextBox.Clear();
            hourTextBox.Clear();
        }

        public void showTable()
        {
            courseView.DataSource = course.getCourseList();
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            imageCol = (DataGridViewImageColumn)courseView.Columns[5];
            imageCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        private void courseBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.courseBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.studentsDatabaseDataSet);

        }

        private void CourseForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentsDatabaseDataSet.Course' table. You can move, or remove it, as needed.
            this.courseTableAdapter.Fill(this.studentsDatabaseDataSet.Course);
            // TODO: This line of code loads data into the 'studentsDatabaseDataSet.Course' table. You can move, or remove it, as needed.

        }

        private void courseBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.courseBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.studentsDatabaseDataSet);

        }

        private void courseBindingNavigatorSaveItem_Click_2(object sender, EventArgs e)
        {
            this.Validate();
            this.courseBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.studentsDatabaseDataSet);

        }
    }
}
