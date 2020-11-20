using Core.Commands.Objects;
using Core.DTOs;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Commands.Handlers
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
}