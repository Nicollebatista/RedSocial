using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.Post 
{
    public class SavePostViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Se requiere una Descripcion")]
        public string Descripcion { get; set; }
        public string? Publicacion { get; set; }
        public string? Usuarioid { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
    }
}
