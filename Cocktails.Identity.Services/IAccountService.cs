using System.Threading;
using System.Threading.Tasks;
using Cocktails.Identity.ViewModels;

namespace Cocktails.Identity.Services
{
    public interface IAccountService
    {
        Task<LoginResultModel> LoginAsync(LoginModel loginModel, CancellationToken cancellationToken);
        Task RegisterAsync(RegisterModel registerModel, CancellationToken cancellationToken);
        Task ConfirmEmailAsync(EmailConfirmationModel confirmationModel, CancellationToken cancellationToken);
        Task ForgotPasswordAsync(ForgotPasswordModel forgotPasswordModel, CancellationToken cancellationToken);
        Task ResetPasswordAsync(ResetPasswordModel resetPasswordModel, CancellationToken cancellationToken);
        Task ChangePasswordAsync(ChangePasswordModel changePasswordModel, CancellationToken cancellationToken);
    }
}
