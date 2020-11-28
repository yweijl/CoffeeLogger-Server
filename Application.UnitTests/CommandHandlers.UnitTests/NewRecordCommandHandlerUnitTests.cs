using Application.Commands;
using Application.DTOs.Record;
using Core.Entities;
using FluentAssertions;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;
using Shared.UnitTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.CommandHandlers.UnitTests
{
    [TestFixture]
    public class NewRecordCommandHandlerUnitTests
    {
        [Test]
        public async Task NewRecordCommandHandler_Works()
        {
            var repository = new Mock<IRepository>();

            var record = GetRecord();

            var flavors = new string[]{ "a","b","c"};

            repository.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Flavor, bool>>>()))
                .ReturnsAsync(new List<Flavor> { new Flavor { Id = 1, Name = "a", } });

            repository.Setup(x => x.InsertAsync(It.IsAny<Record>()))
                .ReturnsAsync(record);

            repository.Setup(x => x.InsertRangeAsync(It.IsAny<List<Flavor>>()))
                .ReturnsAsync(
                new List<Flavor> { 
                    new Flavor { Id = 2, Name = "b", }, 
                    new Flavor { Id = 3, Name = "c", } 
                });

            repository.Setup(x => x.InsertRangeAsync(It.IsAny<List<RecordFlavor>>()));

            var handler = new NewRecordCommandHandler(repository.Object);
            
            var result = await handler
                .Handle(new NewRecordCommand(record.CoffeeId, record.DoseIn, record.DoseOut, record.Time, flavors, record.Rating), 
                        It.IsAny<CancellationToken>())
                .ConfigureAwait(false);

            result.Should().BeOfType<RecordDto>();
            result.Id.Should().Equals(record.Id);
            result.DoseIn.Should().Equals(record.DoseIn);
            result.DoseOut.Should().Equals(record.DoseOut);
            result.Time.Should().Equals(record.Time);
        }

        [Test]
        public async Task NewRecordCommandHandler_Verify_Dependencies()
        {
            var repository = new Mock<IRepository>();

            var record = GetRecord();

            var flavors = new string[] { "a", "b", "c" };

            var flavorPredicateCaptor = new ExpressionCaptor<Flavor, bool>();

            repository.Setup(x => x.ListAsync(flavorPredicateCaptor.Capture()))
                .ReturnsAsync(new List<Flavor> { new Flavor { Id = 1, Name = "a", } });

            var recordCaptor = new ArgumentCaptor<Record>();

            repository.Setup(x => x.InsertAsync(recordCaptor.Capture()))
                .ReturnsAsync(record);

            var flavorCaptor = new ArgumentCaptor<List<Flavor>>();

            repository.Setup(x => x.InsertRangeAsync(flavorCaptor.Capture()))
                .ReturnsAsync(
                new List<Flavor> {
                    new Flavor { Id = 2, Name = "b", },
                    new Flavor { Id = 3, Name = "c", }
                });

            var recordFlavorCaptor = new ArgumentCaptor<List<RecordFlavor>>();

            repository.Setup(x => x.InsertRangeAsync(recordFlavorCaptor.Capture()));

            var handler = new NewRecordCommandHandler(repository.Object);

            var result = await handler
                .Handle(new NewRecordCommand(record.CoffeeId, record.DoseIn, record.DoseOut, record.Time, flavors, record.Rating),
                        It.IsAny<CancellationToken>())
                .ConfigureAwait(false);

            repository.Verify(x => x.InsertAsync(recordCaptor.Value), Times.Once);
            repository.Verify(x => x.InsertRangeAsync(flavorCaptor.Value), Times.Once);
            repository.Verify(x => x.InsertRangeAsync(recordFlavorCaptor.Value), Times.Once);
            repository.Verify(x => x.ListAsync(flavorPredicateCaptor.Value), Times.Once);
            repository.VerifyNoOtherCalls();
        }

        [Test]
        public async Task NewRecordCommandHandler_Verify_Expressions()
        {
            var repository = new Mock<IRepository>();

            var record = GetRecord();

            var flavors = new string[] { "a", "b", "c" };

            var flavorPredicateCaptor = new ExpressionCaptor<Flavor, bool>();

            repository.Setup(x => x.ListAsync(flavorPredicateCaptor.Capture()))
                .ReturnsAsync(new List<Flavor> { new Flavor { Id = 1, Name = "a", } });

            var recordCaptor = new ArgumentCaptor<Record>();

            repository.Setup(x => x.InsertAsync(recordCaptor.Capture()))
                .ReturnsAsync(record);

            var insertFlavorCaptor = new ArgumentCaptor<List<Flavor>>();

            repository.Setup(x => x.InsertRangeAsync(insertFlavorCaptor.Capture()))
                .ReturnsAsync(
                new List<Flavor> {
                    new Flavor { Id = 2, Name = "b", },
                    new Flavor { Id = 3, Name = "c", }
                });

            var recordFlavorCaptor = new ArgumentCaptor<List<RecordFlavor>>();

            repository.Setup(x => x.InsertRangeAsync(recordFlavorCaptor.Capture()));

            var handler = new NewRecordCommandHandler(repository.Object);

            var result = await handler
                .Handle(new NewRecordCommand(record.CoffeeId, record.DoseIn, record.DoseOut, record.Time, flavors, record.Rating),
                        It.IsAny<CancellationToken>())
                .ConfigureAwait(false);

            var existingFlavors = new List<Flavor> { new Flavor { Name = "a" }, new Flavor { Name = "d" }, new Flavor { Name = "e" }, };
            var queriedFlavors = existingFlavors.Where(flavorPredicateCaptor.Compile());

            queriedFlavors.Select(x => x.Name).Should().Equal("a");

            recordCaptor.Value.CoffeeId.Should().Be(record.CoffeeId);
            recordCaptor.Value.DoseIn.Should().Be(record.DoseIn);
            recordCaptor.Value.DoseOut.Should().Be(record.DoseOut);
            recordCaptor.Value.Time.Should().Be(record.Time);

            insertFlavorCaptor.Value.Select(x => x.Name).Should().Equal("b", "c");
            recordFlavorCaptor
                .Value
                .Select(x => (x.FlavorId, x.RecordId))
                .Should().Equal(
                new List<(long, long)> 
                {
                    (1,1),
                    (2,1), 
                    (3,1) 
                });
        }

        private static Record GetRecord() => 
            new Record
            {
                Id = 1,
                CreateDate = DateTime.Now,
                CoffeeId = 1,
                DoseIn = 1,
                DoseOut = 2,
                Rating = 1
            };
    }
}
