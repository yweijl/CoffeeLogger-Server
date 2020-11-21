using Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Objects
{
    public class GetRecordsQuery : IRequest<List<RecordDto>>
    {}
}
