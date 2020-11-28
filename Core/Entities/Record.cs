using System.Collections.Generic;

namespace Core.Entities
{
    public class Record : EntityBase
    {
        public decimal DoseIn { get; set; }
        public decimal DoseOut { get; set; }
        public decimal Time { get; set; }
        public decimal Rating { get; set; }
        public virtual ICollection<Flavor> Flavors { get; set; }
        public virtual ICollection<RecordFlavor> RecordFlavors { get; set; }
        public long CoffeeId { get; set; }
    }
}
