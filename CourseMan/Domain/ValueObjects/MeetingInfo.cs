using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain.ValueObjects
{
	public class MeetingInfo
	{
        private List<MeetingTime> times;
		private Room room;


		public MeetingInfo()
		{
            times = new List<MeetingTime>();
		}
		
		public MeetingInfo(Room room, List<MeetingTime> times)
		{
			this.room = room;
            this.times = new List<MeetingTime>();
			this.times.AddRange(times);
		}


		void AddTime(MeetingTime time)
		{
			times.Add(time);
		}
		
		void Add(DayOfWeek dayOfWeek, int startTimeHours, int startTimeMinutes,
			int endTimeHours, int endTimeMinutes)
		{
			times.Add(new MeetingTime(dayOfWeek, startTimeHours,
				startTimeMinutes, endTimeHours, endTimeMinutes));
		}
		
		void Add(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
		{
			times.Add(new MeetingTime(dayOfWeek, startTime, endTime));
		}
		
        // Return whether this is currently meeting during the given date-time.
        public bool IsMeetingAt(DateTime dateTime)
        {
            foreach (MeetingTime meetigTime in times)
            {
                if (meetigTime.IsOccuringAt(dateTime))
                    return true;
            }
            return false;
        }
				
		// Return true if the given meeting time conflicts with this one (overlaps in time).
		public bool ConflictsWith(MeetingInfo other)
		{
			// Compare rooms.
			if (room != other.room)
				return false;

			// Compare meeting times.
			foreach (MeetingTime m1 in times)
			{
				foreach (MeetingTime m2 in other.times)
				{
					if (m1.ConflictsWith(m2))
						return true;
				}
			}

			return false;
		}
		

        public List<MeetingTime> Times
        {
            get { return times; }
        }
		
        public Room Room
        {
            get { return room; }
            set { room = value; }
        }
	}
}
