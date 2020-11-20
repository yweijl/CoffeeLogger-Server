using Core.Commands.Objects;
using Core.DTOs;
using Core.Queries.Objects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> GetList()
        {
            var brands = await mediatr.Send(new GetBrandsQuery()).ConfigureAwait(false);
            
            return brands is not null
                ? Ok(brands)
                : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var brand = await mediatr.Send(new GetBrandQuery(id)).ConfigureAwait(false);

            return brand is not null 
                ? Ok(brand)
                : NotFound();
        }

        [HttpPost]
        public async Task <IActionResult> Post([FromBody] NewBrandDto newBrand)
        {
            var brand = await mediatr.Send(
                new NewBrandCommand(newBrand.Name, newBrand.imageUri)
                ).ConfigureAwait(false);

            return CreatedAtAction(nameof(Get), new { id = brand.Id }, brand);
        }
    }
}
