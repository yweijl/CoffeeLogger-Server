using Core.DTOs;
using MediatR;

namespace Core.Commands.Objects
{
    public class NewCoffeeCommand : IRequest<CoffeeDto>
    {
        public NewCoffeeCommand(long brandId, string coffeeType, string country)
        {
            BrandId = brandId;
            CoffeeType = coffeeType;
            Country = country;
        }

        public long BrandId { get; }
        public string CoffeeType { get; }
        public string Country { get; }
    }
}
