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
    public partial class ManageScoreForm : Form
    {
        CourseClass course = new CourseClass();
        ScoreClass score = new ScoreClass();
        public ManageScoreForm()
        {
            InitializeComponent();
        }

        private void ManageScoreForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentsDataSet.Score' table. You can move, or remove it, as needed.
            this.scoreTableAdapter.Fill(this.studentsDataSet.Score);
            studentCourseComboBox.DataSource = course.getCourseList2(new SqlCommand("SELECT * FROM Course"));
            studentCourseComboBox.DisplayMember = "CourseName";
            studentCourseComboBox.ValueMember = "CourseName";

            showScore();
        }
        public void showScore()
        {
           studentCourseComboBox.DataSource = score.getScoreList2(new SqlCommand("SELECT Score.StudentID, Student.[First Name], Student.[Last Name], Score.CourseName, Score.Score, Score.Description FROM Student INNER JOIN Score ON Score.StudentID=Student.StudentID"));

        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (studentIdTextBox.Text == "" || scoreTextBox.Text == "")
            {
                MessageBox.Show("Need score data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int stdId = Convert.ToInt32(studentIdTextBox.Text);
                string cName = studentCourseComboBox.Text;
                double scoreValue = Convert.ToInt32(scoreTextBox.Text);
                string desc = descriptionTextBox.Text;

               



                    if (score.updateScore(stdId, cName, scoreValue, desc))
                    {
                        showScore();
                        clearButton.PerformClick();
                        MessageBox.Show("Score edited completed", "Update Score", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Score not edited", "Update Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            
            if(studentIdTextBox.Text == "")
            {
                MessageBox.Show("field error- we need student id ", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int id = Convert.ToInt32(studentIdTextBox.Text);
                //Maybe works ionknow
                MessageBox.Show("Are you sure you want to remove this score", "Delete Score", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                
                    if (score.deleteScore(id))
                    {
                        showScore();
                        MessageBox.Show("Score removed", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearButton.PerformClick();
                    }
                
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            studentIdTextBox.Clear();
            scoreTextBox.Clear();
            descriptionTextBox.Clear();
            searchTextBox.Clear();
        }

        private void scoreView_Click(object sender, EventArgs e)
        {
            studentIdTextBox.Text = scoreView.CurrentRow.Cells[0].Value.ToString();
            studentCourseComboBox.Text = scoreView.CurrentRow.Cells[3].Value.ToString();
            scoreTextBox.Text = scoreView.CurrentRow.Cells[4].Value.ToString();
            descriptionTextBox.Text = scoreView.CurrentRow.Cells[5].Value.ToString();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            scoreView.DataSource = score.getScoreList(new SqlCommand("SELECT Score.StudentID,Student.[First Name], Student.[Last Name], Score.CourseName, Score.Score, Score.Description FROM Student INNER JOIN score ON Score.StudentID=Student.StudentdID WHERE CONCAT(Student.Student.[First Name], Student.[Last Name], Score.CourseName) LIKE  '%" + searchTextBox.Text+"%'"));
        }

        private void scoreBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.scoreBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.studentsDataSet);

        }
    }
}
