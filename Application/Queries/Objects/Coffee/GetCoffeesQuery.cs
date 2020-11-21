using Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Objects
{
    public class GetCoffeesQuery : IRequest<List<CoffeeDto>>
    {}
}
