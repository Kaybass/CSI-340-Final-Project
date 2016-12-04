using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain;
using CourseMan.Domain.ValueObjects;

namespace CourseMan.Interface
{
    class Admin
    {
        private int CurrentUserID;

        public Admin(int id)
        {
            CurrentUserID = id;
        }

        public void DoAdminThings()
        {
            Console.Clear();

            bool KeepGoing = true;

            while (KeepGoing)
            {
                Console.WriteLine("Welcome to the admin console type:\nC to see available Courses in a list.\nS to see availbe Sections in a list.\nCourse to create new Course.\nSection to create new Section.\nL to logout");
                string Input = Console.ReadLine();

                switch (Input)
                {
                    case "C":
                    case "c":

                        foreach(KeyValuePair<CourseID, Course> p in CourseSectionHandler.Instance.Courses)
                        {
                            Console.WriteLine("CourseID:{0}\nCourseName:{1}\nCourseDescription:{2}"
                                ,p.Value.CourseID,p.Value.Name,p.Value.Description);
                        }

                        break;
                    case "S":
                    case "s":
                        foreach(KeyValuePair<SectionID, Section> p in CourseSectionHandler.Instance.Sections)
                        {
                            Console.WriteLine("SectionID:{0}\nMax Seats:{1}\nInsructor ID: {2}", p.Value.SectionID, p.Value.MaxSeats, p.Value.InstructorID);

                            foreach(MeetingTime m in p.Value.MeetingInfo.Times)
                            {
                                Console.WriteLine("Meeting: {0} from {1} until {2}", m.DayOfWeek, m.StartTime, m.EndTime);
                            }
                        }
                        break;
                    case "L":
                    case "l":
                        KeepGoing = false;
                        break;
                    default:
                        Console.WriteLine("Incorrect input");
                        break;
                }
            }
        }
    }
}
