using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Domain.Entities
{
    public class Replies
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaHoraRespuesta { get; set; } = DateTime.Now;
        public string Usuarioid { get; set; }
        public int ComentarioId { get; set; }
        public Comment? Comments { get; set; }
    }
}
