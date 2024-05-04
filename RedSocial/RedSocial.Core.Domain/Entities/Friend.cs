using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Domain.Entities
{
    public class Friend
    {
       
        public int Id { get; set; }
        public string Usuarioid1 { get; set; }
        public string Usuarioid2 { get; set; }
        public DateTime FechaEstablecimiento { get; set; }
    }
}
