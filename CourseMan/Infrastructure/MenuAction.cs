
namespace CourseMan.Infrastructure
{
	public delegate void MenuActionDelegate();
	
	// A menu item which performs a single action provided as a function delegate.
	// This is the Leaf part of the Composite Pattern.
	public class MenuAction : IMenuItem
	{
		private MenuActionDelegate action;

		public MenuAction(MenuActionDelegate action)
		{
			this.action = action;
		}

		// Perform the press action, which is a function delegate.
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
