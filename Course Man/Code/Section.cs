﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan
{
    public class Section
    {
        private int sectionId; // 2 digit section ID number
        private CourseID courseId;
        private Room room;
        private List<MeetingTime> meetingTimes;


        public Section()
        {
            meetingTimes = new List<MeetingTime>();
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
            return String.Format("{0}-{1,2:00}", courseId.ToString(), sectionId);
        }

        public int SectionID
        {
            get { return sectionId; }
            set { sectionId = value; }
        }

        public CourseID CourseID
        {
            get { return courseId; }
            set { courseId = value; }
        }

        public Room Room
        {
            get { return room; }
            set { room = value; }
        }

        public List<MeetingTime> MeetingTimes
        {
            get { return meetingTimes; }
            set { meetingTimes = value; }
        }
    }
}
