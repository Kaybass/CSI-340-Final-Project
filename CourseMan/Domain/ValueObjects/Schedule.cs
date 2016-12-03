using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain
{
    public class Schedule
    {
        public List<Section> sections;


        public Schedule()
        {
			sections = new List<Section>();
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


        public List<Section> Sections
        {
            get { return sections; }
            set { sections = value; }
        }
    }
}
