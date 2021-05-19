using System;
using System.Collections.Generic;

#nullable disable

namespace Diplom
{
    public partial class Workshop
    {
        public Workshop()
        {
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
