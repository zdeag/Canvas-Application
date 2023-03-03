using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Courses
{
    public class CompletedAssignment
    {
        public int Id { get; set; }
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal AvailablePoints { get; set; }
        public decimal totalPointsGiven { get; set; }
        public int Weight { get; set; }

        public bool IsGraded { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} - Grade {totalPointsGiven / AvailablePoints}";
        }
    }
}
