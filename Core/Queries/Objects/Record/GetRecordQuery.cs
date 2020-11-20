using Core.DTOs;
using MediatR;

namespace Core.Queries.Objects
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
