using Application.DTOs.Brand;
using Core.Entities;
using Infrastructure.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.BrandHandlers
{
    public class GetBrandQueryHandler : IRequestHandler<GetBrandQuery, BrandDto>
    {
        private readonly IRepository _repository;

        public GetBrandQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<BrandDto> Handle(GetBrandQuery request, CancellationToken cancellationToken)
        {
            var brands = await _repository.SingleAsync<Brand, BrandDto>(x => x.Id == request.Id,
                x => new BrandDto
                { 
                    Id = x.Id,
                    Name = x.Name,
                    ImageUri = x.ImageUri
                }).ConfigureAwait(false);
            return brands;
        }
    }

    public class GetBrandQuery : IRequest<BrandDto>
    {
        public long Id { get; }

        public GetBrandQuery(long id)
        {
            Id = id;
        }
    }
}
