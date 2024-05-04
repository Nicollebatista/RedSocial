using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Helpers;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Comment;
using RedSocial.Core.Application.ViewModels.Friend;
using RedSocial.Core.Application.ViewModels.Post;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Domain.Entities;
using StockApp.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Services
{
    public class FriendService : GenericService<SaveFriendViewModel, FriendViewModel, Friend>, IFriendService
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;
        private readonly IUserService _userService;

        public FriendService(IUserService userService,IFriendRepository friendRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(friendRepository, mapper)
        {
            _userService = userService;
            _friendRepository = friendRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<List<UserViewModel>> GetFriendsForUser()
        {
            string userId = userViewModel.Id;
            var friends = await _friendRepository.GetAllAsync();

            List<UserViewModel> usuarios = new List<UserViewModel>();
            foreach (var friend in friends)
            {
                string friendId = friend.Usuarioid1 != userId ? friend.Usuarioid1 : friend.Usuarioid2;
                try
                {
                    var usuario = await _userService.GetUserByIdAsync(friendId);
                    usuarios.Add(usuario);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener detalles del usuario {friendId}: {ex.Message}");
                }
            }

            return usuarios;
        }
        public async Task<FriendViewModel> GetAmistadad(string OneUser)
        {
            string userId = userViewModel.Id;
            var friends = await _friendRepository.GetAllAsync();

            FriendViewModel amistad = null;
            foreach (var friend in friends)
            {
                if ((friend.Usuarioid1 == userId && friend.Usuarioid2 == OneUser) ||
                    (friend.Usuarioid1 == OneUser && friend.Usuarioid2 == userId))
                {
                    amistad = _mapper.Map<FriendViewModel>(friend);
                    break;
                }
            }

            return amistad;
        }



        public override async Task<SaveFriendViewModel> Add(SaveFriendViewModel vm)
        {
            vm.Usuarioid1 = userViewModel.Id;
            return await base.Add(vm);
        }



        public async Task<bool> ExisteRelacionDeAmistad( string userId2)
        {
            var friends = await _friendRepository.GetAllAsync();
            string userId1 = userViewModel.Id;
            var existeRelacion = friends.Any(f =>
                (f.Usuarioid1 == userId1 && f.Usuarioid2 == userId2) ||
                (f.Usuarioid1 == userId2 && f.Usuarioid2 == userId1));

            return existeRelacion;
        }

        public async Task<Friend> GetUserFriend(string friendId)
        {
            string userId = userViewModel.Id;

            var friends = await _friendRepository.GetAllAsync();

            var userFriendship = friends.FirstOrDefault(f =>
                (f.Usuarioid1 == userId && f.Usuarioid2 == friendId) ||
                (f.Usuarioid1 == friendId && f.Usuarioid2 == userId));

            return userFriendship;
        }


    }
}
