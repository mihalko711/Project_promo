using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule_
{
    public class Teacher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Rating { get; set; }

        public virtual ICollection<Description> Descriptions { get; set; }
    }

    
    
}
