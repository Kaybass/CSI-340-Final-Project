using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain.ValueObjects;

namespace CourseMan.Domain
{
    public class Section
    {
        private SectionID sectionId;
        private Room room;
		private int maxSeats;
        private List<MeetingTime> meetingTimes;
		private HashSet<int> registeredStudentIds;
		private int instructorId;


        public Section()
        {
            meetingTimes = new List<MeetingTime>();
			registeredStudentIds = new HashSet<int>();
        }

        public Section(SectionID id, Room room, List<MeetingTime> times)
        {
            this.sectionId = id;
            this.room = room;
            this.meetingTimes = new List<MeetingTime>();
			this.meetingTimes.AddRange(times);
        }


        // Return whether the given student is currently registered for this section.
        public bool IsStudentRegistered(int studentId)
		{
			return registeredStudentIds.Contains(studentId);
		}

        // Return whether the section is currently meeting during the given date-time.
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

        public int InstructorID
        {
            get { return instructorId; }
            set { instructorId = value; }
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

		public bool IsFull
		{
			get { return (AvailableSeats == 0); }
		}
    }
}
