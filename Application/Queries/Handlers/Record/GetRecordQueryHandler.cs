using Application.DTOs.Record;
using Core.Entities;
using Infrastructure.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Handlers
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
                    Time = x.Time,
                    CreateDate = x.CreateDate
                }).ConfigureAwait(false);

            return brands;
        }
    }

    public class GetRecordQuery : IRequest<RecordDto>
    {
        public long Id { get; }

        public GetRecordQuery(long id)
        {
            Id = id;
        }
    }
}
