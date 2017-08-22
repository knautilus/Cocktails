using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

using Cocktails.Data.Domain;
using Cocktails.Services;
using Cocktails.ViewModels;

namespace Cocktails.Api.Controllers
{
    /// <summary>
    /// API Controller for Cocktails
    /// </summary>
    [Route("api/[controller]")]
    public class CocktailsController : Controller
    {
        private readonly IService<Cocktail, CocktailModel> _service;

        /// <summary>
        /// Constructor
        /// </summary>
        public CocktailsController(IService<Cocktail, CocktailModel> service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns a collection of cocktails
        /// </summary>
        /// <param name="cancellationToken"></param>
        [HttpGet]
        [SwaggerResponse(200, description: "Success", type: typeof(CocktailModel[]))]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _service.GetAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Returns a specific cocktail
        /// </summary>
        /// <param name="id">Item Id (GUID)</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{id:guid}", Name = "GetCocktail")]
        [SwaggerResponse(200, description: "Item found", type: typeof(CocktailModel))]
        [SwaggerResponse(400, description: "Item not found", type: typeof(IdModel))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound(new IdModel(id));
            }
            return Ok(result);
        }

        /// <summary>
        /// Adds a cocktail
        /// </summary>
        /// <param name="model">Cocktail JSON representation</param>
        /// <param name="cancellationToken"></param>
        [HttpPost]
        [SwaggerResponse(201, description: "Item created successfully", type: typeof(CocktailModel))]
        public async Task<IActionResult> PostAsync([FromBody, Required] CocktailModel model, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(model, cancellationToken);
            return CreatedAtRoute("GetCocktail", new IdModel(result.Id), result);
        }

        /// <summary>
        /// Updates a specific cocktail
        /// </summary>
        /// <param name="id">Item Id (GUID)</param>
        /// <param name="model">Cocktail JSON representation</param>
        /// <param name="cancellationToken"></param>
        [HttpPut("{id:guid}")]
        [SwaggerResponse(200, description: "Item updated successfully", type: typeof(CocktailModel))]
        [SwaggerResponse(404, description: "Item not found", type: typeof(IdModel))]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody, Required] CocktailModel model, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(id, model, cancellationToken);
            if (result == null)
            {
                return NotFound(new IdModel(id));
            }
            return Ok(result);
        }

        /// <summary>
        /// Deletes a specific cocktail
        /// </summary>
        /// <param name="id">Item Id (GUID)</param>
        /// <param name="cancellationToken"></param>
        [HttpDelete("{id:guid}")]
        [SwaggerResponse(204, description: "Item deleted successfully")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
