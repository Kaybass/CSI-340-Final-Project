using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CourseMan.Domain;
using CourseMan.Interface;
using CourseMan.Domain.Services;
using CourseMan.Domain.ValueObjects;
using CourseMan.Application;
using CourseMan.Application.Services;
using CourseMan.Application.ValueObjects;
using CourseMan.Infrastructure;

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
            data.addTheDataToTheThing();
			
			LoginMenu loginMenu = new LoginMenu();
			loginMenu.PerformPressAction();
        }
    }
}
