using System;
using System.Collections.Generic;

#nullable disable

namespace Diplom
{
    public partial class Measure
    {
        public Measure()
        {
            Resources = new HashSet<Resource>();
        }

        public long Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
