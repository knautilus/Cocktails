using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Cocktails.Data.Domain;
using Cocktails.Services;

namespace Cocktails.Api.Controllers
{
    [Route("api/[controller]")]
    public class IngredientCategoriesController : Controller
    {
        private readonly IService<IngredientCategory> _service;

        public IngredientCategoriesController(IService<IngredientCategory> service)
        {
            _service = service;
        }

        // GET api/ingredientcategories/5
        [HttpGet("{id:guid}", Name = "GetIngredientCategory")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        // POST api/ingredientcategories
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] IngredientCategory model, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(model, cancellationToken);
            return CreatedAtRoute("GetIngredientCategory", new { Id = result.Id }, result);
        }

        // PUT api/ingredientcategories/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] IngredientCategory model, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(id, model, cancellationToken);
            return Accepted(result);
        }
    }
}
