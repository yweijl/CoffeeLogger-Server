using Core.Enums;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Coffee : EntityBase
    {
        public string Country { get; set; }
        public CoffeeType CoffeeType { get; set; }
        public long BrandId { get; set; }
        public virtual ICollection<Record> Records { get; set; }
        public decimal Rating { get; set; }
        public int loggedRecords { get; set; }
    }
}
