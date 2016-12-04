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
			AddMenuAction('C', "See available courses", ShowAvailableCourses);
			AddMenuAction('S', "Create a new section", CreateSection);
			AddSubMenu('T', "Enter test sub menu", subMenu);
			AddMenuAction('L', "Logout", Logout);
		}

		public void ShowAvailableCourses()
		{
            Console.Clear();
            foreach(KeyValuePair<CourseID, Course> p in CourseSectionHandler.Instance.Courses)
            {
                Console.WriteLine("CourseID:{0}\nCourseName:{1}\nCourseDescription:{2}",
					p.Value.CourseID, p.Value.Name, p.Value.Description);
            }
            Console.ReadLine();
            Console.Clear();
		}

        public void CreateSection()
        {

        }

		public void Logout()
		{
			AuthenticationService.Instance.LogOut();
			ExitMenu();
		}
	}
}
