using AutoMapper;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.DTOs.Email;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Domain.Entities;
using RedSocial.Core.Application.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RedSocial.Core.Application.Helpers;

namespace RedSocial.Core.Application.Services
{ 
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IAccountService accountService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<UserViewModel> GetUserByUsernameAsync(string username)
        {
            return await _accountService.GetUserByUsernameAsync(username);
        }
        public async Task<UserViewModel> GetUserByUsernameWithOutUserAsync(string username)
        {
            var user = await _accountService.GetUserByUsernameAsync(username);

            if (user != null && user.Id == userViewModel.Id)
            {
                return null; 
            }

            return user; 
        }


        public async Task<UserViewModel> GetUserByIdAsync(string userId)
        {
            return await _accountService.GetUserByIdAsync(userId);
        }
        
        public async Task<SaveUserViewModel> GetUserByIdSaveAsync(string userId)
        {
            return await _accountService.GetUserByIdSaveAsync(userId);
        }
        public async Task<bool> UpdateProfileAsync(string email, SaveUserViewModel profile)
        {
            return await _accountService.UpdateProfileAsync(email, profile);
        }



        public async Task<UserViewModel> GetUserByEmailAsync(string email)
        {
            return await _accountService.GetUserEmailIdAsync(email);
        }
        public async Task<SaveUserViewModel> GetUserByEmailSaveAsync(string email)
        {
            return await _accountService.GetUserEmailIdSaveAsync(email);
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }
        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterBasicUserAsync(registerRequest, origin);
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(resetRequest);
        }

        
    }
}
