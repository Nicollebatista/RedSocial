using RedSocial.Core.Application.ViewModels.Comment;
using System.ComponentModel.DataAnnotations;

namespace RedSocial.Core.Application.ViewModels.Replies
{
    public class SaveRepliesViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe tener un comentario")]
        public string Contenido { get; set; }
        public string? Usuarioid { get; set; }
        public int? ComentarioId { get; set; }
        public CommentViewModel? Comentarios { get; set; }

    }
}
