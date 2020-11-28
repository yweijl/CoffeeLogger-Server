using Application.Commands;
using Application.DTOs.Brand;
using Core.Entities;
using FluentAssertions;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;
using Shared.UnitTests;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.CommandHandlers.UnitTests
{
    [TestFixture]
    public class NewBrandCommandHandlerUnitTests
    {
        [Test]
        public async Task NewBrandCommandHandler_Works()
        {
            var repository = new Mock<IRepository>();
           
            var brand = new Brand { Id = 1, Name = "a", ImageUri = "uri" };

            var captor = new ArgumentCaptor<Brand>();
            repository.Setup(x => x.InsertAsync(captor.Capture()))
                .ReturnsAsync(brand);
            
            var handler = new NewBrandCommandHandler(repository.Object);
            
            var result = await handler
                .Handle(new NewBrandCommand(brand.Name, brand.ImageUri), It.IsAny<CancellationToken>())
                .ConfigureAwait(false);

            result.Should().BeOfType<BrandDto>();
            result.Id.Should().Equals(brand.Id);
            result.ImageUri.Should().Equals(brand.ImageUri);
            result.Name.Should().Equals(brand.Name);

            captor.Value.Name.Should().Be(brand.Name);
            captor.Value.ImageUri.Should().Be(brand.ImageUri);

            repository.Verify(x => x.InsertAsync(captor.Value), Times.Once);
            repository.VerifyNoOtherCalls();
        }
    }
}
