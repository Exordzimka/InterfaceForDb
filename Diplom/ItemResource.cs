using System;
using System.Collections.Generic;

#nullable disable

namespace Diplom
{
    public partial class ItemResource
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long ResourceId { get; set; }
        public int Count { get; set; }

        public virtual Item Item { get; set; }
        public virtual Resource Resource { get; set; }

        public override string ToString()
        {
            return $"{Resource.Title} {Count} шт.";
        }
    }
}
