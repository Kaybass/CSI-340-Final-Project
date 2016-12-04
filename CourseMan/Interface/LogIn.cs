using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain;

namespace CourseMan.Interface
{
    class LogIn
    {
        public LogIn()
        {

        }

        public void LogMeIn()
        {
            while (true)
            {
                Console.WriteLine("Welcome to Course Man please enter your username followed by your password,\nctrl-c to exit");

                string username = Console.ReadLine();
                string password = Console.ReadLine();

                foreach (KeyValuePair<int, User> p in CourseSectionHandler.Instance.Users)
                {
                    if (p.Value.Username == username && p.Value.Password == password)
                    {
                        switch (p.Value.Type)
                        {
                            case UserType.Administrator:
                                Admin admin = new Admin(p.Value.UserID);

                                admin.DoAdminThings();

                                break;
                            case UserType.Instructor:

                                break;

                            case UserType.Student:

                                break;

                            default:
                                Console.WriteLine("User data error");
                                break;
                        }
                    }
                }

                Console.Clear();
            }
        }
    }
}
