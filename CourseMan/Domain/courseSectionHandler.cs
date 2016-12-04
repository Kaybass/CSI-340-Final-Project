using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CourseMan.Domain
{
    public class CourseSectionHandler
    {
        private Dictionary<int, User> users;

        private Dictionary<CourseID, Course> courses;

        private Dictionary<SectionID, Section> sections;

        private static CourseSectionHandler instance = null;
        

        private CourseSectionHandler()
		{
			users = new Dictionary<int, User>();
			courses = new Dictionary<CourseID, Course>();
			sections = new Dictionary<SectionID, Section>();
		}


		public void AddCourse(Course course)
		{
			courses[course.CourseID] = course;
		}
		
		public void AddSection(Section section)
		{
			sections[section.SectionID] = section;
		}
		
		public void AddUser(User user)
		{
			users[user.UserID] = user;
		}


		public static CourseSectionHandler Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new CourseSectionHandler();
				}
				return instance;
			}
		}

		public Dictionary<int, User> Users
        {
            get { return users; }
            set { users = value; }
        }

		public Dictionary<CourseID, Course> Courses
        {
            get { return courses; }
            set { courses = value; }
        }

		public Dictionary<SectionID, Section> Sections
        {
            get { return sections; }
            set { sections = value; }
        }
    }
}
