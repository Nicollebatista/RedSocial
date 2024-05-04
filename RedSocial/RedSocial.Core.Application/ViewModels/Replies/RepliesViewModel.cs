using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.Replies
{
    public class RepliesViewModel
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public string Usuarioid { get; set; }
        public int ComentarioId { get; set; }
        public string UsuarioNombre { get; set; }
        public string ImagenUrl { get; set; }
        public DateTime FechaHoraComentario { get; set; } = DateTime.Now;
    }
}
