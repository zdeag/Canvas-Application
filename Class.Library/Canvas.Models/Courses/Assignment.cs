using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Courses
{
    public class Assignment
    {
        private static int lastID = 0;
        public int ID = 0;
        public int id
        {
            get
            {
                if (ID == 0)
                {
                    ID = ++lastID;
                }
                return ID;
            }
        }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal AvailablePoints { get; set; }
        public DateTime DueDate { get; set; }

        public override string ToString()
        {
            return $"[{ID}] {DueDate} - {Name} ";
        }
    }
}