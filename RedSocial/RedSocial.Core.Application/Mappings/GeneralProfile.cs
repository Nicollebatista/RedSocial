using AutoMapper;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.ViewModels.Comment;
using RedSocial.Core.Application.ViewModels.Friend;
using RedSocial.Core.Application.ViewModels.Post;
using RedSocial.Core.Application.ViewModels.Replies;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {


            //Comentarios

            CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.Post, opt => opt.Ignore())
                .ForMember(x => x.UsuarioNombre, opt => opt.Ignore());


           


            //POST

            CreateMap<Post, SavePostViewModel>()
            .ForMember(x => x.File, opt => opt.Ignore());

            CreateMap<Post, PostViewModel>();

   


            CreateMap<SaveCommentViewModel, Comment>()
           .ForMember(dest => dest.Usuarioid, opt => opt.MapFrom(src => src.UsuarioId)) // Mapear UsuarioId a Usuarioid
           .ForMember(dest => dest.PublicacionId, opt => opt.MapFrom(src => src.PublicacionId)); // Mapear PublicacionId

            CreateMap<Comment, SaveCommentViewModel>()
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.Usuarioid)) // Mapear Usuarioid a UsuarioId
                .ForMember(dest => dest.PublicacionId, opt => opt.MapFrom(src => src.PublicacionId)); // Mapear PublicacionId

            CreateMap<SavePostViewModel, Post>()
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                // Aquí puedes agregar más asignaciones si es necesario
                ;
            //REPLIES
            CreateMap<Replies, SaveRepliesViewModel>()
                .ForMember(x => x.Comentarios, opt => opt.Ignore())
                .ForMember(x => x.Usuarioid, opt => opt.Ignore())
                .ForMember(x => x.ComentarioId, opt => opt.Ignore());

            CreateMap<SaveRepliesViewModel, Replies>()
    
    .ForMember(dest => dest.ComentarioId, opt => opt.MapFrom(src => src.ComentarioId));


            CreateMap<Friend, FriendViewModel>()
                 .ForMember(dest => dest.Usuarioid1, opt => opt.MapFrom(src => src.Usuarioid1))
                .ForMember(dest => dest.Usuarioid2, opt => opt.MapFrom(src => src.Usuarioid2))
                 .ReverseMap()
               .ForMember(dest => dest.Usuarioid1, opt => opt.MapFrom(src => src.Usuarioid1))
               .ForMember(dest => dest.Usuarioid2, opt => opt.MapFrom(src => src.Usuarioid2));



            CreateMap<SaveFriendViewModel, Friend>()
                .ForMember(dest => dest.Usuarioid1, opt => opt.MapFrom(src => src.Usuarioid1))
                .ForMember(dest => dest.Usuarioid2, opt => opt.MapFrom(src => src.Usuarioid2))
                 .ReverseMap()
               .ForMember(dest => dest.Usuarioid1, opt => opt.MapFrom(src => src.Usuarioid1))
               .ForMember(dest => dest.Usuarioid2, opt => opt.MapFrom(src => src.Usuarioid2));



            #region UserProfile
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.ImagenUrl, opt => opt.MapFrom(src => src.ImageUrl));


            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

        }
    }
}