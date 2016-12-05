using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain.ValueObjects
{
	// Value object used to identify courses.
    public struct CourseID : IComparable<CourseID>
    {
        private string majorCode; // 3-letter major code (ex: CSI)
        private int courseNumber; // 3-digit course number (unique per major code).


        public CourseID(string majorCode, int courseNumber)
        {
            this.majorCode = majorCode;
            this.courseNumber = courseNumber;
        }
		
		// Convert a course ID to a string, combining the major code and
		// course number separated by a dash (ex: CSI-340)
        public override string ToString()
        {
            return String.Format("{0}-{1,3:000}", majorCode, courseNumber);
        }

		public override bool Equals(object obj)
		{
			return (obj is CourseID && (CourseID) obj == this);
		}

		public override int GetHashCode()
		{
			int hash = 13;
			hash = (hash * 7) + majorCode.GetHashCode();
			hash = (hash * 7) + courseNumber;
			return hash;
		}

		public int CompareTo(CourseID other)
		{
			// Order By:
			// 1. Major code.
			int returnVal = majorCode.CompareTo(other.MajorCode);
			if (returnVal != 0)
				return returnVal;

			// 2. Course number.
			return courseNumber.CompareTo(other.CourseNumber);
		}

		public static bool operator ==(CourseID a, CourseID b)
		{
			return (a.majorCode == b.majorCode &&
					a.courseNumber == b.courseNumber);
		}

		public static bool operator !=(CourseID a, CourseID b)
		{
			return !(a == b);
		}


        public string MajorCode
        {
            get { return majorCode; }
            set { majorCode = value; }
        }

        public int CourseNumber
        {
            get { return courseNumber; }
            set { courseNumber = value; }
        }
    }
}
