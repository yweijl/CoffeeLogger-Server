using Application.DTOs.Coffee;
using Core.Entities;
using Infrastructure.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.CoffeeHandlers
{
    public class GetCoffeesQueryHandler : IRequestHandler<GetCoffeesQuery, List<CoffeeDto>>
    {
        private readonly IRepository _repository;

        public GetCoffeesQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CoffeeDto>> Handle(GetCoffeesQuery request, CancellationToken cancellationToken)
        {
            var Coffees = await _repository.ListAsync<Coffee, CoffeeDto>(
                x => new CoffeeDto 
                { 
                    Id = x.Id,
                    Country = x.Country,
                    CoffeeType = x.CoffeeType,
                    BrandId = x.BrandId,
                    LoggedRecords = x.loggedRecords,
                    Rating = x.Rating
                }).ConfigureAwait(false);
            
            return Coffees;
        }
    }

    public class GetCoffeesQuery : IRequest<List<CoffeeDto>>
    { }
}
