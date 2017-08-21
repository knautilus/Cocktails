﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Cocktails.Data.Domain;
using Cocktails.Services;
using Cocktails.ViewModels;

namespace Cocktails.Api.Controllers
{
    [Route("api/[controller]")]
    public class CocktailsController : Controller
    {
        private readonly IService<Cocktail, CocktailModel> _service;

        public CocktailsController(IService<Cocktail, CocktailModel> service)
        {
            _service = service;
        }

        // GET api/cocktails
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _service.GetAsync(cancellationToken);
            return Ok(result);
        }

        // GET api/cocktails/5
        [HttpGet("{id:guid}", Name = "GetCocktail")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound(new IdModel(id));
            }
            return Ok(result);
        }

        // POST api/cocktails
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CocktailModel model, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(model, cancellationToken);
            return CreatedAtRoute("GetCocktail", new IdModel(result.Id), result);
        }

        // PUT api/cocktails/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] CocktailModel model, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(id, model, cancellationToken);
            if (result == null)
            {
                return NotFound(new IdModel(id));
            }
            return Ok(result);
        }

        // DELETE api/cocktails/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
