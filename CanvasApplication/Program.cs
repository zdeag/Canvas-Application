using Class.Library.Canvas.Models.Courses;
using Class.Library.Canvas.Models.People;


namespace CanvasApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var peopleList = new List<People>();
            var courseList = new List<Course>();

            bool status = true;

            while(status)
            {
                Console.WriteLine("Canvas Application - Choose an option: ");
                Console.WriteLine("1. Create a Course");
                Console.WriteLine("2. Create a Student");
                Console.WriteLine("3. Edit Course");
                Console.WriteLine("4. Edit Student");
                Console.WriteLine("5. List all Courses");
                Console.WriteLine("6. List all Students");
                Console.WriteLine("7. Search for Course");
                Console.WriteLine("8. Search for Student");
                Console.WriteLine("9. Exit");

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

                        newCourse.Name= Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Please input a Course Description: ");
           
                        newCourse.Description= Console.ReadLine() ?? string.Empty;

                        Console.WriteLine();

                        courseList.Add(newCourse);
                    }
                    else if (userOutput == 2)
                    {
                        var newPerson = new People();

                        Console.WriteLine("Please input Student's Name:");

                        newPerson.Name = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Please input Student's ID: ");

                        newPerson.Id = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Please input Student Description: ");

                        newPerson.Description = Console.ReadLine() ?? string.Empty;

                        peopleList.Add(newPerson);
                    }
                    else if (userOutput == 3)
                    {
                        Console.WriteLine();
                        Console.WriteLine("1. Add Student to Roster");
                        Console.WriteLine("2. Remove Student from Roster");
                        Console.WriteLine("3. Add Assignment");
                        Console.WriteLine("Make a Selection: ");

                        string input = Console.ReadLine() ?? string.Empty;

                        if (int.TryParse(input, out int output))
                        {

                            Console.WriteLine("Please input Course Code or Course Name: ");

                            var query = Console.ReadLine() ?? string.Empty;

                            var filteredList = courseList.Where(t => (t.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase) || t.Code.Contains(query, StringComparison.InvariantCultureIgnoreCase)));

                            Console.WriteLine();
                            Console.WriteLine("Filtered List:");


                            filteredList.ToList().ForEach(t => Console.WriteLine("[" + t.Code + "] " + t.Name + " - " + t.Description));

                            Console.Write("Please input specific Course Code: ");

                            var result = Console.ReadLine() ?? string.Empty;

                            var specCourse = courseList.Where(t => (t.Code.Equals(result, StringComparison.CurrentCultureIgnoreCase))).FirstOrDefault();

                            while (specCourse == null)
                            {
                                Console.WriteLine("Please input the correct Course Code: ");

                                specCourse = courseList.Where(t => (t.Code.Equals(result, StringComparison.CurrentCultureIgnoreCase))).FirstOrDefault();
                            }
                            Console.WriteLine();
                            Console.WriteLine("- Course Data -");
                            Console.WriteLine("[" + specCourse.Code + "] Name: " + specCourse.Name);

                            if (output == 1)
                            {

                                Console.WriteLine();
                                Console.WriteLine(" - Student List - ");
                                peopleList.ForEach(t => Console.WriteLine("[" + t.Id + "] " + t.Name + " - " + t.Description));

                                Console.WriteLine();
                                Console.WriteLine("Please input the Student's ID you wish to add: ");

                                var addStudent = Console.ReadLine() ?? string.Empty;

                                var specificStudent = peopleList.Where(t => (t.Id.Equals(addStudent, StringComparison.CurrentCultureIgnoreCase))).FirstOrDefault();

                                //specCourse.rosterList.Add(specificStudent);

                            }
                            else if (output == 2)
                            {
                                Console.WriteLine();
                                Console.WriteLine(" - Roster List - ");
                                specCourse.rosterList.ForEach(t => Console.WriteLine("[" + t.Id + "] " + t.Name + " - " + t.Description));

                                Console.WriteLine("--------------");
                                Console.WriteLine("Please input the Student's ID you wish to remove: ");

                                var removeStudent = Console.ReadLine() ?? string.Empty;

                                var specificStudent = specCourse.rosterList.Where(t => (t.Id.Equals(removeStudent, StringComparison.CurrentCultureIgnoreCase))).FirstOrDefault();

                                //specCourse.rostersList.Remove(specificStudent);

                                Console.WriteLine("Student was successfuly removed from list.");
                            }
                            else if (output == 3)
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

                                Console.WriteLine("Input the due date using days (integer number)");

                                var time = Convert.ToInt32(Console.ReadLine());

                                newAssignment.Start = DateTime.Now;

                                newAssignment.End = DateTime.Now.AddDays(time);

                                specCourse.AssignmentList.Add(newAssignment);
                            }
                        }

                    }
                    else if (userOutput == 4)
                    {

                    }
                    else if (userOutput == 5)
                    {
                        Console.WriteLine();
                        Console.WriteLine(" - Active Course List - ");
                        courseList.ForEach(t => Console.WriteLine("[" + t.Code+ "] " + t.Name + " - " + t.Description));
                        Console.WriteLine();
                    }
                    else if (userOutput == 6)
                    {
                        Console.WriteLine();
                        Console.WriteLine(" - Active Student List - ");
                        peopleList.ForEach(t => Console.WriteLine("[" + t.Id + "] " + t.Name + " - " + t.Description));
                        Console.WriteLine();
                    }
                    else if (userOutput == 7)
                    {
                        Console.WriteLine("Please input Course Name or Description: ");

                        var query = Console.ReadLine() ?? string.Empty;

                        var filteredList = courseList.Where(t => (t.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase) || t.Description.Contains(query, StringComparison.InvariantCultureIgnoreCase)));

                        Console.WriteLine();
                        Console.WriteLine("Filtered List:");


                        filteredList.ToList().ForEach(t => Console.WriteLine("[" + t.Code + "] " + t.Name + " - " + t.Description));
                        Console.WriteLine();
                    }
                    else if (userOutput == 8)
                    {
                        Console.WriteLine("Please input Student's Name: ");

                        var query = Console.ReadLine() ?? string.Empty;

                        var filteredList = peopleList.Where(t => (t.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase)));

                        Console.WriteLine();
                        Console.WriteLine("Filtered List:");


                        filteredList.ToList().ForEach(t => Console.WriteLine("[" + t.Id + "] " + t.Name + " - " + t.Description));
                        Console.WriteLine();
                    }
                    else if (userOutput == 9)
                    {
                        status = false;
                    }
                }

            }
        }
    }
}