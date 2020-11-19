using Core.DTOs;
using Core.Entities;
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
    public class BrandController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IRepository _repository;

        public BrandController(ILogger<BrandController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("list")]
        public IActionResult GetList()
        {
            var brands = _repository.List<Brand>();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var brand = _repository.Single<Brand>(x => x.Id == id);

            return brand is not null 
                ? Ok(brand)
                : NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewBrandDto newBrand)
        {
            var brand = _repository.InsertAsync(new Brand
            {
                Name = newBrand.Name,
                ImageUri = newBrand.imageUri
            });

            return CreatedAtAction(nameof(Get), new { id = brand.Id }, brand);
        }
    }
}
