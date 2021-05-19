using System;
using System.Collections.Generic;

#nullable disable

namespace Diplom
{
    public partial class Item
    {
        public Item()
        {
            ItemItemChildItems = new HashSet<ItemItem>();
            ItemItemParentItems = new HashSet<ItemItem>();
            ItemResources = new HashSet<ItemResource>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public int? CountOnStore { get; set; }

        public virtual ICollection<ItemItem> ItemItemChildItems { get; set; }
        public virtual ICollection<ItemItem> ItemItemParentItems { get; set; }
        public virtual ICollection<ItemResource> ItemResources { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
