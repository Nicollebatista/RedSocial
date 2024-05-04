using RedSocial.Core.Application.ViewModels.Post;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IPostServices : IGenericService<SavePostViewModel, PostViewModel, Post>
    {
        Task<List<PostViewModel>> GetAllViewModelWithInclude();
        public  Task<List<PostViewModel>> GetPostsCreatedByCurrentUser();
        public  Task<List<PostViewModel>> GetPostsCreatedByFriendsOfCurrentUser();


    }
}
