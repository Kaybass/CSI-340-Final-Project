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
                Console.Write("Are you sure you want to create a new course to add to the system?");
                String input = Console.ReadLine();
                if (input == "Y" || input == "y")
                {
                    CreateNewCourse();
                }
            });
			AddMenuAction('S', "Create a new section", delegate()
            {
                Console.Write("Are you sure you want to create a new section to add to the system? (Y / N)");
                String input = Console.ReadLine();
                if(input == "Y" || input == "y")
                {
                    CreateNewSection();
                }
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

        public void CreateNewCourse()
        {
            CourseID newCourseID;
            String majorCode, courseNumber, newName, newDesc;

            /* Create Course ID */
            Console.WriteLine("First thing first, let's create a new Course ID\nEnter the Major Code for the new course:");
            majorCode = Console.ReadLine();
            while (majorCode.Length != 3)
            {
                Console.WriteLine("Invalid Major Code\nEnter the correct Major Code for the new course:");
                majorCode = Console.ReadLine();
            }
            Console.WriteLine("Great, now enter the course number:");
            courseNumber = Console.ReadLine();
            while (courseNumber.Length != 3)
            {
                Console.WriteLine("Invalid Course Number\nEnter the correct Course Number for the new course:");
                courseNumber = Console.ReadLine();
            }
            newCourseID = new CourseID(majorCode, Int32.Parse(courseNumber));
            Console.WriteLine("Great, you're new Course has the Course ID : {0}", newCourseID.ToString());

            /* Get Room */
            Console.WriteLine("Now what is the name of this new course?");
            newName = Console.ReadLine();

            /* Get Meeting Times */
            Console.WriteLine("How about a description for {0}", newName);
            newDesc = Console.ReadLine();

            /* Create New Course */
            Course newCourse = new Course(newCourseID, newName, newDesc);

            /* Add New Course to Singleton */
            CourseSectionHandler csh = CourseSectionHandler.Instance;
            csh.AddCourse(newCourse);

            Console.WriteLine("Congratulations, {0} has been created successfully and added to the system.", csh.GetCourse(newCourseID).Name);
        }

        public void CreateNewSection()
        {
            //Stub
        }

        public void Logout()
		{
			AuthenticationService.Instance.LogOut();
			ExitMenu();
		}
	}
}
