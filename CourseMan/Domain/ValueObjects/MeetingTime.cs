using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain
{
    public struct MeetingTime
    {
        private DayOfWeek dayOfWeek;
        private TimeSpan startTime;
        private TimeSpan endTime;
        

        public MeetingTime(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            this.dayOfWeek = dayOfWeek;
            this.startTime = startTime;
            this.endTime = endTime;
        }
        
        // Check if this meeting time is occuring during the given date-time.
        public bool IsOccuringAt(DateTime dateTime)
        {
            return (dateTime.DayOfWeek == dayOfWeek &&
                    dateTime.TimeOfDay >= startTime &&
                    dateTime.TimeOfDay <= endTime);
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
