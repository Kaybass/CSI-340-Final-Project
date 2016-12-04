using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain;
using CourseMan.Domain.Services;
using CourseMan.Domain.ValueObjects;
using CourseMan.Infrastructure;

namespace CourseMan.Interface
{
    class InstructorConsole : SubMenu
    {
        public InstructorConsole()
        {


            // Setup the admin console menu.
            Text = "Welcome to the Student console!";
            AddMenuAction("S", "See your schedule", ShowSchedule);
            AddMenuAction("L", "Logout", Logout);
        }

        public void ShowSchedule()
        {
            Console.Clear();
            foreach (KeyValuePair<SectionID, Section> p in CourseSectionHandler.Instance.Sections)
            {
                if (p.Value.InstructorID == AuthenticationService.Instance.LoggedInUser.UserID)
                {
                    Console.WriteLine("CourseName: {0}\nRoom: {1}\nTime:",
                        CourseSectionHandler.Instance.Courses[p.Value.CourseID].Name, p.Value.Room);
                    foreach (MeetingTime m in p.Value.MeetingInfo.Times)
                    {
                        Console.WriteLine("{0}", m.ToString());
                    }
                }
            }
            Console.ReadLine();
            Console.Clear();
        }
        
        public void Logout()
        {
            AuthenticationService.Instance.LogOut();
            ExitMenu();
        }
    }
}
