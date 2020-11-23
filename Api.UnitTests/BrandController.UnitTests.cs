using API.Controllers;
using Application.DTOs.Brand;
using Application.Queries.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.UnitTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public async Task BrandController_GetList_Returns_Succes()
        {
            var logger = new Mock<ILogger<BrandController>>();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(
                It.IsAny<GetBrandsQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(new List<BrandDto> { new BrandDto()});


            var controller = new BrandController(logger.Object, mediator.Object);
            var result = await controller.GetList().ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<OkObjectResult>().And.Property(nameof(OkObjectResult.Value)).InstanceOf<List<BrandDto>>());
        }

        [Test]
        public async Task BrandController_GetList_Returns_Not_Found()
        {
            var logger = new Mock<ILogger<BrandController>>();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(
                It.IsAny<GetBrandsQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync((List<BrandDto>) null);


            var controller = new BrandController(logger.Object, mediator.Object);
            var result = await controller.GetList().ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task BrandController_Get_Returns_Succes()
        {
            var logger = new Mock<ILogger<BrandController>>();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(
                It.IsAny<GetBrandQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync( new BrandDto() );


            var controller = new BrandController(logger.Object, mediator.Object);
            var result = await controller.Get(1).ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<OkObjectResult>().And.Property(nameof(OkObjectResult.Value)).InstanceOf<BrandDto>());
        }

        [Test]
        public async Task BrandController_Get_Returns_Not_Found()
        {
            var logger = new Mock<ILogger<BrandController>>();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(
                It.IsAny<GetBrandQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync((BrandDto)null);


            var controller = new BrandController(logger.Object, mediator.Object);
            var result = await controller.Get(1).ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}