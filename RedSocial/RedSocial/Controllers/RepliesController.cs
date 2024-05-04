using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Replies;

namespace RedSocial.Controllers
{
    [Authorize]
    public class RepliesController : Controller
    {
        private readonly IUserService _userServices;
        private readonly IRepliesServices _RepliesServices;

        public RepliesController(IUserService userServices, IRepliesServices RepliesServices)
        {
            _RepliesServices = RepliesServices;
            _userServices = userServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int CommentId)
        {
            var vm = new SaveRepliesViewModel();
            vm.ComentarioId = CommentId;
            return View("SaveReplies", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveRepliesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveReplies", vm);
            }

            await _RepliesServices.Add(vm);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
