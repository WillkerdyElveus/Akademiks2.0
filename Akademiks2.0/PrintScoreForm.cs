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
using DGVPrinterHelper;
using System.Data.SqlClient;

namespace Akademiks2._0
{
    public partial class PrintScoreForm : Form
    {
        private CourseClass course = new CourseClass();
        DGVPrinter printer = new DGVPrinter();
        public PrintScoreForm()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {

           // scoreDataGridView.DataSource = course.getCourseList2(new SqlCommand("SELECT * FROM  `course` WHERE CONCAT(`CourseName`) Like '%" + searchTextBox.Text + "% '"));
           // searchTextBox.Clear();
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            printer.Title = "Akademiks Courses list";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Akademiks";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(scoreDataGridView);

        }

        private void PrintCourseForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentsDataSet.Course' table. You can move, or remove it, as needed.
            this.scoreTableAdapter.Fill(this.studentsDatabaseDataSet.Score);
            scoreDataGridView.DataSource = course.getCourseList();
        }

        private void courseBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.scoreBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.studentsDatabaseDataSet);

        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            //string searchValue = searchTextBox.Text.Trim();
            SqlCommand command = new SqlCommand("SELECT * FROM Course WHERE CourseName LIKE @search");
          //  command.Parameters.Add("@search", SqlDbType.VarChar).Value = "%" + searchValue + "%";
            scoreDataGridView.DataSource = course.getCourseList2(command);
        }
    }
}
