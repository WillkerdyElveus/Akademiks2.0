using DGVPrinterHelper;
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

namespace Akademiks2._0
{
    public partial class PrintStudentsForm : Form
    {
        DGVPrinter printer = new DGVPrinter();
        public PrintStudentsForm()
        {
            InitializeComponent();
        }

        private void studentBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.studentBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.studentsDatabaseDataSet);

        }

        private void PrintStudentsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'connectedDataSet.Students' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.studentsDatabaseDataSet.Student);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string selectQuery;
            if (allRadioButton.Checked)
            {
                selectQuery = "SELECT * FROM Students";
            }
            else if (maleRadioButton.Checked)
            {
                {
                    selectQuery = "SELECT * FROM Students WHERE Gender='Male'";
                }
            }
            else
            {
                selectQuery = "SELECT * FROM Students WHERE Gender='Female'";
            }
            showData(new SqlCommand(selectQuery));
        }
        public void showData(SqlCommand command)
        {
            studentView.ReadOnly = true;
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            printer.Title = "Akademics Students List";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.PageNumbers = true;
            printer.FooterSpacing = 15;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(studentView);
        }
        private void exitButton_Click(Object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void printButton_Click_1(object sender, EventArgs e)
        {

            printer.Title = "Akademics Students List";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.PageNumbers = true;
            printer.FooterSpacing = 15;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(studentView);
        }
    }
}
