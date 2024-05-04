using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Comment;

namespace RedSocial.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IUserService _userServices;
        private readonly ICommentService _CommentServices;

        public CommentController(IUserService userServices, ICommentService CommentServices)
        {
            _CommentServices = CommentServices;
            _userServices = userServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int PostId)
        {
            var vm = new SaveCommentViewModel();
            vm.PublicacionId = PostId;
            return View("SaveComment", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCommentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveComment", vm);
            }

            await _CommentServices.Add(vm);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
