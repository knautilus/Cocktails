using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

using Cocktails.Common.Exceptions;
using Cocktails.Common.Models;
using Cocktails.Data.Domain;
using Cocktails.Services;
using Cocktails.ViewModels;

namespace Cocktails.Api.Controllers
{
    /// <summary>
    /// API Controller for Cocktails
    /// </summary>
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
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
        /// <param name="context">Sorting and paging parameters</param>
        /// <param name="cancellationToken"></param>
        [HttpGet]
        [SwaggerResponse(200, description: "Success", type: typeof(CollectionWrapper<CocktailModel>))]
        public async Task<IActionResult> GetAllAsync([FromQuery] QueryContext context, CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(context, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Returns a specific cocktail
        /// </summary>
        /// <param name="id">Item Id (GUID)</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{id:guid}", Name = "GetCocktail")]
        [SwaggerResponse(200, description: "Item found", type: typeof(CocktailModel))]
        [SwaggerResponse(404, description: "Item not found", type: typeof(ApiErrorResponse))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound(new ApiErrorResponse(404, "Cocktail with id '{id}' not found"));
            }
            return Ok(result);
        }

        /// <summary>
        /// Adds a cocktail
        /// </summary>
        /// <param name="model">Cocktail JSON representation</param>
        /// <param name="cancellationToken"></param>
        [Authorize]
        [HttpPost]
        [SwaggerResponse(201, description: "Item created successfully", type: typeof(CocktailModel))]
        [SwaggerResponse(400, description: "Invalid model state", type: typeof(ApiErrorResponse))]
        public async Task<IActionResult> PostAsync([FromBody, Required] CocktailModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiBadRequestResponse(ModelState));
            }

            try
            {
                var result = await _service.CreateAsync(model, cancellationToken);
                return CreatedAtRoute("GetCocktail", new { Id = result.Id }, result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }

        /// <summary>
        /// Updates a specific cocktail
        /// </summary>
        /// <param name="id">Item Id (GUID)</param>
        /// <param name="model">Cocktail JSON representation</param>
        /// <param name="cancellationToken"></param>
        [Authorize]
        [HttpPut("{id:guid}")]
        [SwaggerResponse(200, description: "Item updated successfully", type: typeof(CocktailModel))]
        [SwaggerResponse(400, description: "Invalid model state", type: typeof(ApiErrorResponse))]
        [SwaggerResponse(404, description: "Item not found", type: typeof(ApiErrorResponse))]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody, Required] CocktailModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiBadRequestResponse(ModelState));
            }

            try
            {
                var result = await _service.UpdateAsync(id, model, cancellationToken);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiErrorResponse(404, ex.Message));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }

        /// <summary>
        /// Deletes a specific cocktail
        /// </summary>
        /// <param name="id">Item Id (GUID)</param>
        /// <param name="cancellationToken"></param>
        [Authorize]
        [HttpDelete("{id:guid}")]
        [SwaggerResponse(204, description: "Item deleted successfully")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.DeleteAsync(id, cancellationToken);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiErrorResponse(404, ex.Message));
            }
        }
    }
}
