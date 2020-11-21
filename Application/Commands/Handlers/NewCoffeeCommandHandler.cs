using Application.DTOs.Coffee;
using Core.Entities;
using Core.Enums;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Handlers
{
    public class NewCoffeeCommandHandler : IRequestHandler<NewCoffeeCommand, CoffeeDto>
    {
        private readonly IRepository _repository;

        public NewCoffeeCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<CoffeeDto> Handle(NewCoffeeCommand request, CancellationToken cancellationToken)
        {
            var coffee = await _repository.InsertAsync(new Coffee
            {
                BrandId = request.BrandId,
                Country = request.Country,
                CoffeeType = Enum.Parse<CoffeeType>(request.CoffeeType, true)
            }).ConfigureAwait(false);

            return new CoffeeDto
            {
                Id = coffee.Id,
                CoffeeType = coffee.CoffeeType,
                Country = coffee.Country
            };
        }
    }

    public class NewCoffeeCommand : IRequest<CoffeeDto>
    {
        public NewCoffeeCommand(long brandId, string coffeeType, string country)
        {
            BrandId = brandId;
            CoffeeType = coffeeType;
            Country = country;
        }

        public long BrandId { get; }
        public string CoffeeType { get; }
        public string Country { get; }
    }
}