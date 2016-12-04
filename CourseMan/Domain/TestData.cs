using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain.ValueObjects;

namespace CourseMan.Domain
{
    class TestData
    {
        public TestData()
        {

        }

        public void addTheDataToTheThing()
        {
            CourseSectionHandler csh = CourseSectionHandler.Instance;

            User a = new User(1, "Adam.Acer", "pass0001", "Administrator", "CompSci");
            User b = new User(2, "Brian.Banana", "pass0002", "Instructor", "CompSci");
            User c = new User(3, "Chris.Cabana", "pass0003", "Student", "CompSci");
            User d = new User(4, "Dell.Diesel", "pass0004", "Student", "CompSci");
            User e = new User(5, "Evan.Edmund", "pass0005", "Student", "CompSci");
            User f = new User(6, "Faith.Faker", "pass0006", "Student", "CompSci");
            User g = new User(7, "Gabriella.Gilly", "pass0007", "Student", "CompSci");
            User h = new User(8, "Harrison.Hammy", "pass0008", "Student", "CompSci");

            csh.AddUser(a);
            csh.AddUser(b);
            csh.AddUser(c);
            csh.AddUser(d);
            csh.AddUser(e);
            csh.AddUser(f);
            csh.AddUser(g);
            csh.AddUser(h);

            Course softwareSpecialities = new Course(new CourseID("CSI", 340), "Software Specialities", "A class in domain-driven design");

            csh.AddCourse(softwareSpecialities);

            List<MeetingTime> meetingTimes = new List<MeetingTime>();
            meetingTimes.Add(new MeetingTime(DayOfWeek.Tuesday, new TimeSpan(02, 00, 0), new TimeSpan(11, 15, 0)));
            meetingTimes.Add(new MeetingTime(DayOfWeek.Thursday, new TimeSpan(03, 00, 0), new TimeSpan(11, 15, 0)));
            Section z = new Section(new SectionID("CSI", 340, 01), new Room("Wick", 101), meetingTimes);

            csh.AddSection(z);
        }
    }
}
