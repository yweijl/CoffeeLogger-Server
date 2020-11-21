namespace Application.DTOs.Brand
{
    public class BrandDto
    {
        public long Id { get; set; }
        public string Name { get; internal set; }
        public string ImageUri { get; internal set; }
    }
}
