using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.ViewModels.User;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();
        public  Task<UserViewModel> GetUserByIdAsync(string userId);
        public  Task<UserViewModel> GetUserByEmailAsync(string email);
        public  Task<UserViewModel> GetUserByUsernameAsync(string username);
        public  Task<UserViewModel> GetUserByUsernameWithOutUserAsync(string username);
        public Task<SaveUserViewModel> GetUserByIdSaveAsync(string userId);
        public  Task<SaveUserViewModel> GetUserByEmailSaveAsync(string email);
        public Task<bool> UpdateProfileAsync(string email, SaveUserViewModel profile);


    }
}