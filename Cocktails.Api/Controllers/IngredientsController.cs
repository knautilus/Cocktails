using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Cocktails.Data.Domain;
using Cocktails.Services;

namespace Cocktails.Api.Controllers
{
    [Route("api/[controller]")]
    public class IngredientsController : Controller
    {
        private readonly IService<Ingredient> _service;

        public IngredientsController(IService<Ingredient> service)
        {
            _service = service;
        }

        // GET api/ingredients/5
        [HttpGet("{id:guid}", Name = "GetIngredient")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        // POST api/ingredients
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Ingredient model, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(model, cancellationToken);
            return CreatedAtRoute("GetIngredient", new { Id = result.Id }, result);
        }

        // PUT api/ingredients/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] Ingredient model, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(id, model, cancellationToken);
            return Accepted(result);
        }
    }
}
