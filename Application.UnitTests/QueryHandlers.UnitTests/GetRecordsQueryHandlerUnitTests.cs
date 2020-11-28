using Application.DTOs.Record;
using Application.Queries.RecordHandlers;
using Core.Entities;
using FluentAssertions;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;
using Shared.UnitTests;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.QueryHandlers.UnitTests
{
    public class GetRecordsQueryHandlerUnitTests
    {

        [Test]
        public async Task GetRecordsQueryHandler_Works()
        {
            var repository = new Mock<IRepository>();

            var selectorCaptor = new ExpressionCaptor<Record, RecordDto>();

            repository.Setup(x => x.ListAsync(selectorCaptor.Capture())).ReturnsAsync(new List<RecordDto>());

            var queryHandler = new GetRecordsQueryHandler(repository.Object);

            var result = await queryHandler
                .Handle(new GetRecordsQuery(), It.IsAny<CancellationToken>())
                .ConfigureAwait(false);

            Assert.That(result, Is.InstanceOf<List<RecordDto>>());

            var record = new Record { Id = 1, CoffeeId = 1, DoseIn = 1, DoseOut = 1, Rating = 1, Time = 1, CreateDate = DateTime.Today }; 

            var transformedResult = selectorCaptor.Invoke(record);

            transformedResult.Id.Should().Be(1);
            transformedResult.DoseIn.Should().Be(1);
            transformedResult.DoseOut.Should().Be(1);
            transformedResult.Rating.Should().Be(1);
            transformedResult.Time.Should().Be(1);
            transformedResult.CreateDate.Should().Be(DateTime.Today);
        }
    }
}