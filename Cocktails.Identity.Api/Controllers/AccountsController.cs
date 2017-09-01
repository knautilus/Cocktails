using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Cocktails.Common.Models;
using Cocktails.Data.Domain;
using Cocktails.Security;
using Cocktails.ViewModels;

namespace Cocktails.Identity.Api.Controllers
{
    [Route("v{version:apiVersion}")]
    [ApiVersion("1")]
    public class AccountsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthSettings _authSettings;
        //private readonly IMessageService _messageService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"></param>
        public AccountsController(UserManager<User> userManager, IOptions<AuthSettings> authSettings)
        {
            _userManager = userManager;
            _authSettings = authSettings.Value;
            //_messageService = messageService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel registerModel, CancellationToken cancellationToken)
        {
            var newUser = new User
            {
                UserName = registerModel.Username,
                Email = registerModel.Email
            };

            var userCreationResult = await _userManager.CreateAsync(newUser, registerModel.Password);
            if (!userCreationResult.Succeeded)
            {
                foreach (var error in userCreationResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return BadRequest(new ApiBadRequestResponse(ModelState));
            }

            //var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            //var tokenVerificationUrl = Url.Action("VerifyEmail", "Account", new { id = newUser.Id, token = emailConfirmationToken }, Request.Scheme);

            //await _messageService.Send(email, "Verify your email", $"Click <a href=\"{tokenVerificationUrl}\">here</a> to verify your email");

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel, CancellationToken cancellationToken)
        {
            var identity = await GetIdentityAsync(loginModel.Username, loginModel.Password);
            if (identity == null)
            {
                return Unauthorized();
            }

            var now = DateTime.UtcNow;

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _authSettings.Issuer,
                Audience = _authSettings.Audience,
                SigningCredentials = new SigningCredentials(SecurityHelper.GetSymmetricSecurityKey(_authSettings.SecretKey), SecurityAlgorithms.HmacSha256),
                Subject = identity,
                Expires = now.Add(TimeSpan.FromMinutes(_authSettings.Lifetime))
            });

            var response = new
            {
                access_token = handler.WriteToken(securityToken),
                username = identity.Name
            };

            return Ok(response);
        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                return null;
            }

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            //    //new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            //};

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.UserName, "TokenAuth"),
                new[] { new Claim("Id", user.Id.ToString()) });

            //ClaimsIdentity claimsIdentity =
            //new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
            //    ClaimsIdentity.DefaultRoleClaimType);
            return identity;
        }
    }
}
