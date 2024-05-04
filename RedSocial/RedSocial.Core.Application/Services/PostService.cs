using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Helpers;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Comment;
using RedSocial.Core.Application.ViewModels.Friend;
using RedSocial.Core.Application.ViewModels.Post;
using RedSocial.Core.Application.ViewModels.Replies;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Domain.Entities;
using StockApp.Core.Application.Services;

namespace RedSocial.Core.Application.Services
{
    public class PostService : GenericService<SavePostViewModel, PostViewModel, Post>, IPostServices
    {
        private readonly IPostReposityc _postReposity;
        private readonly IFriendService _friendService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userViewModel;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;

        public PostService(ICommentService commentService,IUserService userService, IPostReposityc postRepository, IFriendService friendService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(postRepository, mapper)
        {
            _userService = userService;

            _postReposity = postRepository;
            _friendService = friendService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _commentService = commentService;
        }



        public async Task<List<PostViewModel>> GetPostsCreatedByCurrentUser()
        {
            var postList = await _postReposity.GetAllWithIncludeAsync(new List<string> { "Comments", "Comments.Replies" });
            var filteredPosts = postList.Where(post => post.Usuarioid == userViewModel.Id);

            var postViewModels = new List<PostViewModel>();

            foreach (var post in filteredPosts)
            {
                var usuarioNombre = userViewModel.UserName;

                var postViewModel = new PostViewModel
                {
                    Id = post.Id,
                    Descripcion = post.Descripcion,
                    Publicacion = post.Publicacion,
                    Usuarioid = post.Usuarioid,
                    UsuarioNombre = usuarioNombre,
                    ImagenUrl = userViewModel.ImagenUrl,
                    Comentarios = await _commentService.GetAllCommentsForPost(post.Id) 
                };

                postViewModels.Add(postViewModel);
            }

            return postViewModels;
        }

        public async Task<List<PostViewModel>> GetPostsCreatedByFriendsOfCurrentUser()
        {
            var friendList = await _friendService.GetFriendsForUser();
            var currentUser = userViewModel.Id;

            var postList = await GetAllViewModelWithInclude();

            var postViewModels = new List<PostViewModel>();

            foreach (var post in postList.Where(post => friendList.Any(friend => friend.Id == post.Usuarioid) && post.Usuarioid != currentUser))
            {
                var comentariosTask = _commentService.GetAllCommentsForPost(post.Id);
                var usuarioTask = _userService.GetUserByIdAsync(post.Usuarioid);

                await Task.WhenAll(comentariosTask, usuarioTask);

                var comentarios = await comentariosTask;
                var usuario = await usuarioTask;

                postViewModels.Add(new PostViewModel
                {
                    Id = post.Id,
                    Descripcion = post.Descripcion,
                    Publicacion = post.Publicacion,
                    Usuarioid = post.Usuarioid,
                    ImagenUrl = post.ImagenUrl,
                    UsuarioNombre = usuario.Username, 
                    Comentarios = comentarios
                });
            }

            return postViewModels;
        }





        public override async Task<SavePostViewModel> Add(SavePostViewModel vm)
        {
            vm.Usuarioid = userViewModel.Id;
            return await base.Add(vm);
        }
        public override async Task Update(SavePostViewModel vm, int id)
        {
            vm.Usuarioid = userViewModel.Id;
            await base.Update(vm, id);
        }



        public async Task<List<PostViewModel>> GetAllViewModelWithInclude()
        {
            var postList = await _postReposity.GetAllWithIncludeAsync(new List<string> { "Comments", "Comments.Replies" });

            var postViewModels = new List<PostViewModel>();
            //var usuarioNombre = (await _userService.GetUserByIdAsync(comment.Usuarioid)).Username;

            foreach (var post in postList)
            {
                var usuarioNombre = (await _userService.GetUserByIdAsync(post.Usuarioid)).Username;
                var Imagen = (await _userService.GetUserByIdAsync(post.Usuarioid)).ImageUrl;

                var postViewModel = new PostViewModel
                {
                    Id = post.Id,
                    Descripcion = post.Descripcion,
                    Publicacion = post.Publicacion,
                    Usuarioid = post.Usuarioid,
                    ImagenUrl = Imagen,
                    UsuarioNombre = usuarioNombre, 

                    Comentarios = post.Comments?.Select(comment => new CommentViewModel
                    {
                        Id = comment.Id,
                        Contenido = comment.Contenido,
                        PublicacionId = comment.PublicacionId,
                        Replies = comment.Replies?.Select(reply => new RepliesViewModel
                        {
                            Id = reply.Id,
                            Contenido = reply.Contenido,
                            ComentarioId = reply.ComentarioId
                        }).ToList()
                    }).ToList()
                };

                postViewModels.Add(postViewModel);
            }

            return postViewModels;
        }








    }
}
