using Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Objects
{
    public class GetBrandsQuery : IRequest<List<BrandDto>>
    {}
}
