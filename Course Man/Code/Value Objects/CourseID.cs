using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan
{
    public struct CourseID
    {
        private string majorCode; // 3-letter major code (ex: CSI)
        private int courseId; // 3-digit course ID (unique per major code).


        public CourseID(string majorCode, int courseId)
        {
            this.majorCode = majorCode;
            this.courseId = courseId;
        }


        public override string ToString()
        {
            return String.Format("{0}-{1}", majorCode, courseId);
        }


        public string MajorCode
        {
            get { return majorCode; }
            set { majorCode = value; }
        }

        public int CourseId
        {
            get { return courseId; }
            set { courseId = value; }
        }
    }
}
