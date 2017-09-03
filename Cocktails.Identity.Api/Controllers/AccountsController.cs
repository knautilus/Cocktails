using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

using Cocktails.Common.Exceptions;
using Cocktails.Common.Models;
using Cocktails.Identity.Services;
using Cocktails.ViewModels;

namespace Cocktails.Identity.Api.Controllers
{
    /// <summary>
    /// API Controller for Accounts
    /// </summary>
    [Route("v{version:apiVersion}")]
    [ApiVersion("1")]
    public class AccountsController : Controller
    {
        private readonly IAccountService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountService"></param>
        public AccountsController(IAccountService accountService)
        {
            _service = accountService;
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="registerModel"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("register")]
        [SwaggerResponse(200, description: "User registered successfully")]
        [SwaggerResponse(400, description: "Invalid model state", type: typeof(ApiErrorResponse))]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel registerModel, CancellationToken cancellationToken)
        {
            try
            {
                await _service.RegisterAsync(registerModel, cancellationToken);
                return Ok();
            }
            catch (BadRequestException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return BadRequest(new ApiBadRequestResponse(ModelState));
            }
        }

        /// <summary>
        /// Authenticates a user and returns access token
        /// </summary>
        /// <param name="loginModel"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("login")]
        [SwaggerResponse(200, description: "User authenticated successfully", type: typeof(LoginResultModel))]
        [SwaggerResponse(401, description: "Invalid login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel, CancellationToken cancellationToken)
        {
            var result = await _service.LoginAsync(loginModel, cancellationToken);
            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
