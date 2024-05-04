using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Post;
using WebApp.RedSocial.Middlewares;

namespace RedSocial.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostServices _postService;
        IFormFile? file;
        public PostController(IPostServices postService, ValidateUserSession validateUserSession)
        {
            _postService = postService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            SavePostViewModel vm = new();
            return View("SavePost", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePostViewModel vm)
        {
            

            if (!ModelState.IsValid)
            {
                return View("SavePost", vm);
            }
            if (vm.File != null)
            {
                file = vm.File;
            }
            SavePostViewModel postVm = await _postService.Add(vm);
             
            if (postVm.Id != 0 && postVm != null)
            {
                postVm.File = file;
                if (postVm.File != null)
                {
                                    
                postVm.Publicacion = UploadFile(vm.File, postVm.Id);

                await _postService.Update(postVm, postVm.Id);
                }
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        public async Task<IActionResult> Edit(int id)
        {
            SavePostViewModel vm = await _postService.GetByIdSaveViewModel(id);
            return View("SavePost", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SavePost", vm);
            }

            SavePostViewModel postVm = await _postService.GetByIdSaveViewModel(vm.Id);

            if (postVm != null && !string.IsNullOrEmpty(postVm.Publicacion) && !postVm.Publicacion.Contains("youtube.com"))
            {
                vm.Publicacion = UploadFile(vm.File, vm.Id, true, postVm.Publicacion);
            }

            if (postVm != null)
            {
                await _postService.Update(vm, vm.Id);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }


        public async Task<IActionResult> Delete(int id)
        {
            return View(await _postService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.Delete(id);

            string basePath = $"/Images/Products/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }























        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Post/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}
