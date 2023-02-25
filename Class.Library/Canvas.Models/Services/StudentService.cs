using Class.Library.Canvas.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Services
{
    public class StudentService
    {
        private List<Person> studentList;

        private static StudentService? _instance;

        private StudentService() 
        {
            studentList = new List<Person>();
        }

        public static StudentService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StudentService();
                }

                return _instance;
            }
        }

        public void AddStudent(Person student)
        {
            studentList.Add(student);
        }

        public List<Person> Students
        {
            get
            {
                return studentList;
            }
        }

        public IEnumerable<Person> Search(string query)
        {
            return studentList.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }

    }
}
