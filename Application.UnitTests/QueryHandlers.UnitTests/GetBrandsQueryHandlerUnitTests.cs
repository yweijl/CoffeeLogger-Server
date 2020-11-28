using Application.DTOs.Brand;
using Application.Queries.Handlers;
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
    public class GetBrandsQueryHandlerUnitTests
    {

        [Test]
        public async Task GetBrandsQueryHandler_Works()
        {
            var repository = new Mock<IRepository>();

            var selectorCaptor = new ExpressionCaptor<Brand, BrandDto>();

            repository.Setup(x => x.ListAsync(selectorCaptor.Capture())).ReturnsAsync(new List<BrandDto>());

            var queryHandler = new GetBrandsQueryHandler(repository.Object);

            var result = await queryHandler
                .Handle(new GetBrandsQuery(), It.IsAny<CancellationToken>())
                .ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<List<BrandDto>>());
            var brand = new Brand { Id = 1, Name = "a", ImageUri = "uri1" };
            var transformedResult = selectorCaptor.Invoke(brand);

            transformedResult.Id.Should().Be(1);
            transformedResult.Name.Should().Be("a");
            transformedResult.ImageUri.Should().Be("uri1");
        }
    }
}