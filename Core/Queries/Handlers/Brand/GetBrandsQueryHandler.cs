using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Queries.Objects;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Queries.Handlers
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
}
