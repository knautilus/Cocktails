using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

using Cocktails.Common.Exceptions;
using Cocktails.Common.Objects;
using Cocktails.Identity.Services;
using Cocktails.Identity.ViewModels;

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
                // TODO check model is not null
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

        /// <summary>
        /// Confirms user email using confirmation code
        /// </summary>
        /// <param name="confirmationModel"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("email/confirm")]
        [SwaggerResponse(200, description: "Email confirmed successfully")]
        [SwaggerResponse(401, description: "Invalid confirmation code")]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody] EmailConfirmationModel confirmationModel, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ConfirmEmailAsync(confirmationModel, cancellationToken);
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
        /// Changes user password
        /// </summary>
        /// <param name="changePasswordModel"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("password/change")]
        [SwaggerResponse(200, description: "Password changed successfully")]
        [SwaggerResponse(401, description: "Invalid password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordModel changePasswordModel, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ChangePasswordAsync(changePasswordModel, cancellationToken);
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
        /// Requests token for resetting password
        /// </summary>
        /// <param name="forgotPasswordModel"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("password/forgot")]
        [SwaggerResponse(200, description: "Password reset requested successfully")]
        [SwaggerResponse(401, description: "Invalid email")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordModel forgotPasswordModel, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ForgotPasswordAsync(forgotPasswordModel, cancellationToken);
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
        /// Resets user password
        /// </summary>
        /// <param name="resetPasswordModel"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("password/reset")]
        [SwaggerResponse(200, description: "Password reset successfully")]
        [SwaggerResponse(401, description: "Invalid ")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordModel resetPasswordModel, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ResetPasswordAsync(resetPasswordModel, cancellationToken);
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
    }
}
