using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMan.Domain.Entities;
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

		// Register a student for a section.
		// If an error is encountered, a RegistrationException will be thrown.
		public void Register(int studentId, SectionID sectionId)
		{
			// Find the section.
			Section section = CourseSectionHandler.Instance.GetSection(sectionId);
			if (section == null)
				throw new RegistrationException("section " + sectionId + " does not exist.");

			// Find the student.
			User student = CourseSectionHandler.Instance.GetUser(studentId);
			if (student == null)
				throw new RegistrationException("student ID " + studentId + " does not exist.");

			// Check if the student is already registered for this section.
			if (section.RegisteredStudents.Contains(studentId))
				throw new RegistrationException("student is already registed for this section");

			// Check if there are no empty seats.
			if (section.IsFull)
				throw new RegistrationException("no seats available for this section");

			// TODO: Check for conflicting meeting times.

			// Register the student.
			section.RegisteredStudents.Add(studentId);
		}
		
		// Un-Register a student for a section.
		// If an error is encountered, a RegistrationException will be thrown.
		public void UnRegister(int studentId, SectionID sectionId)
		{
			// Find the section.
			Section section = CourseSectionHandler.Instance.GetSection(sectionId);
			if (section == null)
				throw new RegistrationException("section " + sectionId + " does not exist.");

			// Check if the student is already registered for this section.
			if (!section.RegisteredStudents.Contains(studentId))
				throw new RegistrationException("student is not registed for this section");
			
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
