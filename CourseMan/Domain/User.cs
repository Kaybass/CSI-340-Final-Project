using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain
{
    public enum UserType { Student, Instructor, Administrator };
    public enum Department { CompSci, Engineering, Psychology, English, History }
	
    public class User
    {
        private int userId;
        private string username;
        private string password;
        private UserType type;
        private Department department; // department ≈ major

        public User()
        {
            // Constructor
        }

        public int UserID
        {
            get { return userId; }
            set { userId = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public UserType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Department Department
        {
            get { return department; }
            set { department = value; } 
        }
    }
}
