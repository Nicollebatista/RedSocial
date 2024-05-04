using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Models;
using System.Diagnostics;
using WebApp.RedSocial.Middlewares;

namespace RedSocial.Controllers
{
    [Authorize]
    public class HomeController : Controller
    { 
        private readonly IPostServices _postServices;

        public HomeController(IPostServices postServices,  ValidateUserSession validateUserSession)
        {
            _postServices = postServices;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _postServices.GetPostsCreatedByCurrentUser());
        }

    }
}