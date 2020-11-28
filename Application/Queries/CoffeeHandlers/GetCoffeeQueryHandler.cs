using Application.DTOs.Coffee;
using Core.Entities;
using Infrastructure.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.CoffeeHandlers
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
            var coffee = await _repository.SingleAsync<Coffee, CoffeeDto>(x => x.Id == request.Id,
                x => new CoffeeDto 
                { 
                    Id = x.Id,
                    CoffeeType = x.CoffeeType,
                    Country = x.Country,
                    BrandId = x.BrandId,
                    LoggedRecords = x.loggedRecords,
                    Rating = x.Rating
                }).ConfigureAwait(false);
            
            return coffee;
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
