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
        private SubMenu subMenu;


        public StudentConsole()
        {
          

            // Setup the admin console menu.
            Text = "Welcome to the Student console!";
            AddMenuAction('C', "See available sections", ShowAllSections);
            AddMenuAction('S', "See your schedule", ShowSchedule);
            AddMenuAction('R', "Register for a section, ", RegisterForSection);
            AddMenuAction('L', "Logout", Logout);
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
                    Console.WriteLine("CourseName: {0}\nRoom: {1}\nTime: {2}",
                        CourseSectionHandler.Instance.Courses[p.Value.CourseID].Name, p.Value.Room, p.Value.MeetingInfo.Times);
                }
            }
            Console.ReadLine();
            Console.Clear();
        }

        public void RegisterForSection()
        {
            Console.Clear();
            Console.WriteLine("Enter in the majorcode for the section you want to register");
            string code = Console.ReadLine();
            Console.WriteLine("Course code");
            string classnum = Console.ReadLine();
            int classid = int.Parse(classnum);
            Console.WriteLine("Section id");
            string sectnum = Console.ReadLine();
            int sectid = int.Parse(sectnum);

            SectionID ID = new SectionID(code, classid, sectid);

            RegistrationService reg = new RegistrationService();

            reg.Register(AuthenticationService.Instance.LoggedInUser.UserID, ID);
            Console.Clear();
        }

        public void Logout()
        {
            AuthenticationService.Instance.LogOut();
            ExitMenu();
        }
    }
}
