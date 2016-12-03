using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMan.Domain
{
    class courseSectionHandler
    {
        private CourseID courseId;
        private int sectionId;

        private static courseSectionHandler instance = null;
        
        public courseSectionHandler() { }
        
        public static courseSectionHandler Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new courseSectionHandler();
                }
                return instance;
            }
        } 
    }
}
