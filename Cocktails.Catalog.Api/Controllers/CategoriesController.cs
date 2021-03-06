﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

using Cocktails.Catalog.Services;
using Cocktails.Catalog.ViewModels;
using Cocktails.Common.Exceptions;
using Cocktails.Common.Models;
using Cocktails.Data.Domain;

namespace Cocktails.Catalog.Api.Controllers
{
    /// <summary>
    /// API Controller for Categories
    /// </summary>
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class CategoriesController : Controller
    {
        private readonly IService<Category, CategoryModel> _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public CategoriesController(IService<Category, CategoryModel> service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns a collection of ingredient categories
        /// </summary>
        /// <param name="context">Sorting and paging parameters</param>
        /// <param name="cancellationToken"></param>
        [HttpGet]
        [SwaggerResponse(200, description: "Success", type: typeof(CollectionWrapper<CategoryModel>))]
        public async Task<IActionResult> GetAllAsync([FromQuery] QueryContext context, CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(context, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Returns a specific ingredient category
        /// </summary>
        /// <param name="id">Item Id (GUID)</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{id:guid}", Name = "GetCategory")]
        [SwaggerResponse(200, description: "Item found", type: typeof(CategoryModel))]
        [SwaggerResponse(404, description: "Item not found", type: typeof(ApiErrorResponse))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound(new ApiErrorResponse(404, $"Category with id '{id}' not found"));
            }
            return Ok(result);
        }

        /// <summary>
        /// Adds an ingredient category
        /// </summary>
        /// <param name="model">Category JSON representation</param>
        /// <param name="cancellationToken"></param>
        [Authorize]
        [HttpPost]
        [SwaggerResponse(201, description: "Item created successfully", type: typeof(CategoryModel))]
        [SwaggerResponse(400, description: "Invalid model state", type: typeof(ApiErrorResponse))]
        [SwaggerResponse(401, description: "Unauthorized")]
        public async Task<IActionResult> PostAsync([FromBody, Required] CategoryModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiBadRequestResponse(ModelState));
            }

            try
            {
                var result = await _service.CreateAsync(model, cancellationToken);
                return CreatedAtRoute("GetCategory", new { Id = result.Id }, result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }

        /// <summary>
        /// Updates a specific ingredient category
        /// </summary>
        /// <param name="id">Item Id (GUID)</param>
        /// <param name="model">Category JSON representation</param>
        /// <param name="cancellationToken"></param>
        [Authorize]
        [HttpPut("{id:guid}")]
        [SwaggerResponse(200, description: "Item updated successfully", type: typeof(CategoryModel))]
        [SwaggerResponse(400, description: "Invalid model state", type: typeof(ApiErrorResponse))]
        [SwaggerResponse(401, description: "Unauthorized")]
        [SwaggerResponse(404, description: "Item not found", type: typeof(ApiErrorResponse))]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody, Required] CategoryModel model, CancellationToken cancellationToken)
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
