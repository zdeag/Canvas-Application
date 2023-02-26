using Class.Library.Canvas.Models.Courses;

using Class.Library.Canvas.Models.People;
using Class.Library.Canvas.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasApplication.Helpers
{
    internal class PersonHelper
    {
        private PersonService personService;
        private CourseService courseService;

        public PersonHelper()
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
        }

        public void CreatePersonRecord(Person? selectedPerson = null)
        {
            bool isCreate = false;
            if (selectedPerson == null)
            {
                isCreate = true;
                Console.WriteLine("What type of person would you like to add?");
                Console.WriteLine("(S)tudent");
                Console.WriteLine("(T)eachingAssistant");
                Console.WriteLine("(I)nstructor");
                var choice = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrEmpty(choice))
                {
                    return;
                }
                if (choice.Equals("S", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedPerson = new Student();
                }
                else if (choice.Equals("T", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedPerson = new TeachingAssistant();
                }
                else if (choice.Equals("I", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedPerson = new Instructor();
                }
            }

            Console.WriteLine("What is the ID of the Person?");
            var id = Console.ReadLine();
            Console.WriteLine("What is the Name of the Person?");
            var name = Console.ReadLine();
            if (selectedPerson is Student)
            {
                Console.WriteLine("What is the classification of the student? [(F)reshman, S(O)phomore, (J)unior, (S)enior]");
                var classification = Console.ReadLine() ?? string.Empty;
                PersonClassification classEnum = PersonClassification.Freshman;

                if (classification.Equals("O", StringComparison.InvariantCultureIgnoreCase))
                {
                    classEnum = PersonClassification.Sophomore;
                }
                else if (classification.Equals("J", StringComparison.InvariantCultureIgnoreCase))
                {
                    classEnum = PersonClassification.Junior;
                }
                else if (classification.Equals("S", StringComparison.InvariantCultureIgnoreCase))
                {
                    classEnum = PersonClassification.Senior;
                }
                var studentRecord = selectedPerson as Student;
                if (studentRecord != null)
                {
                    studentRecord.Classification = classEnum;
                    studentRecord.ID = int.Parse(id ?? "0");
                    studentRecord.Name = name ?? string.Empty;

                    if (isCreate)
                    {
                        personService.AddStudent(selectedPerson);
                    }
                }
            }
            else
            {
                if (selectedPerson != null)
                {
                    selectedPerson.ID = int.Parse(id ?? "0");
                    selectedPerson.Name = name ?? string.Empty;
                    if (isCreate)
                    {
                        personService.AddStudent(selectedPerson);
                    }
                }
            }
        }

        public void UpdateStudentRecord()
        {
            Console.WriteLine("Select a person to update:");
            personService.Students.ForEach(Console.WriteLine);

            var selectionStr = Console.ReadLine();

            if (int.TryParse(selectionStr, out int selectionInt))
            {
                var selectedStudent = personService.Students.FirstOrDefault(s => s.ID == selectionInt);
                if (selectedStudent != null)
                {
                    CreatePersonRecord(selectedStudent);
                }
            }
        }

        public void ListStudents()
        {
            personService.Students.ForEach(Console.WriteLine);

            Console.WriteLine("Select a student:");
            var selectionStr = Console.ReadLine();
            var selectionInt = int.Parse(selectionStr ?? "0");

            Console.WriteLine("Student Course List:");
            courseService.Courses.Where(c => c.Roster.Any(s => s.ID == selectionInt)).ToList().ForEach(Console.WriteLine);
        }

        public void SearchStudents()
        {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;

            personService.Search(query).ToList().ForEach(Console.WriteLine);
            var selectionStr = Console.ReadLine();
            var selectionInt = int.Parse(selectionStr ?? "0");

            Console.WriteLine("Student Course List:");
            courseService.Courses.Where(c => c.Roster.Any(s => s.ID == selectionInt)).ToList().ForEach(Console.WriteLine);
        }
    }
}