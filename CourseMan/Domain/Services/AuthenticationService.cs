using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain.Services
{
	public class AuthenticationService
	{
        private static AuthenticationService instance = null;

		private int loggedInUserId;


		private AuthenticationService()
		{
			loggedInUserId = -1;
		}


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

		public void LogOut()
		{
			loggedInUserId = -1;
		}


		public bool IsLoggedIn
		{
			get { return (loggedInUserId != -1); }
		}

		public User LoggedInUser
		{
			get
			{
				return CourseSectionHandler.Instance.GetUser(loggedInUserId);
			}
		}

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
