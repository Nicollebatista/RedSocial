using RedSocial.Core.Application.ViewModels.Friend;
using RedSocial.Core.Application.ViewModels.Post;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IFriendService : IGenericService<SaveFriendViewModel, FriendViewModel, Friend>
    {
        public  Task<bool> ExisteRelacionDeAmistad( string userId2);
        public  Task<List<UserViewModel>> GetFriendsForUser();
        public  Task<FriendViewModel> GetAmistadad(string OneUser);

    }
}
