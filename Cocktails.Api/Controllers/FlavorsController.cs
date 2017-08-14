using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Cocktails.Data.Domain;
using Cocktails.Services;

namespace Cocktails.Api.Controllers
{
    [Route("api/[controller]")]
    public class FlavorsController : Controller
    {
        private readonly IService<Flavor> _service;

        public FlavorsController(IService<Flavor> service)
        {
            _service = service;
        }

        // GET api/flavors
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _service.GetAsync(cancellationToken);
            return Ok(result);
        }

        // GET api/flavors/5
        [HttpGet("{id:guid}", Name = "GetFlavor")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound(new { Id = id });
            }
            return Ok(result);
        }

        // POST api/flavors
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Flavor model, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(model, cancellationToken);
            return CreatedAtRoute("GetFlavor", new { Id = result.Id }, result);
        }

        // PUT api/flavors/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] Flavor model, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(id, model, cancellationToken);
            if (result == null)
            {
                return NotFound(new { Id = id });
            }
            return Accepted(result);
        }
    }
}
