using Application.DTOs;
using MediatR;

namespace Application.Queries.Objects
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
