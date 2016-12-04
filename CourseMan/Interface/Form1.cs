using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CourseMan.Interface;
using CourseMan.Domain;

namespace CourseMan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<int,User> users = CourseSectionHandler.Instance.Users;

            for(int i = 1; i <= users.Count; i++)
            {
                if(users[i].Username == textBox1.Text &&
                    users[i].Password == textBox2.Text)
                {
                    if(users[i].Type == UserType.Administrator)
                    {
                        this.Hide();
                        Form newForm = new AdminForm("meme");
                        newForm.ShowDialog();
                        this.Show();
                    }
                    else if (users[i].Type == UserType.Instructor)
                    {

                    }
                    else if(users[i].Type == UserType.Student)
                    {

                    }
                    else
                    {
                        //error
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
