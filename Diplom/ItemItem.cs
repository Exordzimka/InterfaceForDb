using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace Diplom
{
    public partial class ItemItem
    {
        public long Id { get; set; }
        public long? ParentItemId { get; set; }
        public long? ChildItemId { get; set; }
        public int? Count { get; set; }

        public virtual Item ChildItem { get; set; }
        public virtual Item ParentItem { get; set; }

        public override string ToString()
        {
            using var db = new DiplomContext();
            var item = db.Items.FirstOrDefault(item1 => item1.Id == ChildItemId);
            return $"{item.Title} {Count} шт.";
        }
    }
}
