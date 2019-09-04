using ControlPanel.Domain.Contracts;
using ControlPanel.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO.Compression;
using System.Threading.Tasks;

namespace ControlPanel.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IClientFileProcessorService _fileProcessorService;
        public HomeController(IClientFileProcessorService fileProcessorService)
        {
            _fileProcessorService = fileProcessorService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFile([Required]IFormFile file)
        {
            var message = string.Empty;
            if (file == null)
            {
                message = "File details required.";
            }
            else
            {
                using (var stream = file.OpenReadStream())
                using (ZipArchive archive = new ZipArchive(stream))
                {
                    message = await _fileProcessorService.EncryptAndPostAsync(archive, file.FileName);
                }
            }
            return View("SubmitFIleResult", message);
        }
    }
}
