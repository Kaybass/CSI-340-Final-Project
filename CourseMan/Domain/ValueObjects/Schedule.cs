using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain
{
    public class Schedule
    {
		private int studentId;
        private List<Section> sections;


        public Schedule(int studentId)
        {
			this.studentId = studentId;
			this.sections = new List<Section>();
        }


        // Check if any sections are currently meeting during the given date-time.
        public bool IsBusyAt(DateTime dateTime)
        {
            foreach (Section section in sections)
            {
                if (section.IsMeetingAt(dateTime))
                    return true;
            }
            return false;
        }
		

        public int StudentID
        {
            get { return studentId; }
            set { studentId = value; }
        }

        public List<Section> Sections
        {
            get { return sections; }
            set { sections = value; }
        }
    }
}
