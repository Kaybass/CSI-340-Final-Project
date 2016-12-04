using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain.ValueObjects;

namespace CourseMan.Domain
{
	// Entity class representing an instance of a course. Courses
	// are offered as multiple sections which students can register for,
	// each being able to have different instructors and meeting times/places.
    public class Section
    {
        private SectionID sectionId;
        private MeetingInfo meetingInfo;
		private int maxSeats;
		private HashSet<int> registeredStudentIds;
		private int instructorId;


        public Section()
        {
            meetingInfo = new MeetingInfo();
			registeredStudentIds = new HashSet<int>();
        }

        public Section(SectionID id, Room room, List<MeetingTime> times,int instructorid, int seats)
        {
            sectionId = id;
            meetingInfo = new MeetingInfo(room, times);
            registeredStudentIds = new HashSet<int>();
            instructorId = instructorid;
            maxSeats = seats;
        }


        // Return whether the given student is currently registered for this section.
        public bool IsStudentRegistered(int studentId)
		{
			return registeredStudentIds.Contains(studentId);
		}

        // Return whether the section is currently meeting during the given date-time.
        public bool IsMeetingAt(DateTime dateTime)
        {
            foreach (MeetingTime meetigTime in meetingInfo.Times)
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
            get { return meetingInfo.Room; }
            set { meetingInfo.Room = value; }
        }

        public int InstructorID
        {
            get { return instructorId; }
            set { instructorId = value; }
        }

        public MeetingInfo MeetingInfo
        {
            get { return meetingInfo; }
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
