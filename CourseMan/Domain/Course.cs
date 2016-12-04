using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain
{
    public class Course
    {
        private CourseID courseId;
        private string name;
        private string description;


        public Course()
        {
            // Default Constructor
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
