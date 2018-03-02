using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Api.Common;
using Cocktails.Common.Exceptions;
using Cocktails.Common.Objects;
using Cocktails.Common.Services;
using Cocktails.Data.Identity;
using Cocktails.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cocktails.Identity.Api.Controllers
{
    /// <summary>
    /// API Controller for Profiles
    /// </summary>
    [Route("v{version:apiVersion}")]
    [ApiVersion("1")]
    public class ProfilesController : Controller
    {
        private readonly IService<long, UserProfile, UserProfileModel> _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public ProfilesController(IService<long, UserProfile, UserProfileModel> service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns a specific profile
        /// </summary>
        /// <param name="id">Item Id (long)</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{id:guid}", Name = "GetProfile")]
        [SwaggerResponse(200, description: "Item found", type: typeof(UserProfileModel))]
        [SwaggerResponse(404, description: "Item not found", type: typeof(ApiErrorResponse))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] long id, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound(new ApiErrorResponse(404, $"UserProfile with id '{id}' not found"));
            }
            return Ok(result);
        }

        /// <summary>
        /// Updates a specific profile
        /// </summary>
        /// <param name="model">UserProfile JSON representation</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [SwaggerResponse(200, description: "Item updated successfully", type: typeof(UserProfileModel))]
        [SwaggerResponse(400, description: "Invalid model state", type: typeof(ApiErrorResponse))]
        [SwaggerResponse(401, description: "Unauthorized")]
        public async Task<IActionResult> PutAsync([FromBody, Required] UserProfileModel model, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            try
            {
                var result = await _service.UpdateAsync(userId, model, cancellationToken);
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
