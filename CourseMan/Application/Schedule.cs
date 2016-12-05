using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain.Entities;

namespace CourseMan.Application
{
	// Aggregate object representing a user's schedule of sections.
	// If the user is a student, then the schedule is a list of sections
	// he/she is registered for. If the user is a instructor, then the
	// schedule is a list of sections he/she is instructing.
    public class Schedule
    {
		private int userId;
        private List<Section> sections;


		// Constructor 
        public Schedule(int userId)
        {
			this.userId = userId;
			this.sections = new List<Section>();
        }

		// Return a list of scheduled sections for the given day of the week.
		public IEnumerable<Section> GetSectionsOnDayOfWeek(DayOfWeek dayOfWeek)
		{
			return sections.Where(section => section.MeetingInfo.Times
				.Exists(time => time.DayOfWeek == dayOfWeek));
		}

        // Check if any sections are currently meeting during the given date-time.
        public bool IsBusyAt(DateTime dateTime)
        {
			return sections.Exists(section => section.IsMeetingAt(dateTime));
        }
		

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public List<Section> Sections
        {
            get { return sections; }
        }
    }
}
