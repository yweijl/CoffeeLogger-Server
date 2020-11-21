using Application.Commands.Handlers;
using Application.DTOs.Record;
using Application.Queries.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class RecordController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IMediator _mediatr;

        public RecordController(ILogger<BrandController> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(List<RecordDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList()
        {
            var records = await _mediatr.Send(new GetRecordsQuery()).ConfigureAwait(false);
            
            return records is not null
                ? Ok(records)
                : NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RecordDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task <IActionResult> Get(long id)
        {
            var record = await _mediatr.Send(new GetRecordQuery(id)).ConfigureAwait(false);

            return record is not null
                ? Ok(record)
                : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(RecordDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] NewRecordDto newRecord)
        {
            var record = await _mediatr.Send(new NewRecordCommand(
                newRecord.CoffeeId, 
                newRecord.DoseIn, 
                newRecord.DoseOut, 
                newRecord.Time, 
                newRecord.Flavors, 
                newRecord.Rating
                )).ConfigureAwait(false);

            return CreatedAtAction(nameof(Get), new { id = record.Id }, record);
        }
    }
}
