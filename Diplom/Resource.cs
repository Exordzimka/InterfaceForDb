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
        public long PartitionId { get; set; }

        public virtual Partition Partition { get; set; }
        public virtual ICollection<ItemResource> ItemResources { get; set; }
        
        public override string ToString()
        {
            return Title;
        }
    }
}
