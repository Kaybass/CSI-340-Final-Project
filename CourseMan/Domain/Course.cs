using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain.ValueObjects;

namespace CourseMan.Domain
{
	// Entity class representing a course which that a school offers.
	// An instances of a course offering is called a Section.
    public class Course
    {
        private CourseID courseId;
        private string name;
        private string description;

		
		// Default Constructor
        public Course()
        {
        }

        public Course(CourseID id, string name, string description)
        {
            this.courseId = id;
            this.name = name;
            this.description = description;
        }

        public override string ToString()
        {
            return courseId.ToString();
        }


        public CourseID CourseID
        {
            get { return courseId; }
            set { courseId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
