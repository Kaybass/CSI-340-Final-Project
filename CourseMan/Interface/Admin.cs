using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Console.WriteLine("Welcome to the admin console type:\nC to create section\nL to logout");
                string Input = Console.ReadLine();

                switch (Input)
                {
                    case "C":
                    case "c":
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
