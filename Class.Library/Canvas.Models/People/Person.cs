using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.People
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual string Print => $"[{Id}] {Name}";
        public string Classification { get; set; }
        public string Description { get; set; }
    }
}
