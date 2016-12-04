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
	public class AdminConsole : SubMenu
	{
		private SubMenu subMenu;
		

		public AdminConsole()
		{
			// Create a test menu.
			subMenu = new SubMenu("Example Sub Menu");
			subMenu.AddMenuAction('H', "Hello", delegate()
			{
				Console.WriteLine("Hello, World!");
			});
			subMenu.AddExitItem('B', "Back");

			// Setup the admin console menu.
			Text = "Welcome to the admin console!";
			AddMenuAction('c', "See available courses", ShowAvailableCourses);
            AddMenuAction('s', "See available sections", ShowAvailableSections);
            AddMenuAction('C', "Create a new course", delegate ()
            {
                // NOTE: Example of defining a function inline using a delegate.
                Console.Write("TODO");
            });
			AddMenuAction('S', "Create a new section", delegate()
			{
				// NOTE: Example of defining a function inline using a delegate.
				Console.Write("TODO");
			});
			AddSubMenu('T', "Enter test sub menu", subMenu);
			AddMenuAction('L', "Logout", Logout);
		}

		public void ShowAvailableCourses()
		{
            foreach(KeyValuePair<CourseID, Course> p in CourseSectionHandler.Instance.Courses)
            {
                Console.WriteLine("CourseID:{0}\nCourseName:{1}\nCourseDescription:{2}",
					p.Value.CourseID, p.Value.Name, p.Value.Description);
            }
		}

        public void ShowAvailableSections()
        {
            foreach (KeyValuePair<SectionID, Section> p in CourseSectionHandler.Instance.Sections)
            {
                Console.WriteLine("SectionID:{0}\nMax Seats:{1}\nInsructor ID: {2}", p.Value.SectionID, p.Value.MaxSeats, p.Value.InstructorID);

                foreach (MeetingTime m in p.Value.MeetingInfo.Times)
                {
                    Console.WriteLine("Meeting: {0} from {1} until {2}", m.DayOfWeek, m.StartTime, m.EndTime);
                }
            }
        }

        public void Logout()
		{
			AuthenticationService.Instance.LogOut();
			ExitMenu();
		}
	}
}
