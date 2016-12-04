using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CourseMan.Domain
{
    class CourseSectionHandler
    {
        private List<User> Users;

        private List<Course> Courses;

        private List<Section> Sections;

        private static CourseSectionHandler instance = null;
        
        private CourseSectionHandler() { }
        
        public static CourseSectionHandler getInstance()
        {

           if(instance == null)
           {
                instance = new CourseSectionHandler();
           }
           return instance;

        } 

        public void setUsers(List<User> users)
        {
            Users = users;
        }
        public List<User> getUsers()
        {
            return Users;
        }

        public void setCourses(List<Course> courses)
        {
            Courses = courses;
        }
        public List<Course> getCourses()
        {
            return Courses;
        }

        public void setSections(List<Section> sections)
        {
            Sections = sections;
        }
        public List<Section> getSections()
        {
            return Sections;
        }
    }
}
