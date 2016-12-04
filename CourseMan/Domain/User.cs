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
		private string firstName;
		private string lastName;
        private UserType type;
        private Department department; // department = major

        public User()
        {
            // Constructor
        }

        public User(int id, string username, string password, string type, string department)
        {
            this.userId = id;
            this.username = username;
            this.password = password;
            switch (type)
            {
                case "Student":
                    this.type = UserType.Student;
                    break;
                case "Instructor":
                    this.type = UserType.Instructor;
                    break;
                case "Administrator":
                    this.type = UserType.Administrator;
                    break;
            }

            switch (department)
            {
                case "CompSci":
                    this.department = Department.CompSci;
                    break;
                case "Engineering":
                    this.department = Department.Engineering;
                    break;
                case "Psychology":
                    this.department = Department.Psychology;
                    break;
                case "English":
                    this.department = Department.English;
                    break;
                case "History":
                    this.department = Department.History;
                    break;
            }
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

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
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
