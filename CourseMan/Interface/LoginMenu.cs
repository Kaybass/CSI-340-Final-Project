using System;

using CourseMan.Domain;
using CourseMan.Domain.Entities;
using CourseMan.Domain.Services;
using CourseMan.Infrastructure;

namespace CourseMan.Interface
{
	// The first menu shown in the program, which allows
	// a user to log in with their username and password,
	// taking them to the appropriate menu for whether they
	// are an admin, student, or instructor.
	public class LoginMenu : SubMenu
	{
		private AdminConsole      adminConsole;
        private StudentConsole    studentConsole;
        private InstructorConsole instructorconsole;
		

		public LoginMenu()
		{
			adminConsole = new AdminConsole();
            studentConsole = new StudentConsole();
            instructorconsole = new InstructorConsole();
			
			Text = "Welcome to the CourseMan! Please login to continue.";
			AddMenuAction("L", "Log In", PromptLogin);
			AddMenuAction("A", "About", ShowAboutInfo);
			AddExitItem("Q", "Quit");
		}

		public void PromptLogin()
		{
			AuthenticationService authenticator = AuthenticationService.Instance;

			// Prompt the user for username & password.
			Console.Write("Enter username: ");
			string username = Console.ReadLine();
			Console.Write("Enter password: ");
			string password = Console.ReadLine();

            // Attempt to log in.
            if (!authenticator.LogIn(username, password))
                Console.WriteLine("\nInvalid username or password.");

            // Enter the appropriate sub menu for the user type.
            else
            {
                switch (authenticator.LoggedInUser.Type)
                {
                    case UserType.Administrator:
                        EnterSubMenu(adminConsole);
                        break;
                    case UserType.Instructor:
                        EnterSubMenu(instructorconsole);
                        break;
                    case UserType.Student:
                        EnterSubMenu(studentConsole);
                        break;
                }
            }
		}
		
		public void ShowAboutInfo()
		{
			Console.WriteLine("CourseMan");
			Console.WriteLine("By Alex Apmann, David Jordan, & Joe Listro");
		}
	}
}
