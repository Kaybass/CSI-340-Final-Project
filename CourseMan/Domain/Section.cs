using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain
{
    public class Section
    {
        private SectionID sectionId;
        private Room room;
        private List<MeetingTime> meetingTimes;
		private HashSet<int> registeredStudentIds;
		private int maxSeats;


        public Section()
        {
            meetingTimes = new List<MeetingTime>();
			registeredStudentIds = new HashSet<int>();
        }

        public Section(SectionID sId, Room sRoom, List<MeetingTime> sTimes)
        {
            sectionId = sId;
            room = sRoom;
            meetingTimes = new List<MeetingTime>();
        }


        // Return whether the given student is currently registered for this section.
        public bool IsStudentRegistered(int studentId)
		{
			return registeredStudentIds.Contains(studentId);
		}

        // Check if the section is currently meeting during the given date-time.
        public bool IsMeetingAt(DateTime dateTime)
        {
            foreach (MeetingTime meetigTime in meetingTimes)
            {
                if (meetigTime.IsOccuringAt(dateTime))
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            return sectionId.ToString();
        }


        public SectionID SectionID
        {
            get { return sectionId; }
            set { sectionId = value; }
        }

        public CourseID CourseID
        {
            get { return sectionId.CourseID; }
            set { sectionId.CourseID = value; }
        }

        public Room Room
        {
            get { return room; }
            set { room = value; }
        }

        public List<MeetingTime> MeetingTimes
        {
            get { return meetingTimes; }
        }

        public ISet<int> RegisteredStudents
        {
            get { return registeredStudentIds; }
        }

		public int MaxSeats
		{
            get { return maxSeats; }
            set { maxSeats = value; }
		}
		
		public int AvailableSeats
		{
            get { return maxSeats - registeredStudentIds.Count; }
		}
    }
}
