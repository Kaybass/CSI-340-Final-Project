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

		public AdminConsole()
		{
			String input;

			// Setup the admin console menu.
			Text = "Welcome to the admin console!";
			AddMenuAction("C", "See available courses", ShowAvailableCourses);
            AddMenuAction("S", "See available sections", ShowAvailableSections);
            AddMenuAction("CC", "Create a new course", delegate ()
            {
                Console.Write("Are you sure you want to create a new course to add to the system? (Y / N)");
				input = Console.ReadLine();
				if (input == "Y" || input == "y")
				{
					CreateNewCourse();
				}
			});
			AddMenuAction("SS", "Create a new section", delegate()
			{
				Console.Write("Are you sure you want to create a new section to add to the system? (Y / N)");
				input = Console.ReadLine();
				if (input == "Y" || input == "y")
				{
					CreateNewSection();
				}
			});
			AddMenuAction("L", "Logout", Logout);
		}

		// Called when the menu is opened.
		public override void OnMenuBegin()
		{
			// Customize the text based on the logged-in instructor's name.
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
				Console.WriteLine("{0} - {1}\n{2}\n",
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
		
		// Prompt the user to create a new course, adding it to the system.
        public void CreateNewCourse()
		{
			Course newCourse;
			CourseSectionHandler csh = CourseSectionHandler.Instance;
			CourseID newCourseID;
			String majorCode, courseNumber, newName, newDesc;

			/* Create Course ID */
			/* Get Major Code */
			Console.WriteLine("First thing first, let's create a new Course ID\nEnter the Major Code for the new course:");
			majorCode = Console.ReadLine();
			/* Validate Major Code */
			while (majorCode.Length != 3)
            {
                Console.WriteLine("Invalid Major Code\nEnter the correct Major Code for the new course:");
                majorCode = Console.ReadLine();
            }
			/* Get Course Number */
            Console.WriteLine("Great, now enter the course number:");
            courseNumber = Console.ReadLine();
			/* Validate Course Number */
			while (courseNumber.Length != 3)
            {
                Console.WriteLine("Invalid Course Number\nEnter the correct Course Number for the new course:");
                courseNumber = Console.ReadLine();
            }
            newCourseID = new CourseID(majorCode, Int32.Parse(courseNumber));
            Console.WriteLine("Great, you're new Course has the Course ID : {0}", newCourseID.ToString());

            /* Get Name */
            Console.WriteLine("Now what is the name of this new course?");
            newName = Console.ReadLine();

            /* Get Meeting Times */
            Console.WriteLine("How about a description for {0}", newName);
            newDesc = Console.ReadLine();

            /* Create New Course */
			newCourse = new Course(newCourseID, newName, newDesc);

			/* Add New Course to Singleton */
            csh.AddCourse(newCourse);

            /* Demonstrate the new course exists in the singleton */
            Console.WriteLine("Congratulations, the new Course, {0}, has been created successfully and added to the system.", csh.GetCourse(newCourseID).ToString());

			return;
        }

		public SectionID PromptSectionID()
		{
            CourseSectionHandler csh = CourseSectionHandler.Instance;
			SectionID sectionID = new SectionID();
			bool success = false;

			// Prompt Section ID
			Console.WriteLine("Alright, let's first create a new Section ID");
			Console.WriteLine("The format is: [Major Code]-[Course #]-[Section #] -- (e.g. CSI-130-01)");

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
		
		// Prompt the user to create a new section, adding it to the system.
        public void CreateNewSection()
        {
			Section newSection;
            CourseSectionHandler csh = CourseSectionHandler.Instance;
            SectionID newSectionID = new SectionID();
            //CourseID newCourseID = new CourseID();
            Room newRoom;
			MeetingTime newMT = new MeetingTime();
            List<MeetingTime> newMeetingTimes = new List<MeetingTime>();
            //String majorCode = "", courseNumber = "", sectionNumber = "";
			string roomBuilding = "", roomNumber = "", instructorID = "", seats = "", time = "";
			bool success;

			newSectionID = PromptSectionID();


			///* While loop to verify the user will create section for an existing course */
			//while (!success)
			//{
			//	/* Get Major Code */
			//	Console.WriteLine("Enter the Major Code for the section you wish to add:");
			//	majorCode = Console.ReadLine();
			//	/* Validate Major Code */
			//	while (majorCode.Length != 3)
			//	{
			//		Console.WriteLine("Invalid Major Code\nEnter the correct Major Code for the new section:");
			//		majorCode = Console.ReadLine();
			//	}
			//	/* Set Major Code */
			//	newCourseID.MajorCode = majorCode;

			//	/* Get Course Number */
			//	Console.WriteLine("Great, now enter the course number for the new section:");
			//	courseNumber = Console.ReadLine();
			//	/* Validate Course Number */
			//	while (courseNumber.Length != 3)
			//	{
			//		Console.WriteLine("Invalid Course Number\nEnter the correct Course Number for the new section:");
			//		courseNumber = Console.ReadLine();
			//	}
			//	/* Set Course Number */
			//	newCourseID.CourseNumber = Int32.Parse(courseNumber);

			//	/* Check if a course exists with the specified Major Code and Course Number */
			//	if (csh.GetCourse(newCourseID) == null)
			//	{
			//		Console.WriteLine("The specified course does not exist in the system. If you are trying to create a section for a course that does not exist, please add the course before adding a section of the course.");
			//	}
			//	else
			//	{
			//		success = true;
			//	}
			//}

			///* Get Section Number */
			//Console.WriteLine("Now what number is this new section?");
			//success = false;
			//while (!success)
			//{
			//	Console.WriteLine("Section Number : ");
			//	sectionNumber = Console.ReadLine();
			//	/* Validate Section Number */
			//	if (sectionNumber.Length != 2)
			//	{
			//		Console.WriteLine("Invalid Section Number. Please enter a valid number for this section.");
			//	}
			//	else
			//	{
			//		newSectionID.CourseID = newCourseID;
			//		newSectionID.SectionNumber = Int32.Parse(sectionNumber);
			//		if (csh.GetSection(newSectionID) != null)
			//		{
			//			Console.WriteLine("The specified section number, {0}, already exists for the course {1}-{2}", sectionNumber, majorCode, courseNumber);
			//		}
			//		else
			//		{
			//			success = true;
			//		}
			//	}
			//}
            Console.WriteLine("Great, you're new Section has the Section ID : {0}", newSectionID.ToString());

			/* Get Room for the section */
			Console.WriteLine("Now, please enter the following information about where this section will meet.");
			/* Get building to create room */
			Console.Write("Building: ");
			roomBuilding = Console.ReadLine();
			/* Get room number */
			Console.Write("Room Number: ");
			roomNumber = Console.ReadLine();
			/* Create Room object */
			newRoom = new Room(roomBuilding, Int32.Parse(roomNumber));

			/* Get Meeting Times */
			Console.WriteLine("Now that we know where this new section will meet, lets get the details for when");
			Console.WriteLine("How many times per week will this section meet?");
			Console.Write("Times to Meet per Week (e.g. 2): ");
			int freq = Int32.Parse(Console.ReadLine());
			Console.WriteLine("Please enter the following information for each meeting time.");
			for (int i = 0; i < freq; i++)
			{
				Console.WriteLine("Meeting Time #{0}", i);
				/* Get Day of the week for each MeetingTime */
				Console.Write("    Day of the week (e.g. Monday): ");
				switch(Console.ReadLine())
				{
					case "Monday":
						newMT.DayOfWeek = DayOfWeek.Monday;
						break;
					case "Tuesday":
						newMT.DayOfWeek = DayOfWeek.Tuesday;
						break;
					case "Wednesday":
						newMT.DayOfWeek = DayOfWeek.Wednesday;
						break;
					case "Thursday":
						newMT.DayOfWeek = DayOfWeek.Thursday;
						break;
					case "Friday":
						newMT.DayOfWeek = DayOfWeek.Friday;
						break;
					case "Saturday":
						newMT.DayOfWeek = DayOfWeek.Saturday;
						break;
					case "Sunday":
						newMT.DayOfWeek = DayOfWeek.Sunday;
						break;
				}
				/* Get Meeting time span for each MeetingTime */
				Console.WriteLine("When does this meeting time start?");
				Console.Write("Start Time (HH:MM AM/PM) : ");
				time = Console.ReadLine();
				DateTime dateTime;
				if (DateTime.TryParse(time, out dateTime))
					newMT.StartTime = dateTime.TimeOfDay;
				//newMT.StartTime = TimeSpan.Parse(time);
				Console.WriteLine("When does this meeting time end?");
				Console.Write("End Time (HH:MM AM/PM) : ");
				time = Console.ReadLine();
				if (DateTime.TryParse(time, out dateTime))
					newMT.EndTime = dateTime.TimeOfDay;
				//newMT.EndTime = TimeSpan.Parse(time);


				newMeetingTimes.Add(newMT);
			}

			/* Get Instructor ID */
			Console.WriteLine("Almost done. Who will be the instructor for this section?");
			success = false;
			while (!success)
			{
				Console.Write("Instructor ID: ");
				instructorID = Console.ReadLine();
				if (csh.GetUser(Int32.Parse(instructorID)).Type != UserType.Instructor)
				{
					Console.WriteLine("The specified ID number is not associated with an instructor. Please input the ID number of an instrutor to lead the section.");
				}
				else
				{
					success = true;
				}
			}

			/* Get Seats */
			Console.WriteLine("Alright, last question. How many seats are available in this section?");
			seats = Console.ReadLine();

			newSection = new Section(newSectionID, newRoom, newMeetingTimes, Int32.Parse(instructorID), Int32.Parse(seats));

            /* Add New Section to Singleton */
            csh.AddSection(newSection);

			/* Demonstrate the new course exists in the singleton */
			Console.WriteLine("Congratulations, the new Section, {0}, has been created successfully and added to the system.", csh.GetSection(newSectionID).ToString());

			return;
        }
		
		// Logout and return to the login menu.
        public void Logout()
		{
			AuthenticationService.Instance.LogOut();
			ExitMenu();
		}
	}
}
