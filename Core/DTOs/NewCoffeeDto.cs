using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class NewCoffeeDto : IValidatableObject
    {
        [Required]
        public long BrandId { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string CoffeeType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enum.TryParse<Enums.CoffeeType>(CoffeeType, true, out _))
            {
                yield return new ValidationResult(
                    $"The CoffeeType {CoffeeType} is not valid.",
                    new[] { nameof(CoffeeType) });
            }
        }
    }
}
