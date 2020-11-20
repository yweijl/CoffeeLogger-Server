using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Queries.Objects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Queries.Handlers
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
                    Name = x.Name,
                    ImageUri = x.ImageUri
                }).ConfigureAwait(false);
            return brands;
        }
    }
}
