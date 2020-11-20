using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Queries.Objects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Queries.Handlers
{
    public class GetRecordQueryHandler : IRequestHandler<GetRecordQuery, RecordDto>
    {
        private readonly IRepository _repository;

        public GetRecordQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<RecordDto> Handle(GetRecordQuery request, CancellationToken cancellationToken)
        {
            var brands = await _repository.SingleAsync<Record, RecordDto>(x => x.Id == request.Id,
                x => new RecordDto
                {
                    DoseIn = x.DoseIn,
                    DoseOut = x.DoseOut,
                    Rating = x.Rating,
                    Time = x.Time
                }).ConfigureAwait(false);

            return brands;
        }
    }
}
