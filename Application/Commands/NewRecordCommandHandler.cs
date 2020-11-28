using Application.DTOs.Record;
using Core.Entities;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class NewRecordCommandHandler : IRequestHandler<NewRecordCommand, RecordDto>
    {
        private readonly IRepository _repository;

        public NewRecordCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<RecordDto> Handle(NewRecordCommand request, CancellationToken cancellationToken)
        {
            var existingFlavors =
                await _repository.ListAsync<Flavor>(
                    x => request.Flavors
                    .Contains(x.Name))
                .ConfigureAwait(false);

            var newFlavors = request.Flavors
                .Where(x => !existingFlavors.Any(
                    y => x.Contains(
                        y.Name, StringComparison.InvariantCultureIgnoreCase)
                    ))
                .Select(x => new Flavor { Name = x })
                .ToList();

            if (newFlavors.Count != 0)
            {
                var addedFlavors =
                await _repository.InsertRangeAsync(newFlavors)
                .ConfigureAwait(false);

                existingFlavors.AddRange(addedFlavors);
            }

            var record = await _repository.InsertAsync(
                new Record
                {
                    DoseIn = request.DoseIn,
                    DoseOut = request.DoseOut,
                    Time = request.Time,
                    CoffeeId = request.CoffeeId,
                    Rating = request.Rating,
                }).ConfigureAwait(false);

            var recordFlavors =
                existingFlavors.Select(
                    x => new RecordFlavor
                    {
                        FlavorId = x.Id,
                        RecordId = record.Id
                    }).ToList();

            await _repository.InsertRangeAsync(recordFlavors).ConfigureAwait(false);

            return new RecordDto
            {
                Id = record.Id,
                DoseIn = record.DoseIn,
                DoseOut = record.DoseOut,
                Time = record.Time,
                Rating = record.Rating
            };
        }
    }
    public class NewRecordCommand : IRequest<RecordDto>
    {
        public NewRecordCommand(long coffeeId, decimal doseIn, decimal doseOut, decimal time, string[] flavors, decimal rating)
        {
            CoffeeId = coffeeId;
            DoseIn = doseIn;
            DoseOut = doseOut;
            Time = time;
            Flavors = flavors;
            Rating = rating;
        }

        public long CoffeeId { get; }
        public decimal DoseIn { get; }
        public decimal DoseOut { get; }
        public decimal Time { get; }
        public string[] Flavors { get; }
        public decimal Rating { get; }
    }
}