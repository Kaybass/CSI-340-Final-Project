using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain;
using CourseMan.Domain.Services;
using CourseMan.Domain.ValueObjects;
using CourseMan.Application.ValueObjects;

namespace CourseMan.Application.Services
{
	// Service object used to gather scheduling/timing information, including 
	// creating objects for student and instructor schedules, and determining
	// when rooms are in use at a given time.
	public class ScheduleService
	{
		public ScheduleService()
		{
		}


		// Get the schedule of a student, containing his/her currently registered sections.
		public Schedule GetStudentSchedule(int studentId)
		{
			CourseSectionHandler csh = CourseSectionHandler.Instance;
			
			// Add all sections containing the given student.
			Schedule schedule = new Schedule(studentId);
			schedule.Sections.AddRange(csh.Sections.Values.Where(
				section => section.IsStudentRegistered(studentId)));
			
			return schedule;
		}
		
		// Get the schedule of a instructer, containing his/her currently registered sections.
		public Schedule GetInstructerSchedule(int instructerId)
		{
			CourseSectionHandler csh = CourseSectionHandler.Instance;
			
			// Add all sections instructed by the given instructer.
			Schedule schedule = new Schedule(instructerId);
			schedule.Sections.AddRange(csh.Sections.Values.Where(
				section => section.InstructorID == instructerId));
			
			return schedule;
		}
		
		// Return true if the room is occupied at the given time.
		public bool IsRoomInUse(Room room, DateTime dateTime)
		{
			foreach (Section section in CourseSectionHandler.Instance.Sections.Values)
			{
				if (section.Room == room && section.IsMeetingAt(dateTime))
					return true;
			}
			return false;
		}
	}
}
