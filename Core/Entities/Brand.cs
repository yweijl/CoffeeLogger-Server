using System.Collections.Generic;

namespace Core.Entities
{
    public class Brand : EntityBase
    {
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public virtual ICollection<Coffee> Coffees { get; set; }
    }
}
