    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows.Forms;

    namespace Akademiks2._0
    {
        public partial class ScoreForm : Form
        {
            CourseClass course = new CourseClass();
            Student student = new Student();
            ScoreClass score = new ScoreClass();

            public ScoreForm()
            {
                InitializeComponent();
            }

            private void showScore()
            {
                try
                {
                    scoreView.DataSource = score.getScoreList(
                        new SqlCommand("SELECT Score.StudentID, Student.[First Name], Student.[Last Name], Score.CourseName, Score.Score, Score.Description " +
                                       "FROM Student INNER JOIN Score ON Score.StudentID = Student.StudentId"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading scores: " + ex.Message);
                }
            }

            private void ScoreForm_Load(object sender, EventArgs e)
            {
                try
                {
                    // Load students and scores into the dataset (for other functionality)
                    this.scoreTableAdapter.Fill(this.studentsDataSet.Score);
                    this.studentTableAdapter.Fill(this.studentsDataSet.Student);

                    // Load course list into combobox
                    studentCourseComboBox.DataSource = course.getCourseList2(new SqlCommand("SELECT * FROM Course"));
                    studentCourseComboBox.DisplayMember = "CourseName";
                    studentCourseComboBox.ValueMember = "CourseName";

                    // Optionally load a student list in score view
                    scoreView.DataSource = student.getStudentList2(
                        new SqlCommand("SELECT [First Name], [Last Name] FROM Student"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading form data: " + ex.Message);
                }
            }

            private void addButton_Click(object sender, EventArgs e)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(studentIdTextBox.Text) || string.IsNullOrWhiteSpace(scoreTextBox.Text))
                    {
                        MessageBox.Show("Please enter both Student ID and Score.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int stdId = Convert.ToInt32(studentIdTextBox.Text);
                    string cName = studentCourseComboBox.Text;
                    double scoreValue = Convert.ToDouble(scoreTextBox.Text);
                    string desc = descriptionTextBox.Text;

                    // Check if score already exists
                    if (!score.checkScore(stdId, cName))
                    {
                        if (score.insertScore(stdId, cName, scoreValue, desc))
                        {
                            showScore();
                            clearButton.PerformClick(); // Clear fields
                            MessageBox.Show("New score added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to add score.", "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Score already exists for this student and course.", "Duplicate Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid number format. Make sure ID is an integer and score is a number.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding score: " + ex.Message);
                }
            }

            private void clearButton_Click(object sender, EventArgs e)
            {
                studentIdTextBox.Clear();
                scoreTextBox.Clear();
                descriptionTextBox.Clear();
                studentCourseComboBox.SelectedIndex = 0;
            }

            private void scoreButton_Click(object sender, EventArgs e)
            {
                showScore();
            }

            private void studentBindingNavigatorSaveItem_Click(object sender, EventArgs e)
            {
                this.Validate();
                this.studentBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.studentsDataSet);
            }

       
    }
    }
