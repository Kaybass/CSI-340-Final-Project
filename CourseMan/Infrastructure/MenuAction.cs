using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Infrastructure
{
	public delegate void MenuActionDelegate();
	
	// The leaf part of the Composite Pattern
	public class MenuAction : IMenuItem
	{
		private MenuActionDelegate action;

		public MenuAction(MenuActionDelegate action)
		{
			this.action = action;
		}

		public void PerformPressAction()
		{
			action.Invoke();
		}
		
		public MenuActionDelegate Action
		{
			get { return action; }
			set { action = value; }
		}
	}
}
