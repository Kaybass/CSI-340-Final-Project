using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain;
using CourseMan.Domain.Services;
using CourseMan.Domain.ValueObjects;
using CourseMan.Application.Services;
using CourseMan.Application.ValueObjects;
using CourseMan.Infrastructure;

namespace CourseMan.Interface
{
    public class StudentConsole : SubMenu
    {

        public StudentConsole()
        {
            Text = "Welcome to the Student console!";
            AddMenuAction("C", "See available sections", ShowAvailableSections);
            AddMenuAction("S", "See your schedule", ShowSchedule);
            AddMenuAction("R", "Register for a section, ", RegisterForSection);
            AddMenuAction("D", "Drop a section you are registered for, ", UnRegisterForSection);
            AddMenuAction("L", "Logout", Logout);
        }
		
		// Show a list of all available sections and their corresponding courses.
        public void ShowAvailableSections()
        {
            Console.Clear();
			Console.WriteLine("List of available sections and their courses:\n");

			CourseID currentCourseId =  new CourseID();
			bool first = true;
			Course course = null;

			// Print the list of all sections, grouped by course.
            foreach (Section section in CourseSectionHandler.Instance.Sections.Values.OrderBy(s => s.SectionID))
			{
				// Write course information for each group of sections per course.
				if (first || section.CourseID != currentCourseId)
				{
					first = false;
					currentCourseId = section.CourseID;
					course = CourseSectionHandler.Instance.GetCourse(currentCourseId);
					
					Console.WriteLine("-------------------------------------------------------");
					Console.WriteLine("{0} {1}\n{2}\n",
						course.CourseID, course.Name, course.Description);
					Console.WriteLine("Available Sections:\n");
				}
				
				// Print out section info.
				User instructor = CourseSectionHandler.Instance.GetUser(section.InstructorID);
				Console.WriteLine("{0}, Room: {2}, Seats {3}/{4}, Instructor: {5}",
					section.SectionID, course.Name, section.Room, section.AvailableSeats,
					section.MaxSeats, instructor.FullName);
				
				// Print out meeting times.
				foreach (MeetingTime m in section.MeetingInfo.Times)
					Console.WriteLine("{0}", m.ToString());

				Console.WriteLine();
			}

			Console.Write("Press enter to go back...");
            Console.ReadLine();
            Console.Clear();
        }
		
		// Show all sections the student is registered for.
        public void ShowSchedule()
        {
			User student = AuthenticationService.Instance.LoggedInUser;

			// Compile a schedule for the student.
			ScheduleService scheduleService = new ScheduleService();
			Schedule schedule = scheduleService.GetStudentSchedule(student.UserID);

            Console.Clear();
			Console.WriteLine("You are registered for the following courses:\n");

			// Print out each section in the schedule.
			foreach (Section section in schedule.Sections.OrderBy(s => s.SectionID))
			{
				// Get the course and instructor for this section.
				Course course = CourseSectionHandler.Instance.GetCourse(section.CourseID);
				User instructor = CourseSectionHandler.Instance.GetUser(section.InstructorID);

				// Print out section info.
				Console.WriteLine("{0} {1}\nRoom: {2}, Instructor: {3}",
					section.SectionID, course.Name, section.Room, instructor.FullName);
				
				// Print out meeting times.
                foreach (MeetingTime m in section.MeetingInfo.Times)
                    Console.WriteLine(m.ToString());

				Console.WriteLine();
			}

			Console.Write("Press enter to go back...");
            Console.ReadLine();
            Console.Clear();
        }

		// Prompt the user to register for a section.
        public void RegisterForSection()
        {
            Console.Clear();
			Console.WriteLine("Enter the ID of the section you want to register for:");
			string input = Console.ReadLine();
			Console.WriteLine();

			// Try to parse the input into a section ID.
			SectionID sectionId;
			if (!SectionID.TryParse(input, out sectionId))
			{
				Console.WriteLine("Error: invalid input");
			}
			else
			{
				try
				{
					// Attempt to register for the section.
					int myId = AuthenticationService.Instance.LoggedInUser.UserID;
					RegistrationService.Instance.Register(myId, sectionId);
					Console.WriteLine("Successfully registered for {0}.", sectionId);
				}
				catch (Exception e)
				{
					Console.WriteLine("Error registering for {0}:\n{1}", sectionId, e.Message);
				}
			}
			
			Console.Write("\nPress enter to go back...");
			Console.ReadLine();
            Console.Clear();
		}
		
		// Prompt the user to unregister for (drop) a section.
		public void UnRegisterForSection()
		{
            Console.Clear();
			Console.WriteLine("Enter the ID of the section you want to drop:");
			string input = Console.ReadLine();
			Console.WriteLine();

			// Try to parse the input into a section ID.
			SectionID sectionId;
			if (!SectionID.TryParse(input, out sectionId))
			{
				Console.WriteLine("Error: invalid input");
			}
			else
			{
				try
				{
					// Attempt to un-register for the section.
					int myId = AuthenticationService.Instance.LoggedInUser.UserID;
					RegistrationService.Instance.UnRegister(myId, sectionId);
					Console.WriteLine("Successfully dropped {0}.", sectionId);
				}
				catch (Exception e)
				{
					Console.WriteLine("Error dropping {0}:\n{1}", sectionId, e.Message);
				}
			}
			
			Console.Write("\nPress enter to go back...");
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
