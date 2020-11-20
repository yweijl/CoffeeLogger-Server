using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class NewRecordDto
    {
        [Required]
        public long CoffeeId { get; set; }
        [Required]
        public decimal DoseIn{ get; set; }
        [Required]
        public decimal DoseOut { get; set; }
        [Required]
        public decimal Time { get; set; }
        [Required]
        public decimal Rating { get; set; }
        public string[] Flavors { get; set; }
    }
}
