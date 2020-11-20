using Core.DTOs;
using MediatR;

namespace Core.Commands.Objects
{
    public class NewRecordCommand : IRequest<RecordDto>
    {
        public NewRecordCommand()
        {
        }

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
