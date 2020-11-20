using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Queries.Objects;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Queries.Handlers
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
                    CoffeeType = x.CoffeeType
                }).ConfigureAwait(false);
            
            return Coffees;
        }
    }
}
