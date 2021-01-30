using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule_
{
    public class Description
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }

        public string? Portrait { get; set; }

        public string? Citate { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
