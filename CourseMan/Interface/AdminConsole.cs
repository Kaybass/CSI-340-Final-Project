using System;
using System.Collections.Generic;
using System.Linq;
using CourseMan.Domain;
using CourseMan.Domain.Entities;
using CourseMan.Domain.Services;
using CourseMan.Domain.ValueObjects;
using CourseMan.Infrastructure;

namespace CourseMan.Interface
{
	// The menu shown when an administrator logs in.
	// In this menu, an admin can view all courses, sections, and users.
	// They can also create new courses and sections.
	public class AdminConsole : SubMenu
	{

		public AdminConsole()
		{
			string input;

			// Setup the admin console menu.
			Text = "Welcome to the admin console!";
			AddMenuAction("C", "See available courses", ShowAvailableCourses);
            AddMenuAction("S", "See available sections", ShowAvailableSections);
            AddMenuAction("U", "See all users", ShowAllUsers);
            AddMenuAction("CC", "Create a new course", delegate ()
            {
                Console.Write("Are you sure you want to create a new course to add to the system?\n(Y / N): ");
				input = Console.ReadLine();
				if (input == "Y" || input == "y")
				{
					CreateNewCourse();
				}
				Console.Clear();
			});
			AddMenuAction("SS", "Create a new section", delegate()
			{
				Console.Write("Are you sure you want to create a new section to add to the system?\n(Y / N): ");
				input = Console.ReadLine();
				if (input == "Y" || input == "y")
				{
					CreateNewSection();
				}
				Console.Clear();
			});
			AddMenuAction("L", "Logout", Logout);
		}

		// Called when the menu is opened.
		public override void OnMenuBegin()
		{
			// Customize the text based on the logged-in admin's name.
			User admin = AuthenticationService.Instance.LoggedInUser;
            Text = "Welcome to the admin console, " + admin.FullName + "!";
		}

