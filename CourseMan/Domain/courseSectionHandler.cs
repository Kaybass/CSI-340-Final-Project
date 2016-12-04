using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using CourseMan.Domain.ValueObjects;

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
			// Does this course already exist?
			if (courses.ContainsKey(course.CourseID))
			{
				throw new Exception("There is already a course with that ID");
			}
			
			courses[course.CourseID] = course;
		}
		
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
		
		public void AddUser(User user)
		{
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

		public Course GetCourse(CourseID courseId)
		{
			if (courses.ContainsKey(courseId))
				return courses[courseId];
			return null;
		}

		public Section GetSection(SectionID sectionId)
		{
			if (sections.ContainsKey(sectionId))
				return sections[sectionId];
			return null;
		}

		public User GetUser(int userId)
		{
			if (users.ContainsKey(userId))
				return users[userId];
			return null;
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
