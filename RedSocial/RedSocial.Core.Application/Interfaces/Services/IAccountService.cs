using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.ViewModels.User;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SignOutAsync();
        public Task<UserViewModel> GetUserByIdAsync(string userId);
        public  Task<UserViewModel> GetUserEmailIdAsync(string email);
        public  Task<bool> UpdateProfileAsync(string userId, SaveUserViewModel profile);
        public  Task<UserViewModel> GetUserByUsernameAsync(string username);
        public  Task<SaveUserViewModel> GetUserByIdSaveAsync(string userId);
        public  Task<SaveUserViewModel> GetUserEmailIdSaveAsync(string email);



    }
}