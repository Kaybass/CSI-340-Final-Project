using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Infrastructure;
using CourseMan.Domain;
using CourseMan.Domain.Services;
using CourseMan.Domain.ValueObjects;
using CourseMan.Application.Services;
using CourseMan.Application.ValueObjects;

namespace CourseMan.Interface
{
    class InstructorConsole : SubMenu
    {
		
        public InstructorConsole()
        {
            Text = "Welcome to the Instructor console!";
            AddMenuAction("S", "See your schedule", ShowSchedule);
            AddMenuAction("L", "Logout", Logout);
        }

		// Called when the menu is opened.
		public override void OnMenuBegin()
		{
			// Customize the text based on the logged-in instructor's name.
			User instructor = AuthenticationService.Instance.LoggedInUser;
            Text = "Welcome to the Instructor console, " + instructor.FullName + "!";
		}

		// Show all sections the instructor is teaching.
		public void ShowSchedule()
        {
			User instructor = AuthenticationService.Instance.LoggedInUser;

			// Compile a schedule for the instructor.
			ScheduleService scheduleService = new ScheduleService();
			Schedule schedule = scheduleService.GetInstructerSchedule(instructor.UserID);

            Console.Clear();
			Console.WriteLine("You are teaching the following courses:\n");

			// Print out each section in the schedule.
			foreach (Section section in schedule.Sections.OrderBy(s => s.SectionID))
			{
				// Get the course for this section.
				Course course = CourseSectionHandler.Instance.GetCourse(section.CourseID);

				// Print out section info.
				Console.WriteLine("{0} {1}, Room: {2}", section.SectionID, course.Name, section.Room);
				
				// Print out meeting times.
                foreach (MeetingTime m in section.MeetingInfo.Times)
                    Console.WriteLine(m.ToString());

				Console.WriteLine();
			}

			Console.Write("Press enter to go back...");
            Console.ReadLine();
            Console.Clear();
        }
        
		// Logout and return to the login menu.
        public void Logout()
        {
            AuthenticationService.Instance.LogOut();
            ExitMenu();
        }
    }
}
