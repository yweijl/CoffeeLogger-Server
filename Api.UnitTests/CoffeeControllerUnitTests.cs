using API.Controllers;
using Application.Commands;
using Application.DTOs.Coffee;
using Application.Queries.CoffeeHandlers;
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
    public class CoffeeControllerUnitTests
    {
        [Test]
        public async Task CoffeeController_GetList_Returns_Succes()
        {
            var logger = new Mock<ILogger<CoffeeController>>();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(
                It.IsAny<GetCoffeesQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(new List<CoffeeDto> { new CoffeeDto()});


            var controller = new CoffeeController(logger.Object, mediator.Object);
            var result = await controller.GetList().ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<OkObjectResult>().And.Property(nameof(OkObjectResult.Value)).InstanceOf<List<CoffeeDto>>());
        }

        [Test]
        public async Task CoffeeController_GetList_Returns_Not_Found()
        {
            var logger = new Mock<ILogger<CoffeeController>>();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(
                It.IsAny<GetCoffeesQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync((List<CoffeeDto>) null);


            var controller = new CoffeeController(logger.Object, mediator.Object);
            var result = await controller.GetList().ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task CoffeeController_Get_Returns_Succes()
        {
            var logger = new Mock<ILogger<CoffeeController>>();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(
                It.IsAny<GetCoffeeQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync( new CoffeeDto() );


            var controller = new CoffeeController(logger.Object, mediator.Object);
            var result = await controller.Get(1).ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<OkObjectResult>().And.Property(nameof(OkObjectResult.Value)).InstanceOf<CoffeeDto>());
        }

        [Test]
        public async Task CoffeeController_Get_Returns_Not_Found()
        {
            var logger = new Mock<ILogger<CoffeeController>>();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(
                It.IsAny<GetCoffeeQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync((CoffeeDto)null);


            var controller = new CoffeeController(logger.Object, mediator.Object);
            var result = await controller.Get(1).ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task CoffeeController_Post_Works()
        {
            var logger = new Mock<ILogger<CoffeeController>>();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(
                It.IsAny<NewCoffeeCommand>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(new CoffeeDto { Id = 1});


            var controller = new CoffeeController(logger.Object, mediator.Object);
            var result = 
                await controller.Post(
                    new NewCoffeeDto())
                .ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        }
    }
}