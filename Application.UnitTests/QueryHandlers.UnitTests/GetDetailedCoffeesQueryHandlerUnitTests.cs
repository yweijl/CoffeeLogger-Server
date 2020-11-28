using Application.DTOs.Brand;
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
    public class GetDetailedCoffeesQueryHandlerUnitTests
    {

        [Test]
        public async Task GetCoffeesQueryHandler_Works()
        {
            var repository = new Mock<IRepository>();

            var coffeeSelectorCaptor = new ExpressionCaptor<Coffee, CoffeeDto>();
            var brandSelectorCaptor = new ExpressionCaptor<Brand, BrandDto>();
            var brandPredicateCaptor = new ExpressionCaptor<Brand, bool>();

            repository.Setup(x => x.ListAsync(coffeeSelectorCaptor.Capture()))
                .ReturnsAsync(new List<CoffeeDto>
                {
                    new CoffeeDto {Id = 1, BrandId = 1, CoffeeType = Core.Enums.CoffeeType.Arabica, Country = "a", LoggedRecords = 1, Rating = 1},
                    new CoffeeDto {Id = 2, BrandId = 1, CoffeeType = Core.Enums.CoffeeType.Robusto, Country = "b", LoggedRecords = 2, Rating = 2},
                    new CoffeeDto {Id = 3, BrandId = 2, CoffeeType = Core.Enums.CoffeeType.Robusto, Country = "c", LoggedRecords = 3, Rating = 3}
                });

            repository.Setup(x => x.ListAsync(brandPredicateCaptor.Capture(), brandSelectorCaptor.Capture()))
                .ReturnsAsync(new List<BrandDto>
                {
                    new BrandDto { Id = 1 , Name = "a", ImageUri = "uri-1" },
                    new BrandDto { Id = 2 , Name = "b", ImageUri = "uri-2" }
                });

            var queryHandler = new GetDetailedCoffeesQueryHandler(repository.Object);

            var result = await queryHandler
                .Handle(new GetDetailedCoffeesQuery(), It.IsAny<CancellationToken>())
                .ConfigureAwait(false);

            result.Should().BeOfType<List<DetailedCoffeeDto>>();
            result.Count.Should().Be(3);
            result.Should().BeEquivalentTo(new List<DetailedCoffeeDto>
            {
                new DetailedCoffeeDto { Id = 1, BrandName = "a", CoffeeType = Core.Enums.CoffeeType.Arabica.ToString(), Country = "a", ImageUri = "uri-1", LoggedRecords = 1, Rating = 1},
                new DetailedCoffeeDto { Id = 2, BrandName = "a", CoffeeType = Core.Enums.CoffeeType.Robusto.ToString(), Country = "b", ImageUri = "uri-1", LoggedRecords = 2, Rating = 2},
                new DetailedCoffeeDto { Id = 3, BrandName = "b", CoffeeType = Core.Enums.CoffeeType.Robusto.ToString(), Country = "c", ImageUri = "uri-2", LoggedRecords = 3, Rating = 3}
            });

            var coffee = new Coffee { Id = 1, Country = "a", CoffeeType = Core.Enums.CoffeeType.Arabica, BrandId = 1, loggedRecords = 1, Rating = 1 };
            var transformedCoffeeResult = coffeeSelectorCaptor.Invoke(coffee);

            transformedCoffeeResult.Id.Should().Be(1);
            transformedCoffeeResult.BrandId.Should().Be(1);
            transformedCoffeeResult.Country.Should().Be("a");
            transformedCoffeeResult.LoggedRecords.Should().Be(1);
            transformedCoffeeResult.Rating.Should().Be(1);
            transformedCoffeeResult.CoffeeType.Should().Be(Core.Enums.CoffeeType.Arabica);

            var brands = new List<Brand>
            {
                new Brand { Id = 1, Name = "a", ImageUri = "uri-1" },
                new Brand { Id = 2, Name = "b", ImageUri = "uri-2" },
                new Brand { Id = 3, Name = "c", ImageUri = "uri-3" },
            };

            var transformedBrandResult = brandSelectorCaptor.Invoke(brands.First());
            transformedBrandResult.Id.Should().Be(1);
            transformedBrandResult.Name.Should().Be("a");
            transformedBrandResult.ImageUri.Should().Be("uri-1");

            var filteredBrandIds = brands.Where(brandPredicateCaptor.Compile()).Select(x => x.Id);
            filteredBrandIds.Should().Equal(1, 2);
        }
    }
}