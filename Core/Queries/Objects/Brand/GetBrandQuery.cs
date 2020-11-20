using Core.DTOs;
using MediatR;

namespace Core.Queries.Objects
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
