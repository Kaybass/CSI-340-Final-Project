using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain.Entities;

namespace CourseMan.Domain.Services
{
	// Service singleton object used for authentication. Through this 
	// interface, you can log in and log out as a user.
	public class AuthenticationService
	{
        private static AuthenticationService instance = null;

		private int loggedInUserId;


		// Constructor is private because this is meant to be a singleton.
		private AuthenticationService()
		{
			loggedInUserId = -1;
		}
		
		// Attempt to log in as a user, provided a username and password,
		// returns true if the login was successful.
		public bool LogIn(string username, string password)
		{
			// Must log out first.
			if (IsLoggedIn)
				return false;

			// Find a user with the given username and password.
			User loggedInUser = CourseSectionHandler.Instance.Users.Values.FirstOrDefault(
				user => (user.Username == username && user.Password == password));

			if (loggedInUser != null)
			{
				loggedInUserId = loggedInUser.UserID;
				return true;
			}

			return false;
		}

		// Log the current user out. This must be done in order to log in again.
		public void LogOut()
		{
			loggedInUserId = -1;
		}


		public bool IsLoggedIn
		{
			get { return (loggedInUserId != -1); }
		}

		// Retreive the user who is currently logged in, or null if no one is.
		public User LoggedInUser
		{
			get { return CourseSectionHandler.Instance.GetUser(loggedInUserId); }
		}
		
		// Return the singleton instance for this class.
		public static AuthenticationService Instance
		{
			get
			{
				if (instance == null)
					instance = new AuthenticationService();
				return instance;
			}
		}

	}
}
