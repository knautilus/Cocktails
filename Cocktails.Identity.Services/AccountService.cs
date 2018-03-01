using System;
using System.Globalization;
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
using Cocktails.Common.Objects;
using Cocktails.Data.Identity;
using Cocktails.Mapper;
using Cocktails.Security;
using Cocktails.Identity.ViewModels;
using Cocktails.Mailing;
using Cocktails.Mailing.Mailgun;
using Cocktails.Mailing.Models;

namespace Cocktails.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly AuthSettings _authSettings;
        private readonly MailingSettings _mailingSettings;
        private readonly UserManager<User> _userManager;
        private readonly IMailSender _mailSender;
        private readonly IModelMapper _mapper;

        public AccountService(IOptions<AuthSettings> authSettings, IOptions<MailingSettings> mailingSettings, UserManager<User> userManager, IMailSender mailSender, IModelMapper mapper)
        {
            _authSettings = authSettings.Value;
            _mailingSettings = mailingSettings.Value;
            _userManager = userManager;
            _mailSender = mailSender;
            _mapper = mapper;
        }

        public async Task RegisterAsync(RegisterModel registerModel, CancellationToken cancellationToken)
        {
            User newUser;
            var userCreationResult = IdentityResult.Success;
            var newUserCreated = false;

            if (registerModel.IsSocial)
            {
                var externalUser = await GetExternalUserAsync(registerModel.LoginProvider, registerModel.AccessToken,
                    cancellationToken);

                newUser = _mapper.Map<User>(externalUser);

                var existingUser = newUser.Email != null ? await _userManager.FindByEmailAsync(newUser.Email) : null;
                if (existingUser == null)
                {
                    userCreationResult = await _userManager.CreateAsync(newUser);
                    newUserCreated = true;
                }
                else
                {
                    newUser = existingUser;
                }
                await _userManager.AddLoginAsync(newUser, new UserLoginInfo(registerModel.LoginProvider.ToString(), externalUser.Id,
                        registerModel.LoginProvider.ToString()));
            }
            else
            {
                if (string.IsNullOrWhiteSpace(registerModel.Password))
                {
                    throw new BadRequestException("Password cannot be empty");
                }

                newUser = _mapper.Map<User>(registerModel);
                userCreationResult = await _userManager.CreateAsync(newUser, registerModel.Password);
                newUserCreated = true;
            }

            if (!userCreationResult.Succeeded)
            {
                throw new BadRequestException(userCreationResult.Errors.Select(x => x.Description).ToArray());
            }

            if (newUserCreated && !string.IsNullOrEmpty(newUser.Email))
            {
                var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                _mailSender.Send(new MailMessage("Eda.ru email confirmation",
                    $"<html lang=\"en\"><body><div>Здравствуйте. Ваш код для подтверждения почтового ящика: userId={newUser.Id}&confirmationCode={confirmationCode}</div></body></html>",
                    new MailAddress(_mailingSettings.FromAddress, _mailingSettings.FromName), newUser.Email));
            }
        }

        public async Task<LoginResultModel> LoginAsync(LoginModel loginModel, CancellationToken cancellationToken)
        {
            User user;
            if (loginModel.IsSocial)
            {
                var externalUser = await GetExternalUserAsync(loginModel.LoginProvider, loginModel.AccessToken, cancellationToken); // TODO externalUser check null?

                user = await _userManager.FindByLoginAsync(loginModel.LoginProvider.ToString(), externalUser.Id);
            }
            else
            {
                user = await _userManager.FindByNameAsync(loginModel.Login)
                    ?? await _userManager.FindByEmailAsync(loginModel.Login);
                if (!await _userManager.CheckPasswordAsync(user, loginModel.Password)) // TODO user check null?
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
                ValidTo = securityToken.ValidTo.ToString(CultureInfo.InvariantCulture),
                Username = identity.Name
            };

            return result;
        }

        public async Task<SocialLoginModel[]> GetSocialLoginsAsync(long userId, CancellationToken cancellationToken)
        {
            var user = new User { Id = userId };
            var existingLogins = await _userManager.GetLoginsAsync(user);
            return _mapper.Map<SocialLoginModel[]>(existingLogins);
        }

        public async Task AddSocialLoginAsync(long userId, SocialLoginModel loginModel, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new BadRequestException("UserId not found");
            }

            var externalUser = await GetExternalUserAsync(loginModel.LoginProvider, loginModel.AccessToken, cancellationToken); // TODO externalUser check null?

            var userWithSameLogin = await _userManager.FindByLoginAsync(loginModel.LoginProvider.ToString(), externalUser.Id);
            if (userWithSameLogin != null)
            {
                if (userWithSameLogin.Id != userId)
                {
                    throw new BadRequestException("The login is already in use");
                }
                throw new BadRequestException("The login is already connected");
            }

            await _userManager.AddLoginAsync(user, new UserLoginInfo(loginModel.LoginProvider.ToString(), externalUser.Id, loginModel.LoginProvider.ToString()));
        }

        public async Task RemoveSocialLoginAsync(long userId, LoginRemoveModel loginModel, CancellationToken cancellationToken)
        {
            var user = new User { Id = userId };

            var existingLogins = await _userManager.GetLoginsAsync(user);
            if (existingLogins == null)
            {
                throw new BadRequestException("Login is not found");
            }
            var existingLogin = existingLogins.FirstOrDefault(x => x.LoginProvider == loginModel.LoginProvider.ToString());
            if (existingLogin == null)
            {
                throw new BadRequestException("Login is not found");
            }

            await _userManager.RemoveLoginAsync(user, existingLogin.LoginProvider, existingLogin.ProviderKey);
        }

        public async Task ConfirmEmailAsync(EmailConfirmationModel confirmationModel, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(confirmationModel.UserId);
            if (user == null)
            {
                throw new BadRequestException("User not found");
            }
            var result = await _userManager.ConfirmEmailAsync(user, confirmationModel.ConfirmationCode);
        }

        public async Task ForgotPasswordAsync(ForgotPasswordModel forgotPasswordModel, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
            {
                throw new BadRequestException("Invalid email");
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            _mailSender.Send(new MailMessage("Eda.ru password reset", $"<html lang=\"en\"><body><div>Здравствуйте. Ваш код для сброса пароля: userId={user.Id}&resetcode={code}</div></body></html>", new MailAddress(_mailingSettings.FromAddress, _mailingSettings.FromName), forgotPasswordModel.Email));
        }

        public async Task ResetPasswordAsync(ResetPasswordModel resetPasswordModel, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(resetPasswordModel.UserId);
            if (user == null)
            {
                throw new BadRequestException("Invalid user Id");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Code, resetPasswordModel.NewPassword);
        }

        public async Task ChangePasswordAsync(long userId, ChangePasswordModel changePasswordModel, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new BadRequestException("Invalid user Id");
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
        }

        public async Task ChangeEmailAsync(long userId, ChangeEmailModel changeEmailModel, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new BadRequestException("Invalid user Id");
            }
            var existedUser = await _userManager.FindByEmailAsync(changeEmailModel.Email);
            if (existedUser != null)
            {
                throw new BadRequestException("Email is busy");
            }

            var result = await _userManager.SetEmailAsync(user, changeEmailModel.Email);
            if (result.Succeeded)
            {
                var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                _mailSender.Send(new MailMessage("Eda.ru email confirmation",
                    $"<html lang=\"en\"><body><div>Здравствуйте. Ваш код для подтверждения почтового ящика: userId={userId}&confirmationCode={confirmationCode}</div></body></html>",
                    new MailAddress(_mailingSettings.FromAddress, _mailingSettings.FromName), changeEmailModel.Email));
            }
        }

        private async Task<SocialUserBase> GetExternalUserAsync(LoginProviderType loginProvider, string accessToken, CancellationToken cancellationToken)
        {
            SocialUserBase externalUser;
            if (loginProvider == LoginProviderType.Facebook)
            {
                var service = new FacebookService();
                externalUser = await service.GetProfileAsync(accessToken, cancellationToken);
            }
            else if (loginProvider == LoginProviderType.GooglePlus)
            {
                var service = new GooglePlusService();
                externalUser = await service.GetProfileAsync(accessToken, cancellationToken);
            }
            else //if (loginProvider == LoginProviderType.Vk)
            {
                var service = new VkService();
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
            var identity = new ClaimsIdentity(
                new GenericIdentity(user.UserName, "TokenAuth"),
                new[] { new Claim("Id", user.Id.ToString()) });

            return identity;
        }
    }
}
