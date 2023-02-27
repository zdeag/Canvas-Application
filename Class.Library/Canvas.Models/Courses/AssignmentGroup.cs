using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Courses
{
    public class AssignmentGroup
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int AssignmentWeight { get; set; }

        public List<Assignment> Assignments { get; set; }

        public AssignmentGroup() 
        {
            Name = string.Empty;
            Description = string.Empty;
            AssignmentWeight = 0;
            Assignments = new List<Assignment>();
        }

        public override string ToString()
        {
            return $"[{Name}] {Description} ({AssignmentWeight}%)";
        }


    }
}
