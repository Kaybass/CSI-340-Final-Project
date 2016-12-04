using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain.ValueObjects
{
    public struct MeetingTime
    {
        private DayOfWeek dayOfWeek;
        private TimeSpan startTime;
        private TimeSpan endTime;
        
		
        public MeetingTime(DayOfWeek dayOfWeek, int startTimeHours, int startTimeMinutes, int endTimeHours, int endTimeMinutes)
        {
            this.dayOfWeek = dayOfWeek;
            this.startTime = new TimeSpan(startTimeHours, startTimeMinutes, 0);
            this.endTime = new TimeSpan(endTimeHours, endTimeMinutes, 0);
        }

        public MeetingTime(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            this.dayOfWeek = dayOfWeek;
            this.startTime = startTime;
            this.endTime = endTime;
        }
        
        // Return true if this meeting time is occuring during the given date-time.
        public bool IsOccuringAt(DateTime dateTime)
        {
            return (dateTime.DayOfWeek == dayOfWeek &&
                    dateTime.TimeOfDay >= startTime &&
                    dateTime.TimeOfDay <= endTime);
        }

		// Return true if the given meeting time conflicts with this one (overlaps in time).
		public bool ConflictsWith(MeetingTime other)
		{
			return (other.endTime <= startTime || other.startTime >= endTime);
		}

        public override string ToString()
        {
            string output;

            output = startTime.ToString() + " - " + endTime.ToString() + ", " + dayOfWeek.ToString();

            return output;
        }


        public DayOfWeek DayOfWeek
        {
            get { return dayOfWeek; }
            set { dayOfWeek = value; }
        }

        public TimeSpan EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public TimeSpan StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public TimeSpan Duration
        {
            get { return endTime - startTime; }
        }
    }
}
