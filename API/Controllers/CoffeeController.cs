using Application.Commands.Handlers;
using Application.DTOs.Coffee;
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
    public class CoffeeController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IMediator _mediatr;

        public CoffeeController(ILogger<BrandController> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(List<CoffeeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList()
        {
            var coffees = await _mediatr.Send(new GetCoffeesQuery()).ConfigureAwait(false);

            return coffees is not null
                ? Ok(coffees)
                : NotFound();
        }

        [HttpGet("detailedList")]
        [ProducesResponseType(typeof(List<DetailedCoffeeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDetailedList()
        {
            var detailedCoffees = await _mediatr.Send(new GetDetailedCoffeesQuery()).ConfigureAwait(false);

            return detailedCoffees is not null
                ? Ok(detailedCoffees)
                : NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CoffeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(long id)
        {
            var coffee = await _mediatr.Send(new GetCoffeeQuery(id)).ConfigureAwait(false);

            return coffee is not null 
                ? Ok(coffee)
                : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(CoffeeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] NewCoffeeDto newCoffee)
        {
            var coffee = await _mediatr.Send(

                new NewCoffeeCommand(newCoffee.BrandId, newCoffee.CoffeeType, newCoffee.Country))
                .ConfigureAwait(false);
          
            return CreatedAtAction(nameof(Get), new { id = coffee.Id }, coffee);
        }
    }
}
