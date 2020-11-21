using Core.Enums;

namespace Application.DTOs
{
    public class CoffeeDto
    {
        public long Id { get; set; }
        public string Country { get; set; }
        public CoffeeType CoffeeType { get; set; }
    }
}
