using API.Controllers;
using Application.Commands;
using Application.DTOs.Record;
using Application.Queries.RecordHandlers;
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
    public class RecordControllerUnitTests
    {
        [Test]
        public async Task RecordController_GetList_Returns_Succes()
        {
            var logger = new Mock<ILogger<RecordController>>();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(
                It.IsAny<GetRecordsQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(new List<RecordDto> { new RecordDto()});


            var controller = new RecordController(logger.Object, mediator.Object);
            var result = await controller.GetList().ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<OkObjectResult>().And.Property(nameof(OkObjectResult.Value)).InstanceOf<List<RecordDto>>());
        }

        [Test]
        public async Task RecordController_GetList_Returns_Not_Found()
        {
            var logger = new Mock<ILogger<RecordController>>();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(
                It.IsAny<GetRecordsQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync((List<RecordDto>) null);


            var controller = new RecordController(logger.Object, mediator.Object);
            var result = await controller.GetList().ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task RecordController_Get_Returns_Succes()
        {
            var logger = new Mock<ILogger<RecordController>>();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(
                It.IsAny<GetRecordQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync( new RecordDto() );


            var controller = new RecordController(logger.Object, mediator.Object);
            var result = await controller.Get(1).ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<OkObjectResult>().And.Property(nameof(OkObjectResult.Value)).InstanceOf<RecordDto>());
        }

        [Test]
        public async Task RecordController_Get_Returns_Not_Found()
        {
            var logger = new Mock<ILogger<RecordController>>();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(
                It.IsAny<GetRecordQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync((RecordDto)null);


            var controller = new RecordController(logger.Object, mediator.Object);
            var result = await controller.Get(1).ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task RecordController_Post_Works()
        {
            var logger = new Mock<ILogger<RecordController>>();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(
                It.IsAny<NewRecordCommand>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(new RecordDto { Id = 1});


            var controller = new RecordController(logger.Object, mediator.Object);
            var result = 
                await controller.Post(
                    new NewRecordDto())
                .ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        }
    }
}