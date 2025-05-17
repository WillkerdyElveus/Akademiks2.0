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
            RegisterForm rs = new RegisterForm();
            rs.Show();
            this.Hide();
            hidingMenu();
        }
        private void manageStudentButton_Click(object sender, EventArgs e)
        {
            //This is for the future
            //Code
            hidingMenu();
        }
        private void statusButton_Click(object sender, EventArgs e)
        {
            //This is for the future
            //Code
            hidingMenu();
        }
        private void studentPrintButton_Click(object sender, EventArgs e)
        {
            //This is for the future
            //Code
            hidingMenu();
        }
       

        private void CourseInfo_Click(object sender, EventArgs e)
        {
            showMenu(panelCourseMenu);

        }

        private void newCourseButton_Click(object sender, EventArgs e)
        {
            CourseForm cf = new CourseForm();
            cf.Show();
            this.Hide();
            hidingMenu();
        }
        private void manageCourseButton_Click(object sender, EventArgs e)
        {
            ManageCourseForm mcf = new ManageCourseForm();
            mcf.Show();
            this.Hide();
            hidingMenu();
        }
        private void printCourseButton_Click(object sender, EventArgs e)
        {
            PrintCourseForm pcf = new PrintCourseForm();
            pcf.Show();
            hidingMenu();
        }
        private void panelScore_Click(object sender, EventArgs e)
        {
            showMenu(panelScoreMenu);
        }
        private void newScoreButton_Click(object sender, EventArgs e)
        {
            //This is for the future
            //Code
            hidingMenu();
        }
        private void manageScoreButton_Click(object sender, EventArgs e)
        {
            //This is for the future
            //Code
            hidingMenu();
        }
        private void printScoreButton_Click(object sender, EventArgs e)
        {
            //This is for the future
            //Code
            hidingMenu();
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
    }
}
