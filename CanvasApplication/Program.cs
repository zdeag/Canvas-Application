using Class.Library.Canvas.Models.Courses;
using Class.Library.Canvas.Models.People;
using System.Globalization;

namespace CanvasApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var peopleList = new List<Person>();
            var courseList = new List<Course>();

            bool status = true;

            while (status)
            {
                Console.WriteLine("- Canvas Application -\n=======================");
                Console.WriteLine("1. Create a Course");
                Console.WriteLine("2. Create a Student");
                Console.WriteLine("3. Edit Course");
                Console.WriteLine("4. Edit Student");
                Console.WriteLine("5. List all Courses");
                Console.WriteLine("6. List all Students");
                Console.WriteLine("7. Search for Course");
                Console.WriteLine("8. Search for Student");
                Console.WriteLine("9. List Student's Courses (Schedule)");
                Console.WriteLine("10. Exit");
                Console.WriteLine("=======================\nChoose an option: ");

                string userInput = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(userInput, out int userOutput))
                {
                    if (userOutput == 1)
                    {
                        var newCourse = new Course();

                        Console.WriteLine();
                        Console.WriteLine("Please input a Course Code: ");

                        newCourse.Code = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Please input a Course Name: ");

                        newCourse.Name = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Please input a Course Description: ");

                        newCourse.Description = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine();

                        courseList.Add(newCourse);
                    }
                    else if (userOutput == 2)   // Handles a creation of Person base class
                    {
                        var newPerson = new Student();

                        Console.WriteLine("\nPlease input Student's Name:");

                        newPerson.Name = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Please input Student's ID: ");

                        newPerson.Id = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Please input Student's Classification (Freshman, Sophmore, Junior, Senior): ");

                        newPerson.Classification = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Please input Student Description: ");

                        newPerson.Description = Console.ReadLine() ?? string.Empty;

                        peopleList.Add(newPerson);
                    }
                    else if (userOutput == 3)
                    {
                        Console.WriteLine("\n  - Class Editor - \n=====================");
                        Console.WriteLine("1. Edit Course Code");
                        Console.WriteLine("2. Edit Course Name");
                        Console.WriteLine("3. Edit Course Description");
                        Console.WriteLine("4. Add Student to Roster");
                        Console.WriteLine("5. Remove Student from Roster");
                        Console.WriteLine("6. Add an Assignment");
                        Console.WriteLine("Make a Selection: ");

                        string input = Console.ReadLine() ?? string.Empty;

                        if (int.TryParse(input, out int output))
                        {

                            Console.WriteLine("Please input Course Name or Course Description: ");

                            var query = Console.ReadLine() ?? string.Empty;

                            var filteredList = courseList.Where(t => (t.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase) || t.Description.Contains(query, StringComparison.InvariantCultureIgnoreCase)));
                            Console.WriteLine();
                            Console.WriteLine("Filtered List:");
                            filteredList.ToList().ForEach(t => Console.WriteLine(t.SoftPrint));

                            Console.Write("\nPlease Input Index of Course: ");

                            var result = Console.ReadLine() ?? string.Empty;

                            if (int.TryParse(result, out int index))
                            {
                                var specCourse = courseList[index-1];

                                Console.WriteLine();
                                Console.WriteLine("- Course Data -");
                                Console.WriteLine(specCourse.FullPrint);
                                Console.WriteLine();
                                if (output == 1)
                                {
                                    Console.WriteLine("Please input a new Course Code: ");

                                    var newCode = Console.ReadLine() ?? string.Empty;

                                    specCourse.Code = newCode;
                                }
                                else if (output == 2)
                                {
                                    Console.WriteLine("Please input a new Course Name: ");

                                    var newName = Console.ReadLine() ?? string.Empty;

                                    specCourse.Name = newName;
                                }
                                else if (output == 3)
                                {
                                    Console.WriteLine("Please input a new Course Description: ");

                                    var newDesc = Console.ReadLine() ?? string.Empty;

                                    specCourse.Description = newDesc;
                                }
                                else if (output == 4)
                                {

                                    Console.WriteLine();
                                    Console.WriteLine(" - Student List - ");
                                    peopleList.ForEach(t => Console.WriteLine(t.Print));

                                    Console.WriteLine();
                                    Console.WriteLine("Please input the Student's Name you wish to add: ");

                                    var addStudent = Console.ReadLine() ?? string.Empty;

                                    var specificStudent = peopleList.Where(t => (t.Name.Equals(addStudent, StringComparison.CurrentCultureIgnoreCase))).FirstOrDefault();

                                    specCourse.rosterList.Add(specificStudent);

                                }
                                else if (output == 5)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine(" - Student List - ");
                                    specCourse.rosterList.ForEach(t => Console.WriteLine(t.Print));

                                    Console.WriteLine();
                                    Console.WriteLine("Please input the Student's Name you wish to remove: ");

                                    var addStudent = Console.ReadLine() ?? string.Empty;

                                    var specificStudent = peopleList.Where(t => (t.Name.Contains(addStudent, StringComparison.CurrentCultureIgnoreCase))).FirstOrDefault();

                                    specCourse.rosterList.Remove(specificStudent);
                                }
                                else if (output == 6)
                                {
                                    var newAssignment = new Assignment();

                                    Console.WriteLine();
                                    Console.WriteLine("- Assignment Creator -");
                                    Console.WriteLine("Please input an Assignment Name: ");

                                    newAssignment.Name = Console.ReadLine() ?? string.Empty;

                                    Console.WriteLine("Please input an Assignment Description: ");

                                    newAssignment.Description = Console.ReadLine() ?? string.Empty;

                                    Console.WriteLine("Please input an Assignment Point Value: ");

                                    newAssignment.AvailablePoints = Convert.ToInt32(Console.ReadLine());

                                    Console.WriteLine("Due Date in MM-DD-YYYY: ");

                                    var dueDate = Console.ReadLine() ?? string.Empty;

                                    newAssignment.Start = DateTime.Now;

                                    string format = "MM-dd-yyyy";

                                    newAssignment.End = DateTime.ParseExact(dueDate, format, CultureInfo.InvariantCulture);

                                    specCourse.AssignmentList.Add(newAssignment);
                                }
                            }
                        }

                    }
                    else if (userOutput == 4)
                    {

                        Console.WriteLine("\n  - Student Editor - \n=====================");
                        Console.WriteLine("1. Edit Student's Name");
                        Console.WriteLine("2. Edit Student's ID");
                        Console.WriteLine("3. Edit Student's Classification");
                        Console.WriteLine("4. Edit Student's Description: ");
                        Console.WriteLine("Make a Selection: ");

                        var userCommand = Console.ReadLine() ?? string.Empty;

                        if (int.TryParse(userCommand, out int userOutputCommand))
                        {

                            Console.WriteLine();
                            Console.WriteLine(" - Student List - ");
                            peopleList.ForEach(t => Console.WriteLine(t.Print));

                            Console.WriteLine();
                            Console.WriteLine("Please input the Student's Index you wish to edit: ");

                            var studentIndex = Console.ReadLine() ?? string.Empty;

                            if (int.TryParse(studentIndex, out int index))
                            {
                                var studentEdit = peopleList[index - 1];


                                if (userOutputCommand == 1)
                                {
                                    Console.WriteLine("Please input Student's New Name: ");

                                    var newName = Console.ReadLine() ?? string.Empty;
                                    
                                    studentEdit.Name= newName;

                                }
                                else if (userOutputCommand == 2)
                                {
                                    Console.WriteLine("Please input Student's New ID: ");

                                    var newID = Console.ReadLine() ?? string.Empty;

                                    studentEdit.Id = newID;
                                }
                                else if (userOutputCommand == 3)
                                {
                                    Console.WriteLine("Please input Student's New Classification: ");

                                    var newClass = Console.ReadLine() ?? string.Empty;

                                    studentEdit.Classification= newClass;
                                }
                                else if (userOutputCommand == 4)
                                {
                                    Console.WriteLine("Please input Student's New Description: ");

                                    var newDesc = Console.ReadLine() ?? string.Empty;

                                    studentEdit.Description= newDesc;
                                }

                            };
                        }

                    }
                    else if (userOutput == 5)
                    {
                        Console.WriteLine();
                        Console.WriteLine(" - Active Course List - ");
                        courseList.ForEach(t => Console.WriteLine(t.SoftPrint));
                        Console.WriteLine();
                    }
                    else if (userOutput == 6)
                    {
                        Console.WriteLine();
                        Console.WriteLine(" - Active Student List - ");
                        peopleList.ForEach(t => Console.WriteLine(t.Print));
                        Console.WriteLine();
                    }
                    else if (userOutput == 7)
                    {
                        Console.WriteLine("Please input Course Name or Description: ");

                        var query = Console.ReadLine() ?? string.Empty;

                        var filteredList = courseList.Where(t => (t.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase) || t.Description.Contains(query, StringComparison.InvariantCultureIgnoreCase)));

                        Console.WriteLine();
                        Console.WriteLine("Filtered List:");
                        filteredList.ToList().ForEach(t => Console.WriteLine(t.SoftPrint));
                        Console.WriteLine();
                    }
                    else if (userOutput == 8)
                    {
                        Console.WriteLine("Please input Student's Name: ");

                        var query = Console.ReadLine() ?? string.Empty;

                        var filteredList = peopleList.Where(t => (t.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase)));

                        Console.WriteLine();
                        Console.WriteLine("Filtered List:");
                        filteredList.ToList().ForEach(t => Console.WriteLine(t.Print));
                        Console.WriteLine();
                    }
                    else if (userOutput == 9)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter Student's Name: ");

                        var studentName = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("\nStudent's Schedule:");

                        courseList.ForEach(t =>
                        {
                            t.rosterList.ForEach(s =>
                            {
                                if (s.Name.Contains(studentName, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    Console.WriteLine(t.SoftPrint);
                                }
                            });
                        });

                        Console.WriteLine();

                    }
                    else if (userOutput == 10)
                    {
                        status = false;
                    }
                }

            }
        }
    }
}