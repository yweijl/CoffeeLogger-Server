using Core.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Core.Queries.Objects
{
    public class GetCoffeesQuery : IRequest<List<CoffeeDto>>
    {}
}
