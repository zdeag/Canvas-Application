using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Courses
{
    public class Module
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<ContentItem> ContentItems { get; set; }

        public Module() 
        {
            ContentItems = new List<ContentItem>();
        }
    }
}
