using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

        public Workshop getWorkshop()
        {
            using var db = new DiplomContext();
            var resource = db.Resources.FirstOrDefault(x => x.Id == ResourceId);
            using var db1 = new DiplomContext();
            return db1.Workshops.FirstOrDefault(x => x.Id == resource.WorkshopId);
        }

        public override string ToString()
        {
            using var db = new DiplomContext();
            using var db1 = new DiplomContext();
            var resource = db.Resources.FirstOrDefault(x => x.Id == ResourceId);
            var measure = db1.Measures.FirstOrDefault(x => x.Id == resource.MeasureId);
            return $"{resource.Title} {Count} {measure.Title}";
        }
    }
}
