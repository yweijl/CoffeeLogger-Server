using Application.Commands.Handlers;
using Application.DTOs.Brand;
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
    public class BrandController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IMediator mediatr;

        public BrandController(ILogger<BrandController> logger, IMediator mediatr)
        {
            _logger = logger;
            this.mediatr = mediatr;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(List<BrandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList()
        {
            var brands = await mediatr.Send(new GetBrandsQuery()).ConfigureAwait(false);
            
            return brands is not null
                ? Ok(brands)
                : NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BrandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(long id)
        {
            var brand = await mediatr.Send(new GetBrandQuery(id)).ConfigureAwait(false);

            return brand is not null 
                ? Ok(brand)
                : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(BrandDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task <IActionResult> Post([FromBody] NewBrandDto newBrand)
        {
            var brand = await mediatr.Send(
                new NewBrandCommand(newBrand.Name, newBrand.imageUri)
                ).ConfigureAwait(false);

            return CreatedAtAction(nameof(Get), new { id = brand.Id }, brand);
        }
    }
}
