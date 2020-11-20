﻿using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Queries.Objects;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Queries.Handlers
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
                    DoseIn = x.DoseIn,
                    DoseOut = x.DoseOut,
                    Rating = x.Rating,
                    Time = x.Time
                }).ConfigureAwait(false);

            return Records;
        }
    }
}
