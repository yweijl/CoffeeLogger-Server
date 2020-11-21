namespace Application.DTOs.Coffee
{
    public class DetailedCoffeeDto
    {
        public long Id { get; set; }
        public string BrandName { get; set; }
        public string ImageUri { get; set; }
        public string CoffeeType { get; set; }
        public string Country { get; set; }
        public decimal Rating { get; set; }
        public int Number { get; set; }
    }
}
