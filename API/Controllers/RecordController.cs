using Application.Commands.Objects;
using Application.DTOs;
using Application.Queries.Objects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> GetList()
        {
            var records = await _mediatr.Send(new GetRecordsQuery()).ConfigureAwait(false);
            
            return records is not null
                ? Ok(records)
                : NotFound();
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> Get(long id)
        {
            var record = await _mediatr.Send(new GetRecordQuery(id)).ConfigureAwait(false);

            return record is not null
                ? Ok(record)
                : NotFound();
        }

        [HttpPost]
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
