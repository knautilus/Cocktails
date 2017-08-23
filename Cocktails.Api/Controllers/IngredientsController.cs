using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

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
    /// API Controller for Ingredients
    /// </summary>
    [Route("api/[controller]")]
    public class IngredientsController : Controller
    {
        private readonly IService<Ingredient, IngredientModel> _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public IngredientsController(IService<Ingredient, IngredientModel> service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns a collection of ingredients
        /// </summary>
        /// <param name="cancellationToken"></param>
        [HttpGet]
        [SwaggerResponse(200, description: "Success", type: typeof(IngredientModel[]))]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _service.GetAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Returns a specific ingredient
        /// </summary>
        /// <param name="id">Item Id (GUID)</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{id:guid}", Name = "GetIngredient")]
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
        /// Adds an ingredient
        /// </summary>
        /// <param name="model">Ingredient JSON representation</param>
        /// <param name="cancellationToken"></param>
        [HttpPost]
        [SwaggerResponse(201, description: "Item created successfully", type: typeof(IngredientModel))]
        [SwaggerResponse(400, description: "Invalid model state", type: typeof(ApiErrorResponse))]
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
        [HttpPut("{id:guid}")]
        [SwaggerResponse(200, description: "Item updated successfully", type: typeof(IngredientModel))]
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
