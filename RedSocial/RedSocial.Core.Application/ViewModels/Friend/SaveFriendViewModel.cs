using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.Friend
{
    public class SaveFriendViewModel
    {
        public int Id { get; set; }

        public string Usuarioid1 { get; set; }
        public string Usuarioid2 { get; set; }
    }
}
