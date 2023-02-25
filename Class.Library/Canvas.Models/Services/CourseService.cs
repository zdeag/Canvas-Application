using Class.Library.Canvas.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Services
{
    public class CourseService
    {
        private List<Course> CourseList;
        private static CourseService? _instance;

        public static CourseService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CourseService();
                }

                return _instance;
            }
        }

        private CourseService() 
        {
            CourseList = new List<Course>();
        }

        public void AddCourse(Course course)
        {
            CourseList.Add(course);
        }

        public List<Course> Courses
        {
            get
            {
                return CourseList;
            }
        }

        public IEnumerable<Course> Search(string query)
        {
            return Courses.Where(s => s.Name.ToUpper().Contains(query.ToUpper())
                || s.Description.ToUpper().Contains(query.ToUpper())
                || s.Code.ToUpper().Contains(query.ToUpper()));
        }

    }
}
