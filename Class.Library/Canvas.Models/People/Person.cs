using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.People
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Person()
        {
            Name = string.Empty;
        }

        public override string ToString()
        {
            return $"[{ID}] {Name}";
        }
    }
}