using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseMan.Infrastructure
{
	// A menu item which will enter a sub-menu.
	// This is the composite part of the Composite Pattern.
	public class SubMenu : IMenuItem
	{
		private string text; // Test displayed before the list of items.
		private List<Option> items;
		private bool isRunning;

		// Simple class internally representing a menu item, with its
		// corresponding name and letter shortcut.
		private class Option
		{
			public char Letter { get; set; }
			public string Name { get; set; }
			public IMenuItem Item { get; set; }

			public Option(char letter, string name, IMenuItem item)
			{
				this.Letter = letter;
				this.Name = name;
				this.Item = item;
			}
		}
		
		public SubMenu()
		{
			this.text = "";
			this.items = new List<Option>();
			this.isRunning = false;
		}

		// Create a sub-menu given its text displayed above the list of items.
		public SubMenu(string text)
		{
			this.text = text;
			this.items = new List<Option>();
			this.isRunning = false;
		}

		// Open the sub-menu, letting the user choose from its items.
		public void PerformPressAction()
		{
			Console.Clear();
			Console.WriteLine();
			isRunning = true;

			// Allow menu interactions repeatedly, until the menu is exited.
			while (isRunning)
			{
				Console.WriteLine(text);

				// Print out each menu item.
				for (int i = 0; i < items.Count; i++)
					Console.WriteLine("{0}: {1}", char.ToUpper(items[i].Letter), items[i].Name);

				// Prompt the user for an item number.
				Console.Write("> ");
				string input = Console.ReadLine();
				Console.WriteLine();

				if (input.Length > 0)
				{
					// Find an option with a matching letter.
					Option option = items.FirstOrDefault(it =>
						char.ToLower(it.Letter) == char.ToLower(input[0]));

					// Perform the option's action if it exists.
					if (option != null)
						option.Item.PerformPressAction();
					else
						Console.WriteLine("Invalid option '{0}'!", input);
				}

				if (isRunning)
					Console.WriteLine();
			}
		}
		
		// Add a menu item which will enter a sub-menu.
		public void AddSubMenu(char letter, string name, SubMenu subMenu)
		{
			AddMenuItem(letter, name, subMenu);
		}
		
		// Add a menu item which will perform a single action.
		public void AddMenuAction(char letter, string name, MenuActionDelegate action)
		{
			AddMenuItem(letter, name, new MenuAction(action));
		}

		// Add a menu item which exits this sub-menu.
		public void AddExitItem(char letter, string name)
		{
			AddMenuAction(letter, name, this.ExitMenu);
		}
		
		// Add a menu item.
		public void AddMenuItem(char letter, string name, IMenuItem item)
		{
			items.Add(new Option(letter, name, item));
		}

		// Exit the menu, stopping it from running. If a previous menu
		// entered into this sub-menu, then the interface will return
		// to the previous menu.
		public void ExitMenu()
		{
			isRunning = false;
			Console.Clear();
		}

		// Enter into a sub-menu, running it on top of this one until it is exited.
		public void EnterSubMenu(SubMenu subMenu)
		{
			subMenu.PerformPressAction();
		}

		
		public string Text
		{
			get { return text; }
			set { text = value; }
		}
	}
}
