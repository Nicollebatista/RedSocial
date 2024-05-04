using RedSocial.Core.Application.ViewModels.Comment;
using RedSocial.Core.Application.ViewModels.Friend;
using RedSocial.Core.Domain.Entities;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface ICommentService : IGenericService<SaveCommentViewModel, CommentViewModel, Comment>
    {
        public Task<List<CommentViewModel>> GetAllCommentsForPost(int postId);
    }
}
