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
using MySql.Data.MySqlClient;

namespace Akademiks2._0
{
    public partial class ScoreForm : Form
    {
        CourseClass course = new CourseClass();
        Student student = new Student();
        ScoreClass score  = new ScoreClass();
        Database connect  = new Database();
        public ScoreForm()
        {
            InitializeComponent();
        }

        private void showScore()  
        {
            this.studentTableAdapter.Fill(this.studentDataSet.Student);
        }

        private void ScoreForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentDataSet.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.studentDataSet.Student);
            studentCourseComboBox.DataSource = course.getCourseList2(new SqlCommand("SELECT * FROM `Course`"));
            studentCourseComboBox.DisplayMember = "CourseName";
            studentCourseComboBox.ValueMember = "CourseName";

           showScore();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (studentIdTextBox.Text == "" || scoreTextBox.Text == "")
            {
                MessageBox.Show("Need score data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int stdId = Convert.ToInt32(studentIdTextBox.Text);
                string cName = studentCourseComboBox.Text;
                double scoreValue = Convert.ToDouble(scoreTextBox.Text);
                string desc = descriptionTextBox.Text;

                if (!score.checkScore(stdId, cName))
                {
                    if (score.insertScore(stdId, cName, scoreValue, desc))
                    {
                        showScore();
                        clearButton.PerformClick();
                        MessageBox.Show("New score added", "Add Score", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Score not added", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("The score for this course already exists", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            studentIdTextBox.Clear();
            scoreTextBox.Clear();
            studentCourseComboBox.SelectedIndex = 0;
            descriptionTextBox.Clear();
        }

        private void studentDataGridView_Click(object sender, EventArgs e)
        {
            studentIdTextBox.Text = studentView.CurrentRow.Cells[0].Value.ToString();
        }

        private void studentButton_Click(object sender, EventArgs e)
        {

            SqlCommand command = new SqlCommand("SELECT * FROM Students", connect.getConnection());
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            showScore();
        }

        private void studentBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.studentBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.studentDataSet);

        }
    }
}
