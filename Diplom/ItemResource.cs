using System;
using System.Collections.Generic;
using System.Linq;

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
            using var db = new DiplomContext();
            using var db1 = new DiplomContext();
            var resource = db.Resources.FirstOrDefault(resource1 => resource1.Id == ResourceId);
            var measure = db.Measures.FirstOrDefault(measure1 => measure1.Id == resource.MeasureId);
            return $"{resource.Title} {Count} {measure.Title}";
        }
    }
}
