using Class.Library.Canvas.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Services
{
    public class PersonService
    {
        private List<Person> personList;

        private static PersonService? _instance;

        private PersonService()
        {
            personList = new List<Person>();
        }

        public static PersonService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PersonService();
                }

                return _instance;
            }
        }

        public void AddStudent(Person student)
        {
            personList.Add(student);
        }

        public List<Student> Students
        {
            get
            {
                return personList.OfType<Student>().ToList();
            }
        }

        public List<Instructor> Instructors
        {
            get
            {
                return personList.OfType<Instructor>().ToList();
            }
        }

        public List<TeachingAssistant> TeachingAssistants
        {
            get
            {
                return personList.OfType<TeachingAssistant>().ToList();
            }
        }


        public IEnumerable<Person> Search(string query)
        {
            return personList.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }

    }
}
