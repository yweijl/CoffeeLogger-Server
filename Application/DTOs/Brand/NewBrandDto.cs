using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Brand
{
    public class NewBrandDto
    {
        [Required]
        public string Name { get; set; }
        public string imageUri { get; set; }
    }
}
