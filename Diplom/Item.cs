using System;
using System.Collections.Generic;

#nullable disable

namespace Diplom
{
    public partial class Item
    {
        public Item()
        {
            ItemPartitions = new HashSet<ItemPartition>();
            ItemResources = new HashSet<ItemResource>();
        }

        public long Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<ItemPartition> ItemPartitions { get; set; }
        public virtual ICollection<ItemResource> ItemResources { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
