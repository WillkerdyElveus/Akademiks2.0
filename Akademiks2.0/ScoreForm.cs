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
    public partial class ScoreForm : Form
    {
        CourseClass course = new CourseClass();
        Student student = new Student();
        ScoreClass score  = new ScoreClass();
        public ScoreForm()
        {
            InitializeComponent();
        }

        private void showScore()
        {
            studentDataGridView.DataSource = score.getScoreList(new MySqlCommand("SELECT score.StudentId, student.StdFirstName, student.StdLastName, score.CourseName, score.Score, score.Description, FROM student INNER JOIN score ON score.StudentId=student.StdId"));
        }

        private void ScoreForm_Load(object sender, EventArgs e)
        {
            studentCourseComboBox.DataSource = course.getCourseList2(new MySqlCommand("SELECT * FROM `course`"));
            studentCourseComboBox.DisplayMember = "CourseName";
            studentCourseComboBox.ValueMember = "CourseName";

           // showScore();

            studentDataGridView.DataSource = student.getStudentList(new MySqlCommand("SELECT `StdId`, `StdFirstName`, `StdLastName` FROM `student`"));

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
                double score = Convert.ToInt32(scoreTextBox.Text);
                string desc = descriptionTextBox.Text;

                if(!score.checkScore(stdId, cName, score, desc))
                {

                

                if (score.insertScore(stdId, cName, score, desc ))
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
                    MessageBox.Show("the score for this course already exists", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            studentIdTextBox.Text = studentDataGridView.CurrentRow.Cells[0].Value.ToString();
        }

        private void studentButton_Click(object sender, EventArgs e)
        {
            studentDataGridView.DataSource = student.getStudentList(new MySqlCommand("SELECT `StdId`, `StdFirstName`, `StdLastName` FROM `student`"));
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            showScore();
        }
    }
}
