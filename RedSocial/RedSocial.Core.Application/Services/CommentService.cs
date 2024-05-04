using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Helpers;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Comment;
using RedSocial.Core.Application.ViewModels.Replies;
using RedSocial.Core.Domain.Entities;
using StockApp.Core.Application.Services;


namespace RedSocial.Core.Application.Services
{
    public class CommentService : GenericService<SaveCommentViewModel, CommentViewModel, Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;
        private readonly IUserService _userService;

        public CommentService(IUserService userService,ICommentRepository commentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(commentRepository, mapper)
        {
            _userService = userService;
             _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }
        public override async Task<SaveCommentViewModel> Add(SaveCommentViewModel vm)
        {
            string UsuarioId =  vm.UsuarioId = userViewModel.Id;
            vm.UsuarioNombre = (await _userService.GetUserByIdAsync(UsuarioId)).Username;
            return await base.Add(vm);
        }

        public override async Task Update(SaveCommentViewModel vm, int id)
        {
            vm.UsuarioId = userViewModel.Id;
            await base.Update(vm, id);
        }
        public async Task<List<CommentViewModel>> GetAllCommentsForPost(int postId)
        {
            var comments = await _commentRepository.GetAllWithIncludeAsync(new List<string> { "publicacion", "Replies" });

            var commentsForPost = new List<CommentViewModel>();

            foreach (var comment in comments.Where(comment => comment.PublicacionId == postId))
            {
                var usuarioNombre = (await _userService.GetUserByIdAsync(comment.Usuarioid)).Username;
                var imagen = (await _userService.GetUserByIdAsync(comment.Usuarioid)).ImageUrl;

                var replies = new List<RepliesViewModel>();
                if (comment.Replies != null)
                {
                    foreach (var reply in comment.Replies)
                    {
                        var usuarioNombreReply = (await _userService.GetUserByIdAsync(reply.Usuarioid)).Username;
                        var ImagenReply = (await _userService.GetUserByIdAsync(reply.Usuarioid)).ImageUrl;
                        replies.Add(new RepliesViewModel
                        {
                            Id = reply.Id,
                            Contenido = reply.Contenido,
                            ComentarioId = reply.ComentarioId,
                            Usuarioid = reply.Usuarioid,
                            UsuarioNombre = usuarioNombreReply,
                            ImagenUrl = ImagenReply
                        });
                    }
                }
                commentsForPost.Add(new CommentViewModel
                {
                    Id = comment.Id,
                    Contenido = comment.Contenido,
                    PublicacionId = comment.PublicacionId,
                    UsuarioId = comment.Usuarioid,
                    UsuarioNombre = usuarioNombre,
                    ImagenUrl = imagen,
                    Replies = replies
                });
            }
            return commentsForPost;
        }


    }
}