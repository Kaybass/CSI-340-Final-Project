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

		public bool Register(int studentId, SectionID sectionId)
		{
			// Find the section.
			Section section = CourseSectionHandler.Instance.Sections[sectionId];

			// Find the student.
			User student = CourseSectionHandler.Instance.Users[studentId];

			// Check if the student is already registered for this section.
			if (section.RegisteredStudents.Contains(studentId))
				throw new Exception("Error: student is already registed for this section!");

			// Check if there is an empty seat.
			if (section.AvailableSeats == 0)
				throw new Exception("Error: no seats available for this section!");

			// Register the student.
			section.RegisteredStudents.Add(studentId);

			return true;
		}
		
		bool UnRegister(int studentId, SectionID sectionId)
		{
			// TODO

			return true;
		}
	}
}
