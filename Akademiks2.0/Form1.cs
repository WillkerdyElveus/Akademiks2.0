using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akademiks2._0
{
    public partial class LoginForm : Form
    {
        Student student = new Student();

        public LoginForm()
        {
            InitializeComponent();
            customizeDesign();
            hidingMenu();
        }
        private void customizeDesign()
        {
            panelStudentMenu.Visible = false;
            panelCourseMenu.Visible = false;
            panelScoreMenu.Visible = false;
        }
        private void hidingMenu()
        {
            if (panelStudentMenu.Visible==true) 
              panelStudentMenu.Visible=false;  
            if(panelCourseMenu.Visible==true)
                panelCourseMenu.Visible=false;
            if (panelScoreMenu.Visible==true)
                panelScoreMenu.Visible=false;
        }
        private void showMenu(Panel menu)
        {
            if(menu.Visible==false)
            {
                hidingMenu();
                menu.Visible = true;
            }
            else
            {
                menu.Visible = false;

            }
        }
        private void studentInfo_Click(object sender, EventArgs e)
        {
            showMenu(panelStudentMenu);
        }
        private void registrationButton_Click(object sender, EventArgs e)
        {
            openChildForm(new RegisterForm());
            hidingMenu();
        }
        private void manageStudentButton_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageStudentsForm());
            hidingMenu();
        }
        private void statusButton_Click(object sender, EventArgs e)
        {
            hidingMenu();
        }
        private void studentPrintButton_Click(object sender, EventArgs e)
        {
            openChildForm(new PrintStudentsForm());
            hidingMenu();
        }
       

        private void CourseInfo_Click(object sender, EventArgs e)
        {
            showMenu(panelCourseMenu);

        }
        private void studentCount()
        {
            //Display the values
            totalStudent.Text = "Total Students : " + student.totalStudent();
            totalMaleStudent.Text = "Male : " + student.totalMaleStudent();
            totalFemaleStudent.Text = "Female : " + student.totalFemaleStudent();
        }

        private void newCourseButton_Click(object sender, EventArgs e)
        {
            openChildForm(new CourseForm());
            hidingMenu();
        }
        private void manageCourseButton_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageCourseForm());
            hidingMenu();
        }
        private void printCourseButton_Click(object sender, EventArgs e)
        {
            openChildForm(new PrintCourseForm());
            hidingMenu();
        }
        private void panelScore_Click(object sender, EventArgs e)
        {
            showMenu(panelScoreMenu);
        }
        private void newScoreButton_Click(object sender, EventArgs e)
        {
            openChildForm(new ScoreForm());
       
            hidingMenu();
        }
        private void manageScoreButton_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageScoreForm());
            hidingMenu();
        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(childForm);
            mainPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }
        private void printScoreButton_Click(object sender, EventArgs e)
        {
            openChildForm(new PrintScoreForm());
            hidingMenu();
        }
     
        private void dashBoardButton_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            mainPanel.Controls.Add(coverPanel);
            studentCount();

        }
       
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
