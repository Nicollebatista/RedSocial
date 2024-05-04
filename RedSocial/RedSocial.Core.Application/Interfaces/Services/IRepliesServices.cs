using RedSocial.Core.Application.ViewModels.Replies;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IRepliesServices :  IGenericService<SaveRepliesViewModel, RepliesViewModel, Replies>
    {
    }
}
