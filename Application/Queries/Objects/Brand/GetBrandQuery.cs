using Application.DTOs;
using MediatR;

namespace Application.Queries.Objects
{
    public class GetBrandQuery : IRequest<BrandDto>
    {
        public long Id { get; }

        public GetBrandQuery(long id)
        {
            Id = id;
        }
    }
}
