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
    public class CoffeeController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IRepository _repository;

        public CoffeeController(ILogger<BrandController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("list")]
        public IActionResult GetList()
        {
            var coffee = _repository.List<Brand>();
            return Ok(coffee);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var coffee = _repository.Single<Coffee>(x => x.Id == id);

            return coffee is not null 
                ? Ok(coffee)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewCoffeeDto newCoffee)
        {
            var coffee = await _repository.InsertAsync(new Coffee
            {
                BrandId = newCoffee.BrandId,
                Country = newCoffee.Country,
                CoffeeType = Enum.Parse<CoffeeType>(newCoffee.CoffeeType, true)
            }).ConfigureAwait(false);

            return CreatedAtAction(nameof(Get), new { id = coffee.Id }, coffee);
        }
    }
}
