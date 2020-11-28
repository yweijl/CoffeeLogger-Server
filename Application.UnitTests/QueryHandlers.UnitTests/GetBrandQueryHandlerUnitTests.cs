using Application.DTOs.Brand;
using Application.Queries.BrandHandlers;
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
    public class GetBrandQueryHandlerUnitTests
    {
        [Test]
        public async Task GetBrandQueryHandler_Works()
        {
            var brands = new List<Brand> {
                new Brand { Id = 1, Name = "a", ImageUri = "uri1" }, 
                new Brand { Id = 2, Name = "b", ImageUri = "uri2" } 
            };

            var repository = new Mock<IRepository>();

            var predicateCaptor = new ExpressionCaptor<Brand, bool>();
            var selectorCaptor = new ExpressionCaptor<Brand, BrandDto>();

            repository.Setup(x => x.SingleAsync(
                predicateCaptor.Capture(),
                selectorCaptor.Capture())
            ).ReturnsAsync(new BrandDto());

            var queryHandler = new GetBrandQueryHandler(repository.Object);

            var result = await queryHandler
                .Handle(new GetBrandQuery(1), It.IsAny<CancellationToken>())
                .ConfigureAwait(false);
            
            Assert.That(result, Is.InstanceOf<BrandDto>());
       
            var actualResult = brands.Single(predicateCaptor.Compile());
            var transformedResult = selectorCaptor.Invoke(actualResult);

            transformedResult.Id.Should().Be(1);
            transformedResult.Name.Should().Be("a");
            transformedResult.ImageUri.Should().Be("uri1");
        }
    }
}