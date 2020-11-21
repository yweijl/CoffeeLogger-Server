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
        public async Task<IActionResult> GetList()
        {
            var coffees = await _mediatr.Send(new GetCoffeesQuery()).ConfigureAwait(false);

            return coffees is not null
                ? Ok(coffees)
                : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var coffee = await _mediatr.Send(new GetCoffeeQuery(id)).ConfigureAwait(false);

            return coffee is not null 
                ? Ok(coffee)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewCoffeeDto newCoffee)
        {
            var coffee = await _mediatr.Send(

                new NewCoffeeCommand(newCoffee.BrandId, newCoffee.CoffeeType, newCoffee.Country))
                .ConfigureAwait(false);
          
            return CreatedAtAction(nameof(Get), new { id = coffee.Id }, coffee);
        }
    }
}
