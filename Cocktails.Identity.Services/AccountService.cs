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
            User newUser;
            IdentityResult userCreationResult = IdentityResult.Success;

            if (registerModel.IsSocial)
            {
                var externalUser = await GetExternalUserAsync(registerModel.LoginProvider, registerModel.AccessToken, cancellationToken);

                newUser = _mapper.Map<User>(externalUser);

                var existingUser = newUser.Email != null ? await _userManager.FindByEmailAsync(newUser.Email) : null;
                if (existingUser == null)
                {
                    userCreationResult = await _userManager.CreateAsync(newUser);
                }
                else
                {
                    newUser = existingUser;
                }
                await _userManager.AddLoginAsync(newUser, new UserLoginInfo("Facebook", externalUser.Id, "Facebook"));
            }
            else
            {
                if (string.IsNullOrWhiteSpace(registerModel.Password))
                {
                    throw new BadRequestException("Password cannot be empty");
                }

                newUser = _mapper.Map<User>(registerModel);
                userCreationResult = await _userManager.CreateAsync(newUser, registerModel.Password);
            }

            if (!userCreationResult.Succeeded)
            {
                throw new BadRequestException(userCreationResult.Errors.Select(x => x.Description).ToArray());
            }
        }

        public async Task<LoginResultModel> LoginAsync(LoginModel loginModel, CancellationToken cancellationToken)
        {
            User user;
            if (loginModel.IsSocial)
            {
                var externalUser = await GetExternalUserAsync(loginModel.LoginProvider, loginModel.AccessToken, cancellationToken);

                user = await _userManager.FindByLoginAsync(loginModel.LoginProvider.ToString(), externalUser.Id);
            }
            else
            {
                user = await _userManager.FindByNameAsync(loginModel.Username);
                if (!await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    return null;
                }
            }

            var identity = GetIdentity(user);
            if (identity == null)
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _authSettings.Issuer,
                Audience = _authSettings.Audience,
                SigningCredentials = new SigningCredentials(SecurityHelper.GetSymmetricSecurityKey(_authSettings.SecretKey), SecurityAlgorithms.HmacSha256),
                Subject = identity,
                Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(_authSettings.Lifetime))
            });

            var result = new LoginResultModel
            {
                Token = handler.WriteToken(securityToken),
                ValidTo = securityToken.ValidTo.ToString(),
                Username = identity.Name
            };

            return result;
        }

        private async Task<SocialUserBase> GetExternalUserAsync(LoginProviderType loginProvider, string accessToken, CancellationToken cancellationToken)
        {
            SocialUserBase externalUser;
            if (loginProvider == LoginProviderType.Facebook)
            {
                var service = new FacebookService();
                externalUser = await service.GetProfileAsync(accessToken, cancellationToken);
            }
            else //if (registerModel.LoginProvider == LoginProviderType.GooglePlus)
            {
                var service = new GooglePlusService();
                externalUser = await service.GetProfileAsync(accessToken, cancellationToken);
            }
            return externalUser;
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            if (user == null)
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
