using Application.DTOs.Coffee;
using Application.Queries.CoffeeHandlers;
using Core.Entities;
using FluentAssertions;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;
using Shared.UnitTests;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.QueryHandlers.UnitTests
{
    public class GetCoffeeQueryHandlerUnitTests
    {
        [Test]
        public async Task GetCoffeeQueryHandler_Works()
        {
            var Coffees = new List<Coffee> {
                new Coffee { Id = 1, Country = "a", CoffeeType = Core.Enums.CoffeeType.Arabica, BrandId = 1, loggedRecords = 1, Rating = 1 }, 
                new Coffee { Id = 2, Country = "b", CoffeeType = Core.Enums.CoffeeType.Robusto, BrandId = 2, loggedRecords = 2, Rating = 2 }, 
            };

            var repository = new Mock<IRepository>();

            var predicateCaptor = new ExpressionCaptor<Coffee, bool>();
            var selectorCaptor = new ExpressionCaptor<Coffee, CoffeeDto>();

            repository.Setup(x => x.SingleAsync(
                predicateCaptor.Capture(),
                selectorCaptor.Capture())
            ).ReturnsAsync(new CoffeeDto());

            var queryHandler = new GetCoffeeQueryHandler(repository.Object);

            var result = await queryHandler
                .Handle(new GetCoffeeQuery(1), It.IsAny<CancellationToken>())
                .ConfigureAwait(false);

            result.Should().BeOfType<CoffeeDto>();
       
            var actualResult = Coffees.Single(predicateCaptor.Compile());
            var transformedResult = selectorCaptor.Invoke(actualResult);

            transformedResult.Id.Should().Be(1);
            transformedResult.BrandId.Should().Be(1);
            transformedResult.Country.Should().Be("a");
            transformedResult.LoggedRecords.Should().Be(1);
            transformedResult.Rating.Should().Be(1);
            transformedResult.CoffeeType.Should().Be(Core.Enums.CoffeeType.Arabica);
        }
    }
}