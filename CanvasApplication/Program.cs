using App.LearningManagement.Helpers;
using CanvasApplication.Helpers;
using Class.Library.Canvas.Models.Courses;
using Class.Library.Canvas.Models.People;
using System.Globalization;

namespace CanvasApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var personHelper = new PersonHelper();
            var courseHelper = new CourseHelper();
            bool cont = true;

            while (cont)
            {
                Console.WriteLine("1. Access Persons");
                Console.WriteLine("2. Access Courses");
                Console.WriteLine("3. Exit");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    if (result == 1)
                    {
                        ShowPersonMenu(personHelper);
                    }
                    else if (result == 2)
                    {
                        ShowCourseMenu(courseHelper);
                    }
                    else if (result == 3)
                    {
                        cont = false;
                    }
                }

            }

        }
        static void ShowPersonMenu(PersonHelper personHelper)
        {
            Console.WriteLine("Choose an action:");
            Console.WriteLine("1. Add a New Person");
            Console.WriteLine("2. Update a Person");
            Console.WriteLine("3. List all People");
            Console.WriteLine("4. Search for a Person");

            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    personHelper.CreatePersonRecord();
                }
                else if (result == 2)
                {
                    personHelper.UpdateStudentRecord();
                }
                else if (result == 3)
                {
                    personHelper.ListStudents();
                }
                else if (result == 4)
                {
                    personHelper.SearchStudents();
                }
            }
        }

        static void ShowCourseMenu(CourseHelper courseHelper)
        {
            Console.WriteLine("1. Add a New Course");               //course
            Console.WriteLine("2. Update a Course");
            Console.WriteLine("3. Update Student on Course");       //course
            Console.WriteLine("4. Update Assignment Groups on Course");
            Console.WriteLine("5. Update Assignments on Course");
            Console.WriteLine("6. Update Model on Course");
            Console.WriteLine("7. List all courses");               //course
            Console.WriteLine("8. Search for a course");            //course


            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    courseHelper.CreateCourse();
                }
                else if (result == 2)
                {
                    courseHelper.UpdateCourse();
                }
                else if (result == 3)
                {
                    courseHelper.UpdateStudentMenu();
                }
                else if (result == 4)
                {
                    // Assignment Groups
                }
                else if (result == 5)
                {
                    courseHelper.UpdateAssignmentMenu();
                }
                else if (result == 6)
                {

                }
                else if (result == 7)
                {
                    courseHelper.SearchCourses();
                }
                else if (result == 8)
                {
                    Console.WriteLine("Enter a query:");
                    var query = Console.ReadLine() ?? string.Empty;
                    courseHelper.SearchCourses(query);
                }
            }
        }
    }
}