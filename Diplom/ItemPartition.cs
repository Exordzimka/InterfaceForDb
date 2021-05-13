using System;
using System.Collections.Generic;

#nullable disable

namespace Diplom
{
    public partial class ItemPartition
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long PartitionId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Partition Partition { get; set; }
    }
}
