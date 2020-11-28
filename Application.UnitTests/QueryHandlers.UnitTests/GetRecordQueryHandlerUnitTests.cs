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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.QueryHandlers.UnitTests
{
    public class GetRecordQueryHandlerUnitTests
    {
        [Test]
        public async Task GetRecordQueryHandler_Works()
        {
            var Records = new List<Record> {
                new Record { Id = 1, CoffeeId = 1, DoseIn = 1, DoseOut = 1, Rating = 1, Time = 1, CreateDate = DateTime.Today }, 
                new Record { Id = 2, CoffeeId = 2, DoseIn = 2, DoseOut = 2, Rating = 2, Time = 2, CreateDate = DateTime.Today.AddDays(-1) }, 
            };

            var repository = new Mock<IRepository>();

            var predicateCaptor = new ExpressionCaptor<Record, bool>();
            var selectorCaptor = new ExpressionCaptor<Record, RecordDto>();

            repository.Setup(x => x.SingleAsync(
                predicateCaptor.Capture(),
                selectorCaptor.Capture())
            ).ReturnsAsync(new RecordDto());

            var queryHandler = new GetRecordQueryHandler(repository.Object);

            var result = await queryHandler
                .Handle(new GetRecordQuery(1), It.IsAny<CancellationToken>())
                .ConfigureAwait(false);

            result.Should().BeOfType<RecordDto>();
       
            var actualResult = Records.Single(predicateCaptor.Compile());
            var transformedResult = selectorCaptor.Invoke(actualResult);

            transformedResult.Id.Should().Be(1);
            transformedResult.DoseIn.Should().Be(1);
            transformedResult.DoseOut.Should().Be(1);
            transformedResult.Rating.Should().Be(1);
            transformedResult.Time.Should().Be(1);
            transformedResult.CreateDate.Should().Be(DateTime.Today);
        }
    }
}