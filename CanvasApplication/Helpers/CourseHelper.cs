using Class.Library.Canvas.Models.Courses;
using Class.Library.Canvas.Models.People;
using Class.Library.Canvas.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CanvasApplication.Helpers
{
    public class CourseHelper
    {
        private CourseService courseService;
        private PersonService personService;

        public CourseHelper()
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
        }

        public void CreateCourse(Course? selectedCourse = null)
        {
            bool isNewCourse = false;

            // Creates a new course if parameter is null
            if (selectedCourse == null)
            {
                isNewCourse = true;
                selectedCourse = new Course();
            }

            var choice = "Y";
            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to edit the Course Code?");
                choice = Console.ReadLine() ?? "N";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Write("Input Course Code:");
                selectedCourse.Code = Console.ReadLine() ?? string.Empty;
            }

            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to edit the Course Name?");
                choice = Console.ReadLine() ?? "N";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Write("Input Course Name:");
                selectedCourse.Name = Console.ReadLine() ?? string.Empty;
            }

            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to edit the Course Description?");
                choice = Console.ReadLine() ?? "N";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Input Course Description:");
                selectedCourse.Description = Console.ReadLine() ?? string.Empty;
            }

            if (isNewCourse)
            {

                SetupRoster(selectedCourse);
                SetupAssignments(selectedCourse);
                SetupModules(selectedCourse);
            }

            if (isNewCourse)
            {
                courseService.AddCourse(selectedCourse);
            }
        }

        public void UpdateCourse()
        {
            Console.WriteLine("Enter the Code for the Course to update:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                CreateCourse(selectedCourse);
            }
        }

        public void SearchCourses(string? query = null)
        {
            if (string.IsNullOrEmpty(query))
            {
                courseService.Courses.ForEach(Console.WriteLine);
            }
            else
            {
                courseService.Search(query).ToList().ForEach(Console.WriteLine);
            }

            Console.WriteLine("Select a course:");
            var code = Console.ReadLine() ?? string.Empty;

            var selectedCourse = courseService
                .Courses
                .FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine(selectedCourse.DetailDisplay);
            }
        }

        public void UpdateStudentMenu()
        {
            Console.WriteLine("\n1. Add a Student");
            Console.WriteLine("2. Remove a Student");
            Console.WriteLine("Please Select an Option:");

            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    AddStudent();
                }
                else if (result == 2)
                {
                    RemoveStudent();
                }
            }
        }

        public void AddStudent()
        {
            Console.WriteLine("Enter the code for the course to add the student to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                personService.Students.Where(s => !selectedCourse.Roster.Any(s2 => s2.ID == s.ID)).ToList().ForEach(Console.WriteLine);
                if (personService.Students.Any(s => !selectedCourse.Roster.Any(s2 => s2.ID == s.ID)))
                {
                    selection = Console.ReadLine() ?? string.Empty;
                }

                if (selection != null)
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = personService.Students.FirstOrDefault(s => s.ID == selectedId);
                    if (selectedStudent != null)
                    {
                        selectedCourse.Roster.Add(selectedStudent);
                    }
                }

            }
        }
        public void RemoveStudent()
        {
            Console.WriteLine("Enter the code for the course to remove the student from:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Roster.ForEach(Console.WriteLine);
                if (selectedCourse.Roster.Any())
                {
                    selection = Console.ReadLine() ?? string.Empty;
                }
                else
                {
                    selection = null;
                }

                if (selection != null)
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = personService.Students.FirstOrDefault(s => s.ID == selectedId);
                    if (selectedStudent != null)
                    {
                        selectedCourse.Roster.Remove(selectedStudent);
                    }
                }

            }
        }

        public void UpdateAssignmentGroupMenu()
        {
            Console.WriteLine("Enter the Course Code you want to add Assigment Group to: ");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));

            if (selection != null)
            {
                Console.WriteLine("1. Create an Assignment Group");
                Console.WriteLine("2. Edit an Assignment Group");
                Console.WriteLine("3. Remove an Assignment Group");
                Console.WriteLine("4. Add an Assignment to Group");
                Console.WriteLine("5. Remove an Assigment from Group");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    if (result == 1)
                    {
                        AddAssigmentGroup(selectedCourse);
                    }
                    else if (result > 1 && result < 6)
                    {
                        Console.WriteLine($"Please Enter the name of the desired Assignment Group found in {selectedCourse.Name}");
                        selectedCourse.AssignmentGroups.ForEach(Console.WriteLine);
                        var userInput = Console.ReadLine();

                        var selectedAssigmnetGroup = selectedCourse.AssignmentGroups.FirstOrDefault(s => s.Name.Equals(userInput, StringComparison.InvariantCultureIgnoreCase));

                        if (selectedAssigmnetGroup!= null)
                        {
                            if (result == 2)
                            {
                                /// Edit Assigment
                            } else if (result == 3) 
                            {
                                // Remove an Assigment Group
                            } else if (result == 4)
                            {
                                // Add an Assigment to group
                            } else if (result == 5)
                            {
                                // Remove an Assigment from group
                            }
                        }
                    }
                }
            }
        }

        public void AddAssigmentGroup(Course c)
        {
            if (c != null)
            {
                c.AssignmentGroups.Add(CreateAssignmentGroup(c));
            }
        }

        private AssignmentGroup CreateAssignmentGroup(Course c)
        {
            Console.WriteLine("Please input Assigment Group Name:");
            var assignmentGroupName = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Please input Assigment Group Description:");
            var assignmentGroupDesc = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Please input Assignment Group Weightage:");
            var assignmentGroupWeight = Console.ReadLine() ?? string.Empty;
            int weightage = int.Parse(assignmentGroupWeight);

            return new AssignmentGroup
            {
                Name = assignmentGroupName,
                Description = assignmentGroupDesc,
                AssignmentWeight = weightage
            };
        }

        private void EditAssignmentGroup()
        {
            
        }

        public void UpdateAssignmentMenu()
        {
            Console.WriteLine("\n1. Add an Assignment");
            Console.WriteLine("2. Remove an Assignment");
            Console.WriteLine("3. Update an Assignment");
            Console.WriteLine("Please Select an Option:");

            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    AddAssignment();
                }
                else if (result == 2)
                {
                    RemoveAssignment();
                }
                else if (result == 3)
                {
                    UpdateAssignment();
                }
            }
        }

        public void AddAssignment()
        {
            Console.WriteLine("Enter the code for the course to add the assignment to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Assignments.Add(CreateAssignment());
            }
        }

        public void UpdateAssignment()
        {
            Console.WriteLine("Enter the code for the course:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an assignment to update:");
                selectedCourse.Assignments.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAssignment = selectedCourse.Assignments.FirstOrDefault(a => a.ID == selectionInt);
                if (selectedAssignment != null)
                {
                    var index = selectedCourse.Assignments.IndexOf(selectedAssignment);
                    selectedCourse.Assignments.RemoveAt(index);
                    selectedCourse.Assignments.Insert(index, CreateAssignment());
                }
            }
        }

        public void RemoveAssignment()
        {
            Console.WriteLine("Enter the code for the course:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an assignment to delete:");
                selectedCourse.Assignments.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAssignment = selectedCourse.Assignments.FirstOrDefault(a => a.ID == selectionInt);
                if (selectedAssignment != null)
                {
                    selectedCourse.Assignments.Remove(selectedAssignment);
                }
            }
        }

        private void SetupRoster(Course c)
        {
            Console.WriteLine("Which students should be enrolled in this course? ('Q' to quit)");
            bool continueAdding = true;
            while (continueAdding)
            {
                personService.Students.Where(s => !c.Roster.Any(s2 => s2.ID == s.ID)).ToList().ForEach(Console.WriteLine);
                var selection = "Q";
                if (personService.Students.Any(s => !c.Roster.Any(s2 => s2.ID == s.ID)))
                {
                    selection = Console.ReadLine() ?? string.Empty;
                }

                if (selection.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    continueAdding = false;
                }
                else
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = personService.Students.FirstOrDefault(s => s.ID == selectedId);

                    if (selectedStudent != null)
                    {
                        c.Roster.Add(selectedStudent);
                    }
                }


            }
        }

        private void SetupAssignments(Course c)
        {
            Console.WriteLine("Would you like to add assignments? (Y/N)");
            var assignResponse = Console.ReadLine() ?? "N";
            bool continueAdding;
            if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                continueAdding = true;
                while (continueAdding)
                {
                    c.Assignments.Add(CreateAssignment());
                    Console.WriteLine("Add more assignments? (Y/N)");
                    assignResponse = Console.ReadLine() ?? "N";
                    if (assignResponse.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continueAdding = false;
                    }
                }
            }

        }

        private Assignment CreateAssignment()
        {
            //Name
            Console.WriteLine("Name:");
            var assignmentName = Console.ReadLine() ?? string.Empty;
            //Description
            Console.WriteLine("Description:");
            var assignmentDescription = Console.ReadLine() ?? string.Empty;
            //TotalPoints
            Console.WriteLine("TotalPoints:");
            var totalPoints = decimal.Parse(Console.ReadLine() ?? "100");
            //DueDate
            Console.WriteLine("DueDate:");
            var dueDate = DateTime.Parse(Console.ReadLine() ?? "01/01/1900");

            return new Assignment
            {
                Name = assignmentName,
                Description = assignmentDescription,
                AvailablePoints = totalPoints,
                DueDate = dueDate
            };
        }

        private void UpdateModuleMenu()
        {
            Console.WriteLine("\n1. Add a Module");
            Console.WriteLine("2. Remove a Module");
            Console.WriteLine("3. Update Content");
            Console.WriteLine("Please Select an Option:");

            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    AddAssignment();
                }
                else if (result == 2)
                {
                    RemoveAssignment();
                }
                else if (result == 3)
                {
                    UpdateAssignment();
                }
            }
        }

        private void SetupModules(Course c)
        {
            Console.WriteLine("Would you like to add Modules? (Y/N)");
            var input = Console.ReadLine() ?? "N";
            bool continueAdding;
            if (input.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                continueAdding = true;
                while (continueAdding)
                {
                    CreateModule(c);
                    Console.WriteLine("Do you want to add more Modules? (Y/N)");
                    input = Console.ReadLine() ?? "N";
                    if (input.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continueAdding = false;
                    }
                }
            }

        }

        public void CreateModule(Course c, Module? selectedModule = null)
        {
            bool isNewModule = false;

            // Creates a new course if parameter is null
            if (selectedModule == null)
            {
                isNewModule = true;
                selectedModule = new Module();
            }

            var choice = "Y";
            if (!isNewModule)
            {
                Console.WriteLine("Do you want to add/change the Module Name?");
                choice = Console.ReadLine() ?? "N";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Write("Input Module Name:");
                selectedModule.Name = Console.ReadLine() ?? string.Empty;
            }

            if (!isNewModule)
            {
                Console.WriteLine("Do you want to add/change the Module Description?");
                choice = Console.ReadLine() ?? "N";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Input Module Description:");
                selectedModule.Description = Console.ReadLine() ?? string.Empty;
            }

            if (!isNewModule)
            {
                Console.WriteLine("Do you want to add/change any Module Content?");
                choice = Console.ReadLine() ?? string.Empty;
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                CreateContentItemMenu(c, selectedModule);
            }

            if (isNewModule)
            {
                c.Modules.Add(selectedModule);
            }

        }


        private void CreateContentItemMenu(Course c, Module? selectedModule = null)
        {
            if (selectedModule == null)
            {
                Console.WriteLine("Enter the Code for the Course to update:");
                c.Modules.ForEach(Console.WriteLine);
                var selection = Console.ReadLine();

                var module = c.Modules.FirstOrDefault(s => s.Name.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            }

            var choice = "Y";

            Console.WriteLine("Do you want to add/edit Page Item?");
            choice = Console.ReadLine() ?? string.Empty;

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                CreatePageItem(selectedModule);
            }

            Console.WriteLine("Do you want to add/edit Assignment Item?");
            choice = Console.ReadLine() ?? string.Empty;

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                // deal with adding // editting assignment item
            }


            Console.WriteLine("Do you want to add/edit File Item?");
            choice = Console.ReadLine() ?? string.Empty;

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                CreateFileItem(selectedModule);
            }
        }

        private void CreatePageItem(Module s)
        {
            Console.WriteLine("1. Add Page Item");
            Console.WriteLine("2. Edit Page Item");
            Console.WriteLine("3. Remove Page Item");
            Console.WriteLine("4. Exit");
            Console.WriteLine("Input Selection:");

            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    Console.WriteLine("Input Page HTML:");
                    var htmlCode = Console.ReadLine() ?? string.Empty;


                    s.ContentItems.Add(new PageItem { HtmlBody = htmlCode });
                }
                else if (result == 2 || result == 3)
                {

                    Console.WriteLine("Input Content ID:");
                    if (int.TryParse(Console.ReadLine(), out int output))
                    {
                        var selectedContent = s.ContentItems.OfType<PageItem>().FirstOrDefault(s => s.ID.Equals(output));

                        if (selectedContent != null)
                        {
                            if (result == 1)
                            {

                                Console.WriteLine("Do you want to edit the HTML Code:");

                                var choice = Console.ReadLine() ?? string.Empty;

                                if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Console.WriteLine("Input HTML Code:");
                                    selectedContent.HtmlBody = Console.ReadLine();
                                }
                            }
                            else if (result == 2)
                            {
                                s.ContentItems.Remove(selectedContent);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }
            }
        }

        private void CreateFileItem(Module s)
        {
            Console.WriteLine("1. Add File Item");
            Console.WriteLine("2. Edit File Item");
            Console.WriteLine("3. Remove File Item");
            Console.WriteLine("4. Exit");
            Console.WriteLine("Input Selection:");

            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    Console.WriteLine("Input File Path:");
                    var filePath = Console.ReadLine() ?? string.Empty;


                    s.ContentItems.Add(new FileItem { Path = filePath });
                }
                else if (result == 2 || result == 3)
                {

                    Console.WriteLine("Input Content ID:");
                    if (int.TryParse(Console.ReadLine(), out int output))
                    {
                        var selectedContent = s.ContentItems.OfType<FileItem>().FirstOrDefault(s => s.ID.Equals(output));

                        if (selectedContent != null)
                        {
                            if (result == 1)
                            {

                                Console.WriteLine("Do you want to edit the File Path:");

                                var choice = Console.ReadLine() ?? string.Empty;

                                if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Console.WriteLine("Input HTML Code:");
                                    selectedContent.Path = Console.ReadLine();
                                }
                            }
                            else if (result == 2)
                            {
                                s.ContentItems.Remove(selectedContent);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }
            }
        }


    }
}


