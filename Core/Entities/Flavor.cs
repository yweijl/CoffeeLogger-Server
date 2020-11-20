using System.Collections.Generic;

namespace Core.Entities
{
    public class Flavor : EntityBase
    {
        public string Name { get; set; }
        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<RecordFlavors> RecordFlavors { get; set; }
    }
}