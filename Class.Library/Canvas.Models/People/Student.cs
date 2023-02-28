using Class.Library.Canvas.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.People
{
    public class Student : Person
    {

        public Dictionary<int, double> Grades { get; set; }

        public PersonClassification Classification { get; set; }

        public Student()
        {
            Grades = new Dictionary<int, double>();
        }

        public override string ToString()
        {
            return $"[{ID}] {Classification} - {Name}";
        }

    }

    public enum PersonClassification
    {
        Freshman, Sophomore, Junior, Senior
    }
}