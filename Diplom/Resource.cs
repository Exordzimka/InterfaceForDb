using System;
using System.Collections.Generic;

#nullable disable

namespace Diplom
{
    public partial class Resource
    {
        public Resource()
        {
            ItemResources = new HashSet<ItemResource>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public int? CountOnStore { get; set; }
        public long? MeasureId { get; set; }
        public int? WorkshopId { get; set; }

        public virtual Measure Measure { get; set; }
        public virtual Workshop Workshop { get; set; }
        public virtual ICollection<ItemResource> ItemResources { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
