
namespace CourseMan.Infrastructure
{
	// An interface for defining a menu item that has an action when pressed.
	// This is the Component part of the Composite Pattern.
	public interface IMenuItem
	{
		// Called when the item is pressed within a menu.
		void PerformPressAction();
	}
}
