using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain.ValueObjects;

namespace CourseMan.Domain.Services
{
	public class RegistrationException : Exception
	{
		public RegistrationException(string message) :
			base(message)
		{
		}
	}


	public class RegistrationService
	{
        private static RegistrationService instance = null;


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
				throw new RegistrationException("Error: student is already registed for this section!");

			// Check if there are no empty seats.
			if (section.IsFull)
				throw new RegistrationException("Error: no seats available for this section!");

			// TODO: Check for conflicting meeting times.

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

		
		public static RegistrationService Instance
		{
			get
			{
				if (instance == null)
					instance = new RegistrationService();
				return instance;
			}
		}
	}
}
