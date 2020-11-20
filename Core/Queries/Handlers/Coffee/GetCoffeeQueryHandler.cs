using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Queries.Objects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Queries.Handlers
{
    public class GetCoffeeQueryHandler : IRequestHandler<GetCoffeeQuery, CoffeeDto>
    {
        private readonly IRepository _repository;

        public GetCoffeeQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<CoffeeDto> Handle(GetCoffeeQuery request, CancellationToken cancellationToken)
        {
            var brands = await _repository.SingleAsync<Coffee, CoffeeDto>(x => x.Id == request.Id,
                x => new CoffeeDto 
                { 
                    Id = x.Id,
                    CoffeeType = x.CoffeeType,
                    Country = x.Country
                }).ConfigureAwait(false);
            
            return brands;
        }
    }
}
