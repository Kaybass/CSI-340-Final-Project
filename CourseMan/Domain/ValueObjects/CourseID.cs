using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain
{
    public struct CourseID
    {
        private string majorCode; // 3-letter major code (ex: CSI)
        private int courseNumber; // 3-digit course number (unique per major code).


        public CourseID(string majorCode, int courseNumber)
        {
            this.majorCode = majorCode;
            this.courseNumber = courseNumber;
        }


        public override string ToString()
        {
            return String.Format("{0}-{1,3:000}", majorCode, courseNumber);
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
