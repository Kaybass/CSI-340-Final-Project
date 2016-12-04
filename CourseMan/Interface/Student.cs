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
                                p.Value.SectionID,p.Value.CourseID,CourseSectionHandler.Instance.Courses[p.Value.CourseID],
                                CourseSectionHandler.Instance.Users[p.Value.InstructorID].Username, p.Value.AvailableSeats,p.Value.MaxSeats,
                                p.Value.IsFull, p.Value.Room);
                        }
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "S":
                    case "s":
                        break;
                    case "R":
                    case "r":
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
