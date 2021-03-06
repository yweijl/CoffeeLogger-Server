﻿using Application.DTOs.Record;
using Core.Entities;
using Infrastructure.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.RecordHandlers
{
    public class GetRecordsQueryHandler : IRequestHandler<GetRecordsQuery, List<RecordDto>>
    {
        private readonly IRepository _repository;

        public GetRecordsQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RecordDto>> Handle(GetRecordsQuery request, CancellationToken cancellationToken)
        {
            var Records = await _repository.ListAsync<Record, RecordDto>(
                x => new RecordDto
                {
                    Id = x.Id,
                    DoseIn = x.DoseIn,
                    DoseOut = x.DoseOut,
                    Rating = x.Rating,
                    Time = x.Time,
                    CreateDate = x.CreateDate
                }).ConfigureAwait(false);

            return Records;
        }
    }

    public class GetRecordsQuery : IRequest<List<RecordDto>>
    { }
}
