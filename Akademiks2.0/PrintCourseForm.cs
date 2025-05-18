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
    public partial class PrintCourseForm : Form
    {
        private CourseClass course = new CourseClass(); 
        DGVPrinter printer = new DGVPrinter();
        public PrintCourseForm()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string search = searchTextBox.Text;

            SqlCommand command = new SqlCommand(
                "SELECT * FROM Courses WHERE CourseName LIKE @search"
            );

            command.Parameters.Add("@search", SqlDbType.VarChar).Value = "%" + search + "%";

            coursesView.DataSource = course.getCourseList2(command);
            searchTextBox.Clear();
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
            printer.PrintDataGridView(coursesView);

        }

        private void PrintCourseForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'connectedDataSet.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter.Fill(this.connectedDataSet.Courses);
        }

        private void coursesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.coursesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.connectedDataSet);

        }
    }
}
