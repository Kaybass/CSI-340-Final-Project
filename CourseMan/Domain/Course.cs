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
        private int instructorId;
        private string name;
        private string description;


        public Course()
        {

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

        public int InstructorID
        {
            get { return instructorId; }
            set { instructorId = value; }
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
