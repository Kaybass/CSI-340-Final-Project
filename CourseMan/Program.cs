using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CourseMan.Domain;

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

            Course c = new Course();
            c.CourseID = new CourseID("CSI", 385);
            c.Name = "Operating Systems Architecture";
            c.Description = "Write the FAT12 with Josh!";
            c.InstructorID = 123;
            
            Section s = new Section();
            s.SectionID = new SectionID(c.CourseID, 2);
            s.MeetingTimes.Add(new MeetingTime(DayOfWeek.Tuesday,
                new TimeSpan(12, 30, 0), new TimeSpan(1, 45, 0)));
            s.Room = new Room("Wick", 101);

            Debug.WriteLine(c.CourseID + " "  + c.Name);
            Debug.WriteLine(s.SectionID);
            Debug.WriteLine(s.Room);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
