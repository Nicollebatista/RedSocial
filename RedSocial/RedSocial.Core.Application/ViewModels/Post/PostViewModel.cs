using RedSocial.Core.Application.ViewModels.Comment;


namespace RedSocial.Core.Application.ViewModels.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Publicacion { get; set; }
        public string Usuarioid { get; set; }
        public string UsuarioNombre { get; set; }
        public string ImagenUrl { get; set; }
      
        public ICollection<CommentViewModel>? Comentarios { get; set; } 
    }
}