		// Show a list of all available courses.
		public void ShowAvailableCourses()
		{
            Console.Clear();
			Console.WriteLine("Available courses:\n");

			// Print information for each course.
            foreach (KeyValuePair<CourseID, Course> p in CourseSectionHandler
				.Instance.Courses.OrderBy(c => c.Key))
			{
				Console.WriteLine("{0}: {1}\nDescription: {2}\n",
					p.Value.CourseID, p.Value.Name, p.Value.Description);
			}

			Console.Write("Press enter to go back...");
            Console.ReadLine();
            Console.Clear();
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

		// Show a list of all users in the system.
		public void ShowAllUsers()
		{
            Console.Clear();
			Console.WriteLine("List of all users:\n");

			string format = "{0,4} {1,17} {2,18} {3,15} {4,12}";

			Console.WriteLine(format,
				"ID", "Username", "Full Name", "Type", "Department");
			Console.WriteLine(format,
				"----", "-------------", "--------------", "------------", "----------");

			foreach (User user in CourseSectionHandler
				.Instance.Users.Values.OrderBy(u => u.UserID))
			{
				Console.WriteLine(format, user.UserID, user.Username,
					user.FullName, user.Type, user.Department);
			}
			
			Console.Write("\nPress enter to go back...");
            Console.ReadLine();
            Console.Clear();
		}
		
		// Prompt the user to create a new course, adding it to the system.
        public void CreateNewCourse()
		{
			CourseSectionHandler csh = CourseSectionHandler.Instance;
			
            Console.Clear();
			Console.WriteLine("Create a new course.");
			Console.WriteLine();

			// Prompt the course ID.
			Console.WriteLine("First thing first, let's create a new Course ID");
			Console.WriteLine("Enter the Major Code for the new course:");
			Console.WriteLine("The format is: [Major Code]-[Course #] - (e.g. CSI-385)");
			CourseID courseId = PromptCourseID();
            Console.WriteLine("Great, your new Course has the Course ID : {0}", courseId.ToString());
			Console.WriteLine();
			
            // Prompt the course name.
            Console.WriteLine("Now what is the name of this new course?");
            string name = Console.ReadLine();
			Console.WriteLine();

            // Prompt the course description.
            Console.WriteLine("Enter a description for {0}:", name);
            string description = Console.ReadLine();
			Console.WriteLine();

			// Construct the new course and add it to the singleton.
			if (AddCourseToSystem(new Course(courseId,  name, description)))
			{
				// Demonstrate the new course exists in the singleton.
				Console.WriteLine("Congratulations, the new Course, {0}, has been created successfully and added to the system.",
					csh.GetCourse(courseId).CourseID);
			}
            
			Console.Write("\nPress enter to go back...");
            Console.ReadLine();
            Console.Clear();
        }

		// Prompt the user to create a new section, adding it to the system.
        public void CreateNewSection()
        {
            CourseSectionHandler csh = CourseSectionHandler.Instance;

            Console.Clear();
			Console.WriteLine("Create a new section.");
			Console.WriteLine();

			// Prompt the section ID.
			Console.WriteLine("Alright, let's first create a new Section ID");
			Console.WriteLine("The format is: [Major Code]-[Course #]-[Section #] -- (e.g. CSI-130-01)");
			SectionID newSectionID = PromptSectionID();
            Console.WriteLine("Great, your new Section has an ID of {0}", newSectionID.ToString());
			Console.WriteLine();

			// Prompt the room information.
			Console.WriteLine("Now, please enter the following information about where this section will meet.");
			Room room = PromptRoom();
			Console.WriteLine();
			
			// Prompt the meeting times.
			Console.WriteLine("Now that we know where this new section will meet, lets get the details for when");
			List<MeetingTime> meetingTimes = PromptMeetingTimes();
			Console.WriteLine();

			// Prompt the instructor ID.
			Console.WriteLine("Almost done. Who will be the instructor for this section?");
			int instructorId = PromptInstructorID();
			Console.WriteLine("Instrutor ID {0} ({1}) will lead this section.\n",
				instructorId, csh.GetUser(instructorId).FullName);
			
			// Prompt the number of seats.
			Console.WriteLine("Last question. How many seats are available in this section?");
			int numSeats = PromptNumberOfSeats();
			Console.WriteLine();

			// Construct the new section and add it to the singleton.
			if (AddSectionToSystem(new Section(newSectionID, room,
					meetingTimes, instructorId, numSeats)))
			{
				// Demonstrate the new course exists in the singleton.
				Console.WriteLine("Congratulations! The new Section {0} has been created!",
					csh.GetSection(newSectionID).SectionID);
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
		
		// Add the given course to the system, returning true if successful, or false upon error.
		public bool AddCourseToSystem(Course course)
		{
			try
			{
				CourseSectionHandler.Instance.AddCourse(course);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: {0}", e.Message);
				return false;
			}
			return true;
		}

		// Add the given section to the system, returning true if successful, or false upon error.
		public bool AddSectionToSystem(Section section)
		{
			try
			{
				CourseSectionHandler.Instance.AddSection(section);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: {0}", e.Message);
				return false;
			}
			return true;
		}

		// Prompt the user to enter a course ID (for creating a new course).
		public CourseID PromptCourseID()
		{
            CourseSectionHandler csh = CourseSectionHandler.Instance;
			CourseID courseID = new CourseID();
			bool success = false;

			// Prompt Section ID
			while (!success)
			{
				Console.Write("Enter a course ID: ");
				string input = Console.ReadLine();

				// Parse the section ID.
				if (!CourseID.TryParse(input, out courseID))
				{
					Console.WriteLine("Error: invalid input!");
				}
				// Does this course already exist?
				else if (csh.GetCourse(courseID) != null)
				{
					Console.WriteLine("Error: The course number {0} already exists",
						courseID);
				}
				else
				{
					success = true;
				}
			}

			return courseID;
		}

		// Prompt the user to enter a section ID (for creating a new section).
		public SectionID PromptSectionID()
		{
            CourseSectionHandler csh = CourseSectionHandler.Instance;
			SectionID sectionID = new SectionID();
			bool success = false;

			// Prompt Section ID
			while (!success)
			{
				Console.Write("Enter a section ID: ");
				string input = Console.ReadLine();

				// Parse the section ID.
				if (!SectionID.TryParse(input, out sectionID))
				{
					Console.WriteLine("Error: invalid input!");
				}
				// Does this course exist?
				else if (csh.GetCourse(sectionID.CourseID) == null)
				{
					Console.WriteLine("Error: The course {0} does not exist in the system.", sectionID.CourseID);
					Console.WriteLine("If you are trying to create a section for a course that does not");
					Console.WriteLine("exist, please add the course before adding a section of the course.");
				}
				// Does this section already exist?
				else if (csh.GetSection(sectionID) != null)
				{
					Console.WriteLine("Error: The specified section number {0} already exists for the course {1}",
						sectionID.SectionNumber, sectionID.CourseID);
				}
				else
				{
					success = true;
				}
			}

			return sectionID;
		}
		
		// Prompt the user to enter section meeting times (for creating a new section).
		public List<MeetingTime> PromptMeetingTimes()
		{
			List<MeetingTime> meetingTimes = new List<MeetingTime>();
			MeetingTime meetingTime;
			string input;

			// Prompt the number of meeting times per week.
			Console.WriteLine("How many times per week will this section meet?");
			int numMeetingTimes;
			while (true)
			{
				Console.Write("Times to Meet per Week (e.g. 2): ");
				input = Console.ReadLine();
				if (Int32.TryParse(input, out numMeetingTimes))
					break;
				else
					Console.WriteLine("Error: invalid input");
			}
			
			// Prompt details for each meeting time.
			Console.WriteLine("Please enter the following information for each meeting time.");
			for (int i = 0; i < numMeetingTimes; i++)
			{
				Console.WriteLine("Meeting Time #{0}", i + 1);
				meetingTime = PromptMeetingTime();
				meetingTimes.Add(meetingTime);
			}

			return meetingTimes;
		}

		// Prompt the user to enter a single meeting time (for creating a new section).
		public MeetingTime PromptMeetingTime()
		{
			MeetingTime meetingTime = new MeetingTime();
			string input;
			DateTime dateTime;

			// Prompt the day of the week.
			DayOfWeek dayOfWeek = PromptDayOfWeek();
			
			// Prompt the start time.
			Console.WriteLine("When does this meeting time start?");
			while (true)
			{
				Console.Write("Start Time (HH:MM AM/PM): ");
				input = Console.ReadLine();
				if (DateTime.TryParse(input, out dateTime))
				{
					meetingTime.StartTime = dateTime.TimeOfDay;
					break;
				}
				else
					Console.WriteLine("Error: invalid input");
			}

			// Prompt the end time.
			Console.WriteLine("When does this meeting time end?");
			while (true)
			{
				Console.Write("End Time (HH:MM AM/PM): ");
				input = Console.ReadLine();
				if (DateTime.TryParse(input, out dateTime))
				{
					meetingTime.EndTime = dateTime.TimeOfDay;
					break;
				}
				else
					Console.WriteLine("Error: invalid input");
			}

			return meetingTime;
		}
		
		// Prompt the user to enter a day of the week (for creating a new section).
		public DayOfWeek PromptDayOfWeek()
		{
			DayOfWeek dayOfWeek;
			string input;

			// Prompt the day of the week.
			while (true)
			{
				Console.Write("Day of the week (e.g. Monday): ");
				input = Console.ReadLine();
				if (Enum.TryParse(input, true, out dayOfWeek))
					break;
				else
					Console.WriteLine("Error: invalid input");
			}

			return dayOfWeek;
		}
		
		// Prompt the user to enter an instructor ID (for creating a new section).
		public int PromptInstructorID()
		{
			int instructorId = -1;
			bool success = false;
			string input;
			
            CourseSectionHandler csh = CourseSectionHandler.Instance;

			while (!success)
			{
				Console.Write("Instructor ID: ");
				input = Console.ReadLine();

				// Parse and validate the instructor ID.
				if (!int.TryParse(input, out instructorId))
				{
					Console.WriteLine("Error: invalid input");
				}
				else if (csh.GetUser(instructorId) == null)
				{
					Console.WriteLine("No user exists with an ID of {0}.", instructorId);
					Console.WriteLine("Please input the ID number of an instrutor to lead the section.");
				}
				else if (csh.GetUser(instructorId).Type != UserType.Instructor)
				{
					Console.WriteLine("The specified ID number is not associated with an instructor.");
					Console.WriteLine("Please input the ID number of an instrutor to lead the section.");
				}
				else
				{
					success = true;
				}
			}

			return instructorId;
		}
		
		// Prompt the user to enter room information (for creating a new section).
		public Room PromptRoom()
		{
			int roomNumber = -1;
			string input;

			// Prompt the building name.
			Console.Write("Building: ");
			string  buildingName = Console.ReadLine();
			
			// Prompt the room number.
			while (true)
			{
				Console.Write("Room Number: ");
				input = Console.ReadLine();
				if (int.TryParse(input, out roomNumber))
					break;
				else
					Console.WriteLine("Error: invalid input");
			}

			return new Room(buildingName, roomNumber);
		}

		// Prompt the number of seats (for creating a new section).
		public int PromptNumberOfSeats()
		{
			int numSeats = -1;
			string input;

			while (true)
			{
				Console.Write("Number of seats: ");
				input = Console.ReadLine();
				if (int.TryParse(input, out numSeats))
					break;
				else
					Console.WriteLine("Error: invalid input");
			}

			return numSeats;
		}
	}
}
