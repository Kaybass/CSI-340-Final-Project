using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CourseMan.Domain;
using CourseMan.Interface;

namespace CourseMan
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Some test code.

            //         Course c = new Course() {
            //	CourseID = new CourseID("CSI", 385),
            //	Name = "Operating Systems Architecture",
            //	Description = "Write the FAT12 with Josh!",
            //	InstructorID = 0xBAD,
            //};

            //         Section s = new Section() {
            //	SectionID = new SectionID(c.CourseID, 2),
            //	MaxSeats = 20,
            //	Room = new Room("Wick", 101),
            //};
            //         s.MeetingTimes.Add(new MeetingTime(DayOfWeek.Tuesday, 12,30, 1,45));

            //User u = new User() {
            //	UserID = 1,
            //	Username = "bob",
            //	Password = "12345",
            //	Type = UserType.Student,
            //	Department = Department.CompSci,
            //};

            //         Debug.WriteLine(c.CourseID + " "  + c.Name);
            //         Debug.WriteLine(s.SectionID);
            //         Debug.WriteLine(s.Room);

            //CourseSectionHandler.Instance.AddCourse(c);
            //CourseSectionHandler.Instance.AddSection(s);
            //CourseSectionHandler.Instance.AddUser(u);

            //RegistrationService registrationService = new RegistrationService();

            //registrationService.Register(u.UserID, s.SectionID);

            //CourseSectionHandler.Instance.Courses;

            TestData data = new TestData();
            data.addTheDataToTheThing();

            LogIn login = new LogIn();
            login.LogMeIn();
        }
    }
}
