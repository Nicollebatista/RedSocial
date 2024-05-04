using RedSocial.Core.Application.ViewModels.Post;
using RedSocial.Core.Application.ViewModels.Replies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.Comment
{
    public class CommentViewModel
    { 
        public int Id { get; set; }
        public string Contenido { get; set; }
        public int  PublicacionId { get; set; }
        public List<RepliesViewModel>? Replies { get; set; }
        public PostViewModel? Post { get; set; }
        public string UsuarioNombre { get; set; } 
        public string ImagenUrl { get; set; } 
        public string UsuarioId { get; set; } 
        public DateTime FechaHoraComentario { get; set; }  = DateTime.Now;
    }
}
