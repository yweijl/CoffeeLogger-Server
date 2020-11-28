using Application.Commands;
using Application.DTOs.Coffee;
using Core.Entities;
using FluentAssertions;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;
using Shared.UnitTests;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.CommandHandlers.UnitTests
{
    [TestFixture]
    public class NewCoffeeCommandHandlerUnitTests
    {
        [Test]
        public async Task NewCoffeeCommandHandler_Works()
        {
            var repository = new Mock<IRepository>();
           
            var coffee = new Coffee 
            { 
                Id = 1, 
                BrandId = 1, 
                CoffeeType = Core.Enums.CoffeeType.Robusto, 
                Country = "a", 
            };

            var captor = new ArgumentCaptor<Coffee>();
            repository.Setup(x => x.InsertAsync(captor.Capture()))
                .ReturnsAsync(coffee);
            
            var handler = new NewCoffeeCommandHandler(repository.Object);
            
            var result = await handler
                .Handle(new NewCoffeeCommand(coffee.BrandId, coffee.CoffeeType.ToString(), coffee.Country), 
                        It.IsAny<CancellationToken>())
                .ConfigureAwait(false);

            result.Should().BeOfType<CoffeeDto>();
            result.Id.Should().Equals(coffee.Id);
            result.CoffeeType.Should().Equals(coffee.CoffeeType);
            result.Country.Should().Equals(coffee.Country);

            captor.Value.BrandId.Should().Be(coffee.BrandId);
            captor.Value.CoffeeType.Should().Be(coffee.CoffeeType);
            captor.Value.Country.Should().Be(coffee.Country);

            repository.Verify(x => x.InsertAsync(captor.Value), Times.Once);
            repository.VerifyNoOtherCalls();
        }
    }
}
