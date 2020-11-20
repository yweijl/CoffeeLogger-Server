using Core.DTOs;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IRepository _repository;

        public RecordController(ILogger<BrandController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("list")]
        public IActionResult GetList()
        {
            var record = _repository.List<Brand>();
            return Ok(record);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var record = _repository.Single<Coffee>(x => x.Id == id);

            return record is not null
                ? Ok(record)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewRecordDto newRecord)
        {
            var existingFlavors = _repository.List<Flavor>(
            x => newRecord.Flavors.Contains(x.Name, StringComparer.InvariantCultureIgnoreCase));

            var newFlavors = newRecord.Flavors
                .Where(x => !existingFlavors.Any(y => x.Contains(y.Name, StringComparison.InvariantCultureIgnoreCase)))
                .Select(x => new Flavor { Name = x }).ToList();

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
                    DoseIn = newRecord.DoseIn,
                    DoseOut = newRecord.DoseOut,
                    Time = newRecord.Time,
                    CoffeeId = newRecord.CoffeeId,
                    Rating = newRecord.Rating,
                }).ConfigureAwait(false);

            var recordFlavors =
                existingFlavors.Select(
                    x => new RecordFlavors
                    {
                        FlavorId = x.Id,
                        RecordId = record.Id
                    }).ToList();

            await _repository.InsertRangeAsync(recordFlavors).ConfigureAwait(false);


            return CreatedAtAction(nameof(Get), new { id = record.Id }, record);
        }
    }
}
