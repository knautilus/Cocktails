using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Api.Common;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

using Cocktails.Common.Exceptions;
using Cocktails.Common.Objects;
using Cocktails.Identity.Services;
using Cocktails.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> RegisterAsync([FromBody][Required] RegisterModel registerModel, CancellationToken cancellationToken)
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

        /// <summary>
        /// Returns current user account data
        /// </summary>
        /// <param name="cancellationToken"></param>
        [Authorize]
        [HttpGet("account", Name = "GetAccount")]
        [SwaggerResponse(200, description: "Item found", type: typeof(UserModel))]
        [SwaggerResponse(401, description: "Unauthorized")]
        public async Task<IActionResult> GetByIdAsync(CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var result = await _service.GetByIdAsync(userId, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Links social login to the current account
        /// </summary>
        /// <param name="loginModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("sociallogin")]
        public async Task<IActionResult> AddSocialLoginAsync(SocialLoginModel loginModel, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            try
            {
                await _service.AddSocialLoginAsync(userId, loginModel, cancellationToken);
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
        /// Unlinks social login from the current account
        /// </summary>
        /// <param name="loginRemoveModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("sociallogin")]
        public async Task<IActionResult> RemoveSocialLoginAsync([FromBody] LoginRemoveModel loginRemoveModel, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            try
            {
                await _service.RemoveSocialLoginAsync(userId, loginRemoveModel, cancellationToken);
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
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("sociallogin")]
        public async Task<IActionResult> GetSocialLoginsAsync(CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            try
            {
                var result = await _service.GetSocialLoginsAsync(userId, cancellationToken);
                return Ok(result);
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
        /// Changes user email
        /// </summary>
        /// <param name="changeEmailModel"></param>
        /// <param name="cancellationToken"></param>
        [Authorize]
        [HttpPut("email")]
        [SwaggerResponse(200, description: "Email changed successfully")]
        [SwaggerResponse(400, description: "Email is busy")]
        [SwaggerResponse(401, description: "Invalid user id")] // TODO review all return codes and descriptions
        public async Task<IActionResult> ChangeEmailAsync([FromBody] ChangeEmailModel changeEmailModel, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            try
            {
                await _service.ChangeEmailAsync(userId, changeEmailModel, cancellationToken);
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
        [Authorize]
        [HttpPut("password")]
        [SwaggerResponse(200, description: "Password changed successfully")]
        [SwaggerResponse(401, description: "Invalid password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordModel changePasswordModel, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            try
            {
                await _service.ChangePasswordAsync(userId, changePasswordModel, cancellationToken);
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
