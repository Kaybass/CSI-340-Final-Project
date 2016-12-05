using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain.ValueObjects;

namespace CourseMan.Domain
{
	// Class used to populate the system with some initial test data,
	// including users (students, instructors, and admins), courses, and
	// sections.
    public class TestData
    {
        public TestData()
        {
        }
		
		// Populate initial test data to the system.
        public void addTheDataToTheThing()
        {
            CourseSectionHandler csh = CourseSectionHandler.Instance;

            User a = new User(1, "Adam.Acer", "password", "Administrator", "CompSci");
            User b = new User(2, "Brian.Banana", "password", "Instructor", "CompSci");
            User c = new User(3, "Chris.Cabana", "password", "Student", "CompSci");
            User d = new User(4, "Dell.Diesel", "password", "Student", "CompSci");
            User e = new User(5, "Evan.Edmunds", "password", "Student", "CompSci");
            User f = new User(6, "Faith.Faker", "password", "Student", "CompSci");
            User g = new User(7, "Gabriella.Gilly", "password", "Student", "CompSci");
            User h = new User(8, "Harrison.Hammy", "password", "Student", "CompSci");
			User admin = new User(9, "a", "a", "Administrator", "CompSci");
			User instructor = new User(10, "i", "i", "Instructor", "CompSci");
			User student = new User(11, "s", "s", "Student", "CompSci");

            csh.AddUser(a);
            csh.AddUser(b);
            csh.AddUser(c);
            csh.AddUser(d);
            csh.AddUser(e);
            csh.AddUser(f);
            csh.AddUser(g);
            csh.AddUser(h);
            csh.AddUser(admin);
            csh.AddUser(instructor);
            csh.AddUser(student);

            Course softwareSpecialities = new Course(new CourseID("CSI", 340), "Software Specialities", "A class in domain-driven design");

            csh.AddCourse(softwareSpecialities);

            List<MeetingTime> meetingTimes = new List<MeetingTime>();
            meetingTimes.Add(new MeetingTime(DayOfWeek.Tuesday, new TimeSpan(02, 00, 0), new TimeSpan(03, 15, 0)));
            meetingTimes.Add(new MeetingTime(DayOfWeek.Thursday, new TimeSpan(03, 30, 0), new TimeSpan(04, 45, 0)));
            Section z = new Section(new SectionID("CSI", 340, 01), new Room("Wick", 101), meetingTimes,10,20);

            csh.AddSection(z);
			csh.AddSection(new Section(new SectionID("CSI", 340, 02), new Room("MIC", 308), meetingTimes,10,20));


        }
    }
}
