using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain;
using CourseMan.Domain.ValueObjects;

namespace CourseMan.Interface
{
    class Admin
    {
        private int CurrentUserID;

        public Admin(int id)
        {
            CurrentUserID = id;
        }

        public void DoAdminThings()
        {
            Console.Clear();

            bool KeepGoing = true;

            while (KeepGoing)
            {
                Console.WriteLine("Welcome to the admin console type:\nC to see available Courses in a list\nS to create section\nL to logout");
                string Input = Console.ReadLine();

                switch (Input)
                {
                    case "C":
                    case "c":

                        foreach(KeyValuePair<CourseID, Course> p in CourseSectionHandler.Instance.Courses)
                        {
                            Console.WriteLine("CourseID:{0}\nCourseName:{1}\nCourseDescription:{2}"
                                ,p.Value.CourseID,p.Value.Name,p.Value.Description);
                        }

                        break;
                    case "S":
                    case "s":
                        break;
                    case "L":
                    case "l":
                        KeepGoing = false;
                        break;
                    default:
                        Console.WriteLine("Incorrect input");
                        break;
                }
            }
        }
    }
}
