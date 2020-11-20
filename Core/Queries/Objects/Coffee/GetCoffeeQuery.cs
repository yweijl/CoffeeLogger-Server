using Core.DTOs;
using MediatR;

namespace Core.Queries.Objects
{
    public class GetCoffeeQuery : IRequest<CoffeeDto>
    {
        public long Id { get; }

        public GetCoffeeQuery(long id)
        {
            Id = id;
        }
    }
}
