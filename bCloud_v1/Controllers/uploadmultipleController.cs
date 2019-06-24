using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using bCloud_v1.Models;
using bCloud_v1.Models.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using bCloud_v1.Models.AccountViewModels;
using System.Security.Claims;

namespace bCloud_v1.Controllers
{
    public class uploadmultipleController : Controller
    {
        private readonly IFileProvider fileProvider;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;


        //class constructor
       

        public uploadmultipleController(IFileProvider fileProvider, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            this.fileProvider = fileProvider;
            _userManager = userManager;

        }
        [HttpPost]
  

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            var user = await _userManager.GetUserAsync(User);
            var email = user.Email;
            var dirName = $"Dir{email}";

            if (files == null || files.Count == 0)
                return Content("files not selected");
            
            foreach (var file in files)
            {
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), $"wwwroot/upload/{dirName}/",
                        file.GetFilename());

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return RedirectToAction("Files");
           
        }


        public IActionResult Files()
        {

            
            var model = new FilesViewModel();

            foreach (var item in this.fileProvider.GetDirectoryContents(""))
            {
                model.Files.Add(
                    new FileDetails { Name = item.Name, Path = item.PhysicalPath });
            }
            return View(model);
        }

        public async Task<IActionResult> Download(string filename)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            var user = await _userManager.GetUserAsync(User);
            var email = user.Email;
            var dirName = $"Dir{email}";
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           $"wwwroot/upload/{dirName}/", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
   
    }
}
