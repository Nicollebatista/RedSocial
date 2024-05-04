using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RedSocial.Core.Domain.Entities
{
    public class Post 
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string? Publicacion { get; set; }
        public string? Usuarioid { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
