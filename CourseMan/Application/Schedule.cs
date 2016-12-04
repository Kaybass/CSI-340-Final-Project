using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain;

namespace CourseMan.Application
{
    public class Schedule
    {
		private int userId;
        private List<Section> sections;


        public Schedule(int userId)
        {
			this.userId = userId;
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
		

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public List<Section> Sections
        {
            get { return sections; }
            set { sections = value; }
        }
    }
}
