using ControlPanel.Domain;
using ControlPanel.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ControlPanel.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileInfoController : ControllerBase
    {
        private readonly IServerFileProcessorService _fileProcessorService;
        public FileInfoController(IServerFileProcessorService fileProcessorService)
        {
            _fileProcessorService = fileProcessorService;
        }

        [HttpPost]
        public async Task<string> Post(RequestDto data)
        {
            return await _fileProcessorService.DecryptAndSaveDetailsAsync(data.EncryptedHierarchy, data.FileName);
        }
    }
}
