using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain;
using CourseMan.Domain.Services;
using CourseMan.Domain.ValueObjects;
using CourseMan.Infrastructure;

namespace CourseMan.Interface
{
	public class LoginMenu : SubMenu
	{
		private AdminConsole   adminConsole;
        private StudentConsole studentConsole;
		

		public LoginMenu()
		{
			adminConsole = new AdminConsole();
            studentConsole = new StudentConsole();


			Text = "Welcome to the CourseMan! Please login to continue.";
			AddMenuAction('L', "Log In", PromptLogin);
			AddMenuAction('A', "About", ShowAboutInfo);
			AddExitItem('Q', "Quit");
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
                        Console.WriteLine("TODO");
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
