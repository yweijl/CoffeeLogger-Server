using Application.DTOs;
using MediatR;

namespace Application.Commands.Objects
{
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
