using Class.Library.Canvas.Models.People;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Courses
{
    public class Course
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Student> rosterList = new List<Student>();
        public List<Assignment> AssignmentList= new List<Assignment>();
        //public List<Modules>()

    }
}
