using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaHoraComentario { get; set; } = DateTime.Now;
        public string Usuarioid { get; set; }
        public int PublicacionId { get; set; }
        public Post? publicacion { get; set; }
        public ICollection<Replies>? Replies { get; set; }



    }
}
