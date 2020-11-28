using Application.DTOs.Brand;
using Core.Entities;
using Infrastructure.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
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

    public class NewBrandCommand : IRequest<BrandDto>
    {
        public NewBrandCommand(string name, string imageUri)
        {
            Name = name;
            ImageUri = imageUri;
        }

        public string Name { get; }
        public string ImageUri { get; }
    }
}
