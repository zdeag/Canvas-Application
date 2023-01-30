using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Courses
{
    public class Assignment
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int AvailablePoints { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
