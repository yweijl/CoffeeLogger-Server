using Application.DTOs.Coffee;
using Core.Entities;
using Infrastructure.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Handlers
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

    public class GetCoffeeQuery : IRequest<CoffeeDto>
    {
        public long Id { get; }

        public GetCoffeeQuery(long id)
        {
            Id = id;
        }
    }
}
