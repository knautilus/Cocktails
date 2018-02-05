using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Cocktails.Common.Exceptions;
using Cocktails.Common.Models;
using Cocktails.Data.Domain;
using Cocktails.Mapper;
using Cocktails.Security;
using Cocktails.Identity.ViewModels;

namespace Cocktails.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthSettings _authSettings;
        private readonly IModelMapper _mapper;

        public AccountService(UserManager<User> userManager, IOptions<AuthSettings> authSettings, IModelMapper mapper)
        {
            _userManager = userManager;
            _authSettings = authSettings.Value;
            _mapper = mapper;
        }

        public async Task RegisterAsync(RegisterModel registerModel, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(registerModel.Password))
            {
                throw new BadRequestException("Password cannot be empty");
            }

            var newUser = _mapper.Map<User>(registerModel);

            var userCreationResult = await _userManager.CreateAsync(newUser, registerModel.Password);
            if (!userCreationResult.Succeeded)
            {
                throw new BadRequestException(userCreationResult.Errors.Select(x => x.Description).ToArray());
            }
        }

        public async Task<LoginResultModel> LoginAsync(LoginModel loginModel, CancellationToken cancellationToken)
        {
            var identity = await GetIdentityAsync(loginModel.Username, loginModel.Password);
            if (identity == null)
            {
                return null;
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

            var result = new LoginResultModel
            {
                Token = handler.WriteToken(securityToken),
                ValidTo = securityToken.ValidTo.ToString(),
                Username = identity.Name
            };

            return result;
        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                return null;
            }

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.UserName, "TokenAuth"),
                new[] { new Claim("Id", user.Id.ToString()) });

            return identity;
        }
    }
}
