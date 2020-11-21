using Application.DTOs.Brand;
using Core.Entities;
using Infrastructure.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Handlers
{
    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, List<BrandDto>>
    {
        private readonly IRepository _repository;

        public GetBrandsQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BrandDto>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _repository.ListAsync<Brand, BrandDto>(
                x => new BrandDto 
                { 
                    Name = x.Name,
                    ImageUri = x.ImageUri
                }).ConfigureAwait(false);
            return brands;
        }
    }

    public class GetBrandsQuery : IRequest<List<BrandDto>>
    { }
}
