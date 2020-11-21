using Application.Commands.Objects;
using Application.DTOs;
using Core.Entities;
using Infrastructure.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Handlers
{
    public class NewBrandCommandHandler : IRequestHandler<NewBrandCommand, BrandDto>
    {
        private readonly IRepository _repository;

        public NewBrandCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<BrandDto> Handle(NewBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _repository.InsertAsync(new Brand
            {
                Name = request.Name,
                ImageUri = request.ImageUri
            }).ConfigureAwait(false);

            return new BrandDto {
                Id = brand.Id,
                Name = brand.Name, 
                ImageUri = brand.ImageUri 
            };
        }
    }
}
