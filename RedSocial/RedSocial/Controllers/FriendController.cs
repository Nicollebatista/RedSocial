using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Services;
using RedSocial.Core.Application.ViewModels.Friend;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Domain.Entities;
using System.Threading.Tasks;

[Authorize]
public class FriendController : Controller
{
    private readonly IFriendService _friendServices;
    private readonly IUserService _userServices;
    private IPostServices _postServices;

    public FriendController(IUserService userServices, IFriendService friendServices, IPostServices postServices)
    {
        _postServices = postServices;
        _userServices = userServices;
        _friendServices = friendServices;
    }

    public async Task<IActionResult> Index()
    {
        var friends = await _friendServices.GetFriendsForUser();
        var posts = await _postServices.GetPostsCreatedByFriendsOfCurrentUser();
        var viewModel = (Amigos: friends, Posts: posts);
        return View(viewModel);
    }


    public IActionResult Buscar()
    {
        if (TempData.ContainsKey("ErrorMessage"))
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SearchUsers(string username)
    {
        

        var userViewModels = await _userServices.GetUserByUsernameWithOutUserAsync(username);
        return View("Buscar", userViewModels);
    }

    [HttpPost]
    public async Task<IActionResult> ToBeFriends(string userId)
    {
        SaveFriendViewModel Amigos = new SaveFriendViewModel();
        Amigos.Usuarioid2 = userId;

        var sonAmigos = await _friendServices.ExisteRelacionDeAmistad(userId);
        if (!sonAmigos)
        {
            TempData["ErrorMessage"] = "¡Tienes un nuevo amigo!";
            await _friendServices.Add(Amigos);
        }
        else
        {
            TempData["ErrorMessage"] = "Ups, Al parecer ya eran amigos!";
        }

        return RedirectToAction("Buscar");
    }

    public async Task<IActionResult> Delete(string id)
    {
        var friends = await _friendServices.GetAmistadad(id);
           
        return View(friends);
    }

    [HttpPost]
    public async Task<IActionResult> DeletePost(int id)
    {
        await _friendServices.Delete(id);
        return RedirectToRoute(new { controller = "Friend", action = "Index" });
    }
}
