using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain
{
	public class RegistrationService
	{
		public RegistrationService()
		{
		}

		public void Register(int studentId, SectionID sectionId)
		{
			// Find the section.
			Section section = CourseSectionHandler.Instance.Sections[sectionId];

			// Find the student.
			User student = CourseSectionHandler.Instance.Users[studentId];

			// Check if the student is already registered for this section.
			if (section.RegisteredStudents.Contains(studentId))
				throw new Exception("Error: student is already registed for this section!");

			// Check if there are no empty seats.
			if (section.IsFull)
				throw new Exception("Error: no seats available for this section!");

			// Register the student.
			section.RegisteredStudents.Add(studentId);
		}
		
		public void UnRegister(int studentId, SectionID sectionId)
		{
			// Find the section.
			Section section = CourseSectionHandler.Instance.Sections[sectionId];

			// Check if the student is already registered for this section.
			if (!section.RegisteredStudents.Contains(studentId))
				throw new Exception("Error: student is not registed for this section!");
			
			// Unregister the student.
			section.RegisteredStudents.Remove(studentId);
		}

		// Get the schedule of a student TODO: move this into a different place in the code.
		public Schedule GetStudentSchedule(int studentId)
		{
			CourseSectionHandler csh = CourseSectionHandler.Instance;
			
			// Add all sections containing the given student.
			Schedule schedule = new Schedule(studentId);
			schedule.Sections.AddRange(csh.Sections.Values.Where(
				section => section.IsStudentRegistered(studentId)));
			
			return schedule;
		}
	}
}
