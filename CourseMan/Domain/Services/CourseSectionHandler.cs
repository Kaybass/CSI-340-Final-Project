using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using CourseMan.Domain.Entities;
using CourseMan.Domain.ValueObjects;

namespace CourseMan.Domain.Services
{
	// Services class which provides a facade to the repositories of
	// users, courses, and sections.
    public class CourseSectionHandler
    {
        private static CourseSectionHandler instance = null;

        private Dictionary<int, User> users;
        private Dictionary<CourseID, Course> courses;
        private Dictionary<SectionID, Section> sections;


		// Private default constructor, creating empty data sets.
        private CourseSectionHandler()
		{
			users = new Dictionary<int, User>();
			courses = new Dictionary<CourseID, Course>();
			sections = new Dictionary<SectionID, Section>();
		}

		
		// Add a new course to the system, validating its attributes and
		// throwing an exception if anything was invalid.
		public void AddCourse(Course course)
		{
			// Does this course already exist?
			if (courses.ContainsKey(course.CourseID))
			{
				throw new Exception("There is already a course with that ID");
			}
			
			courses[course.CourseID] = course;
		}

		// Add a new section to the system, validating its attributes and
		// throwing an exception if anything was invalid.
		public void AddSection(Section section)
		{
			// Does this section already exist?
			if (sections.ContainsKey(section.SectionID))
			{
				throw new Exception("There is already a section with that ID");
			}

			// Does the instructor exist?
			if (!users.ContainsKey(section.InstructorID))
			{
				throw new Exception("The instructor doesn't exist");
			}
			
			// Does the course exist?
			if (!courses.ContainsKey(section.CourseID))
			{
				throw new Exception("The course doesn't exist");
			}

			// Is the meeting times/room conflicting?
			foreach (Section s in sections.Values)
			{
				if (s.MeetingInfo.ConflictsWith(section.MeetingInfo))
				{
					throw new Exception("This section's meeting times conflict with another's");
				}
			}

			sections[section.SectionID] = section;
		}
		
		// Add a new user to the system, validating its attributes and
		// throwing an exception if anything was invalid.
		public void AddUser(User user)
		{
			// Does this section already exist?
			if (users.ContainsKey(user.UserID))
			{
				throw new Exception("There is already a user with that ID");
			}

			users[user.UserID] = user;
		}

		public void RemoveCourseAndItsSections(Course course)
		{
			courses.Remove(course.CourseID);

			// Remove all sections of this course.
			foreach (var section in sections.Where(kv =>
				kv.Key.CourseID == course.CourseID).ToList())
			{
				sections.Remove(section.Key);
			}
		}

		// Retrieve a course by its ID, returning null if it doesn't exist.
		public Course GetCourse(CourseID courseId)
		{
			if (courses.ContainsKey(courseId))
				return courses[courseId];
			return null;
		}
		
		// Retrieve a section by its ID, returning null if it doesn't exist.
		public Section GetSection(SectionID sectionId)
		{
			if (sections.ContainsKey(sectionId))
				return sections[sectionId];
			return null;
		}

		// Retrieve a user by its ID, returning null if it doesn't exist.
		public User GetUser(int userId)
		{
			if (users.ContainsKey(userId))
				return users[userId];
			return null;
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

        public int CurrentSystemUser
        {
            get { return CurrentSystemUser;  }
            set { CurrentSystemUser = value;  }
        }

		// Return the singleton instance for this class.
		public static CourseSectionHandler Instance
		{
			get
			{
				if (instance == null)
					instance = new CourseSectionHandler();
				return instance;
			}
		}
    }
}
