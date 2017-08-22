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
    /// API Controller for Categories
    /// </summary>
    [Route("api/[controller]")]
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
        /// <param name="cancellationToken"></param>
        [HttpGet]
        [SwaggerResponse(200, description: "Success", type: typeof(CategoryModel[]))]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _service.GetAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Returns a specific ingredient category
        /// </summary>
        /// <param name="id">Item Id (GUID)</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{id:guid}", Name = "GetCategory")]
        [SwaggerResponse(200, description: "Item found", type: typeof(CategoryModel))]
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
        /// Adds an ingredient category
        /// </summary>
        /// <param name="model">Category JSON representation</param>
        /// <param name="cancellationToken"></param>
        [HttpPost]
        [SwaggerResponse(201, description: "Item created successfully", type: typeof(CategoryModel))]
        public async Task<IActionResult> PostAsync([FromBody, Required] CategoryModel model, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(model, cancellationToken);
            return CreatedAtRoute("GetCategory", new IdModel(result.Id), result);
        }

        /// <summary>
        /// Updates a specific ingredient category
        /// </summary>
        /// <param name="id">Item Id (GUID)</param>
        /// <param name="model">Category JSON representation</param>
        /// <param name="cancellationToken"></param>
        [HttpPut("{id:guid}")]
        [SwaggerResponse(200, description: "Item updated successfully", type: typeof(CategoryModel))]
        [SwaggerResponse(404, description: "Item not found", type: typeof(IdModel))]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody, Required] CategoryModel model, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(id, model, cancellationToken);
            if (result == null)
            {
                return NotFound(new IdModel(id));
            }
            return Ok(result);
        }
    }
}
