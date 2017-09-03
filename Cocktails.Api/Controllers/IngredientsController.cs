using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

using Cocktails.Common.Exceptions;
using Cocktails.Common.Models;
using Cocktails.Services;
using Cocktails.ViewModels;

namespace Cocktails.Api.Controllers
{
    /// <summary>
    /// API Controller for Ingredients
    /// </summary>
    [Route("v{version:apiVersion}")]
    [ApiVersion("1")]
    public class IngredientsController : Controller
    {
        private readonly IIngredientService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public IngredientsController(IIngredientService service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns a collection of ingredients
        /// </summary>
        /// <param name="context">Sorting and paging parameters</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("ingredients")]
        [SwaggerResponse(200, description: "Success", type: typeof(CollectionWrapper<IngredientModel>))]
        public async Task<IActionResult> GetAllAsync([FromQuery] QueryContext context, CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(context, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Returns a specific ingredient
        /// </summary>
        /// <param name="id">Item Id (GUID)</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("ingredients/{id:guid}", Name = "GetIngredient")]
        [SwaggerResponse(200, description: "Item found", type: typeof(IngredientModel))]
        [SwaggerResponse(404, description: "Item not found", type: typeof(ApiErrorResponse))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound(new ApiErrorResponse(404, "Ingredient with id '{id}' not found"));
            }
            return Ok(result);
        }

        /// <summary>
        /// Returns ingredients by specific category id
        /// </summary>
        /// <param name="categoryId">Category Id (GUID)</param>
        /// <param name="context">Sorting and paging parameters</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("categories/{categoryId:guid}/ingredients")]
        [SwaggerResponse(200, description: "Success", type: typeof(CollectionWrapper<IngredientModel>))]
        public async Task<IActionResult> GetByCategoryIdAsync([FromRoute, Required] Guid categoryId, [FromQuery] QueryContext context, CancellationToken cancellationToken)
        {
            var result = await _service.GetByCategoryIdAsync(categoryId, context, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Adds an ingredient
        /// </summary>
        /// <param name="model">Ingredient JSON representation</param>
        /// <param name="cancellationToken"></param>
        [Authorize]
        [HttpPost("ingredients")]
        [SwaggerResponse(201, description: "Item created successfully", type: typeof(IngredientModel))]
        [SwaggerResponse(400, description: "Invalid model state", type: typeof(ApiErrorResponse))]
        [SwaggerResponse(401, description: "Unauthorized")]
        public async Task<IActionResult> PostAsync([FromBody, Required] IngredientModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiBadRequestResponse(ModelState));
            }

            try
            {
                var result = await _service.CreateAsync(model, cancellationToken);
                return CreatedAtRoute("GetIngredient", new { Id = result.Id }, result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }

        /// <summary>
        /// Updates a specific ingredient
        /// </summary>
        /// <param name="id">Item Id (GUID)</param>
        /// <param name="model">Ingredient JSON representation</param>
        /// <param name="cancellationToken"></param>
        [Authorize]
        [HttpPut("ingredients/{id:guid}")]
        [SwaggerResponse(200, description: "Item updated successfully", type: typeof(IngredientModel))]
        [SwaggerResponse(401, description: "Unauthorized")]
        [SwaggerResponse(404, description: "Item not found", type: typeof(ApiErrorResponse))]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody, Required] IngredientModel model, CancellationToken cancellationToken)
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
    }
}
