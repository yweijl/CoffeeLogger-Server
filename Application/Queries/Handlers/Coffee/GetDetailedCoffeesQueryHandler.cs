using Application.DTOs.Brand;
using Application.DTOs.Coffee;
using Core.Entities;
using Infrastructure.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Handlers
{
    public class GetDetailedCoffeesQueryHandler : IRequestHandler<GetDetailedCoffeesQuery, List<DetailedCoffeeDto>>
    {
        private readonly IRepository _repository;

        public GetDetailedCoffeesQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DetailedCoffeeDto>> Handle(GetDetailedCoffeesQuery request, CancellationToken cancellationToken)
        {
            var coffees = await _repository.ListAsync<Coffee, CoffeeDto>(
                x => new CoffeeDto 
                { 
                    Id = x.Id,
                    Country = x.Country,
                    CoffeeType = x.CoffeeType,
                    BrandId = x.BrandId,
                    LoggedRecords = x.loggedRecords,
                    Rating = x.Rating
                }).ConfigureAwait(false);

            var brands = await _repository.ListAsync<Brand, BrandDto>(
                x => coffees.Select(x => x.BrandId).Contains(x.Id),
                x => new BrandDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUri = x.ImageUri
                }).ConfigureAwait(false);

            var detailedCoffees = coffees.Select(x =>
            {
                var brand = brands.Single(y => y.Id == x.BrandId);

                return new DetailedCoffeeDto
                {
                    Id = x.Id,
                    CoffeeType = x.CoffeeType.ToString(),
                    Country = x.Country,
                    Rating = x.Rating,
                    BrandName = brand.Name,
                    ImageUri = brand.ImageUri,
                    LoggedRecords = x.LoggedRecords
                };
            }).ToList();
            
            return detailedCoffees;
        }
    }

     public class GetDetailedCoffeesQuery : IRequest<List<DetailedCoffeeDto>>
    {}
}
