using Core.Enums;

namespace Application.DTOs.Coffee
{
    public class CoffeeDto
    {
        public long Id { get; set; }
        public long BrandId { get; set; }
        public string Country { get; set; }
        public CoffeeType CoffeeType { get; set; }
        public decimal Rating { get; set; }
        public int LoggedRecords { get; set; }
    }
}
