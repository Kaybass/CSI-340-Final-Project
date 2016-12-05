using System;

namespace CourseMan.Domain.ValueObjects
{
	// Value object representing the time range and
	// day-of-the-week of when a section meets.
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
			return !(other.endTime <= startTime || other.startTime >= endTime);
		}

        public override string ToString()
        {
			int h1 = (startTime.Hours % 12);
			if (h1 == 0)
				h1 = 12;
			int h2 = (endTime.Hours % 12);
			if (h2 == 0)
				h2 = 12;

			return String.Format("{0}, {1}:{2,2:00} {3} - {4}:{5,2:00} {6}",
				dayOfWeek.ToString(),
				h1, startTime.Minutes, (startTime.Hours >= 12 ? "AM" : "PM"),
				h2, endTime.Minutes, (endTime.Hours >= 12 ? "AM" : "PM"));
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
