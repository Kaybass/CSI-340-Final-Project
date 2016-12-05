using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain.ValueObjects;
using CourseMan.Domain.Services;

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
			
			User a = new User(1, "Adam.Acer", "password", "Adam", "Acer", UserType.Administrator, Department.CompSci);
			User b = new User(2, "Brian.Banana", "password", "Brian", "Banana", UserType.Instructor, Department.CompSci);
			User c = new User(3, "Chris.Cabana", "password", "Chris", "Cabana", UserType.Student, Department.CompSci);
			User d = new User(4, "Dell.Diesel", "password", "Dell", "Diesel", UserType.Student, Department.CompSci);
			User e = new User(5, "Evan.Edmunds", "password", "Evan", "Edmunds", UserType.Student, Department.CompSci);
			User f = new User(6, "Faith.Faker", "password", "Faith", "Faker", UserType.Student, Department.CompSci);
			User g = new User(7, "Gabriella.Gilly", "password", "Gabriella", "Gilly", UserType.Student, Department.CompSci);
			User h = new User(8, "Harrison.Hammy", "password", "Harrison", "Hammy", UserType.Student, Department.CompSci);
			User admin = new User(9, "a", "a", "Mr.", "Admin", UserType.Administrator, Department.CompSci);
			User instructor = new User(10, "i", "i", "Mr.", "Instructor", UserType.Instructor, Department.CompSci);
			User student = new User(11, "s", "s", "Mr.", "Student", UserType.Student, Department.CompSci);
			
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

			Course operatingSystems = new Course(new CourseID("CSI", 385),
				"Operating Systems Architecture", "A class about operating systems and FAT12");
            csh.AddCourse(operatingSystems);

			Section csi38501 = new Section(new SectionID("CSI", 385, 01), new Room("Wick", 101), 10, 20); 
			csi38501.MeetingInfo.Times.Add(new MeetingTime(DayOfWeek.Tuesday, 12,30, 13,45));
			csi38501.MeetingInfo.Times.Add(new MeetingTime(DayOfWeek.Thursday, 12,30, 13,45));
			csh.AddSection(csi38501);
			Section csi38502 = new Section(new SectionID("CSI", 385, 02), new Room("Wick", 101), 10, 20); 
			csi38502.MeetingInfo.Times.Add(new MeetingTime(DayOfWeek.Tuesday, 11,00, 12,15));
			csi38502.MeetingInfo.Times.Add(new MeetingTime(DayOfWeek.Friday, 9,30, 10,45));
			csh.AddSection(csi38502);
			
            Course softwareSpecialities = new Course(new CourseID("CSI", 340),
				"Software Specialities", "A class in domain-driven design");
            csh.AddCourse(softwareSpecialities);

			Section csi34001 = new Section(new SectionID("CSI", 340, 01), new Room("Wick", 101), 10, 20); 
			csi34001.MeetingInfo.Times.Add(new MeetingTime(DayOfWeek.Tuesday, 2,00, 3,15));
			csi34001.MeetingInfo.Times.Add(new MeetingTime(DayOfWeek.Friday, 2,00, 3,15));
			csh.AddSection(csi34001);
			Section csi34002 = new Section(new SectionID("CSI", 340, 02), new Room("MIC", 101), 10, 20); 
			csi34002.MeetingInfo.Times.Add(new MeetingTime(DayOfWeek.Monday, 9,30, 10,45));
			csi34002.MeetingInfo.Times.Add(new MeetingTime(DayOfWeek.Wednesday, 9,30, 10,45));
			csh.AddSection(csi34002);

			RegistrationService.Instance.Register(student.UserID, csi38502.SectionID);
			RegistrationService.Instance.Register(student.UserID, csi34001.SectionID);
        }
    }
}
