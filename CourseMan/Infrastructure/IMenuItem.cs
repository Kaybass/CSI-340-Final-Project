using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Infrastructure
{
	// An interface for defining a menu item that has an action when pressed.
	// The component part of the Composite Pattern
	public interface IMenuItem
	{
		void PerformPressAction();
	}
}
