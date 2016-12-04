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
    class StudentConsole : SubMenu
    {
        public StudentConsole()
        {
          

            // Setup the admin console menu.
            Text = "Welcome to the Student console!";
            AddMenuAction("C", "See available sections", ShowAllSections);
            AddMenuAction("S", "See your schedule", ShowSchedule);
            AddMenuAction("R", "Register for a section, ", RegisterForSection);
            AddMenuAction("L", "Logout", Logout);
        }

        public void ShowAllSections()
        {
            Console.Clear();
            foreach (KeyValuePair<SectionID, Section> p in CourseSectionHandler.Instance.Sections)
            {
                Console.WriteLine("SectionID: {0}\nCourseID: {1}\nCourse Name: {2}\nInstructor: {3}\nSeats: {4}/{5}\nFilled: {6}\nRoom: {7}",
                    p.Value.SectionID, p.Value.CourseID, CourseSectionHandler.Instance.Courses[p.Value.CourseID].Name,
                    CourseSectionHandler.Instance.Users[p.Value.InstructorID].Username, p.Value.AvailableSeats, p.Value.MaxSeats,
                    p.Value.IsFull, p.Value.Room);
            }
            Console.ReadLine();
            Console.Clear();
        }

        public void ShowSchedule()
        {
            Console.Clear();
            foreach (KeyValuePair<SectionID, Section> p in CourseSectionHandler.Instance.Sections)
            {
                if (p.Value.IsStudentRegistered(AuthenticationService.Instance.LoggedInUser.UserID))
                {
                    Console.WriteLine("CourseName: {0}\nRoom: {1}\nTime:",
                        CourseSectionHandler.Instance.Courses[p.Value.CourseID].Name, p.Value.Room);
                    foreach(MeetingTime  m in p.Value.MeetingInfo.Times)
                    {
                        Console.WriteLine("{0}", m.ToString());
                    }
                }
            }
            Console.ReadLine();
            Console.Clear();
        }

        public void RegisterForSection()
        {
            int classid = 0, sectid = 0;
            Console.Clear();
            Console.WriteLine("Enter in the majorcode for the section you want to register");
            string code = Console.ReadLine();
            Console.WriteLine("Course code");
            string classnum = Console.ReadLine();
            try
            {
                classid = int.Parse(classnum);
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("invalid");
                return;
            }
            Console.WriteLine("Section id");
            string sectnum = Console.ReadLine();
            try
            {
                sectid = int.Parse(sectnum);
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("invalid");
                return;
            }
            SectionID ID = new SectionID(code, classid, sectid);

            RegistrationService reg = new RegistrationService();
            try
            {
                reg.Register(AuthenticationService.Instance.LoggedInUser.UserID, ID);
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("invalid");
                return;
            }
            Console.Clear();
        }

        public void Logout()
        {
            AuthenticationService.Instance.LogOut();
            ExitMenu();
        }
    }
}
