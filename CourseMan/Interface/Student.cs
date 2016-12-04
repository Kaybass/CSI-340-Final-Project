using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain;
using CourseMan.Domain.ValueObjects;
using CourseMan.Domain.Services;

namespace CourseMan.Interface
{
    class Student
    {
        private int CurrentUserId;

        public Student(int id)
        {
            CurrentUserId = id;
        }

        public void DoStudentThings()
        {
            Console.Clear();

            bool KeepGoing = true;

            while (KeepGoing)
            {
                Console.WriteLine("Welcome to the Student console type:\nS to see your schedule\nC to see available Sections\nR to register for a section\nL to logout");
                string Input = Console.ReadLine();

                switch (Input)
                {
                    case "C":
                    case "c":
                        Console.Clear();
                        foreach(KeyValuePair<SectionID, Section> p in CourseSectionHandler.Instance.Sections)
                        {
                            Console.WriteLine("SectionID: {0}\nCourseID: {1}\nCourse Name: {2}\nInstructor: {3}\nSeats: {4}/{5}\nFilled: {6}\nRoom: {7}",
                                p.Value.SectionID,p.Value.CourseID,CourseSectionHandler.Instance.Courses[p.Value.CourseID].Name,
                                CourseSectionHandler.Instance.Users[p.Value.InstructorID].Username, p.Value.AvailableSeats,p.Value.MaxSeats,
                                p.Value.IsFull, p.Value.Room);
                        }
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "S":
                    case "s":
                        Console.Clear();
                        foreach (KeyValuePair<SectionID, Section> p in CourseSectionHandler.Instance.Sections)
                        {
                            if (p.Value.IsStudentRegistered(CurrentUserId))
                            {
                                Console.WriteLine("CourseName: {0}\nRoom: {1}\nTime: {2}",
                                    CourseSectionHandler.Instance.Courses[p.Value.CourseID].Name, p.Value.Room, p.Value.MeetingInfo.Times);
                            }
                        }
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "R":
                    case "r":
                        Console.WriteLine("Enter in the Section ID for the section you want to register");
                        string code = Console.ReadLine();
                        string classnum = Console.ReadLine();
                        int classid = int.Parse(classnum);
                        string sectnum = Console.ReadLine();
                        int sectid = int.Parse(sectnum);

                        SectionID ID = new SectionID(code, classid, sectid);

                        RegistrationService reg = new RegistrationService();

                        reg.Register(CurrentUserId, ID);

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
