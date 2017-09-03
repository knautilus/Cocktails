using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Cocktails.Common.Exceptions;
using Cocktails.Common.Models;
using Cocktails.Identity.Services;
using Cocktails.ViewModels;

namespace Cocktails.Identity.Api.Controllers
{
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

        [HttpPost("register")]
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

        [HttpPost("login")]
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
