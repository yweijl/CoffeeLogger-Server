using Application.DTOs.Coffee;
using Application.Queries.CoffeeHandlers;
using Core.Entities;
using FluentAssertions;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;
using Shared.UnitTests;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.QueryHandlers.UnitTests
{
    public class GetCoffeesQueryHandlerUnitTests
    {

        [Test]
        public async Task GetCoffeesQueryHandler_Works()
        {
            var repository = new Mock<IRepository>();

            var selectorCaptor = new ExpressionCaptor<Coffee, CoffeeDto>();

            repository.Setup(x => x.ListAsync(selectorCaptor.Capture())).ReturnsAsync(new List<CoffeeDto>());

            var queryHandler = new GetCoffeesQueryHandler(repository.Object);

            var result = await queryHandler
                .Handle(new GetCoffeesQuery(), It.IsAny<CancellationToken>())
                .ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<List<CoffeeDto>>());
            var coffee = new Coffee { Id = 1, Country = "a", CoffeeType = Core.Enums.CoffeeType.Arabica, BrandId = 1, loggedRecords = 1, Rating = 1 };
            var transformedResult = selectorCaptor.Invoke(coffee);

            transformedResult.Id.Should().Be(1);
            transformedResult.BrandId.Should().Be(1);
            transformedResult.Country.Should().Be("a");
            transformedResult.LoggedRecords.Should().Be(1);
            transformedResult.Rating.Should().Be(1);
            transformedResult.CoffeeType.Should().Be(Core.Enums.CoffeeType.Arabica);
        }
    }
}