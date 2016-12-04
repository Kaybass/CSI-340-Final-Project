using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain;
using CourseMan.Domain.ValueObjects;

namespace CourseMan.Interface
{
    class Instructor
    {
        private int CurrentUserID;

        public Instructor(int id)
        {
            CurrentUserID = id;
        }

        public void DoInstructorThings()
        {
            Console.Clear();

            bool KeepGoing = true;

            while (KeepGoing)
            {
                Console.WriteLine("Welcome to the Instructor console type:\nS to see your schedule\nL to logout");
                string Input = Console.ReadLine();

                switch (Input)
                {
                    case "C":
                    case "c":
                        break;
                    case "S":
                    case "s":
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
