using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Cocktails.Data.Domain;
using Cocktails.Services;
using Cocktails.ViewModels;

namespace Cocktails.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly IService<Category, CategoryModel> _service;

        public CategoriesController(IService<Category, CategoryModel> service)
        {
            _service = service;
        }

        // GET api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _service.GetAsync(cancellationToken);
            return Ok(result);
        }

        // GET api/categories/5
        [HttpGet("{id:guid}", Name = "GetCategory")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound(new IdModel(id));
            }
            return Ok(result);
        }

        // POST api/categories
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CategoryModel model, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(model, cancellationToken);
            return CreatedAtRoute("GetCategory", new IdModel(result.Id), result);
        }

        // PUT api/categories/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] CategoryModel model, CancellationToken cancellationToken)
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
