using Application.DTOs;
using MediatR;

namespace Application.Queries.Objects
{
    public class GetRecordQuery : IRequest<RecordDto>
    {
        public long Id { get; }

        public GetRecordQuery(long id)
        {
            Id = id;
        }
    }
}
