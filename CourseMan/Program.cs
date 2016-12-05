using System;
using CourseMan.Application;
using CourseMan.Interface;

namespace CourseMan
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			TestData data = new TestData();
            data.AddTestDataToSystem();
			
			LoginMenu loginMenu = new LoginMenu();
			loginMenu.PerformPressAction();
        }
    }
}
