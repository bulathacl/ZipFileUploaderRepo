using ControlPanel.Domain;
using ControlPanel.Domain.Contracts;
using System.Threading.Tasks;

namespace ControlPanel.Services
{
    public class ServerFileProcessorService : IServerFileProcessorService
    {
        private readonly IDataContext _dataContext;
        private readonly ICustomEncryptionService _customEncryptionService;
        public ServerFileProcessorService(IDataContext dataContext, ICustomEncryptionService customEncryptionService)
        {
            _dataContext = dataContext;
            _customEncryptionService = customEncryptionService;
        }

        public async Task<string> DecryptAndSaveDetailsAsync(byte[] chiper, string fileName)
        {
            var message = string.Empty;
            try
            {
                var decryptedValue = _customEncryptionService.DecryptStringFromBytes(chiper);
                try
                {
                    var zipFileInfo = new ZipFileInfo
                    {
                        FileHierarchy = decryptedValue,
                        FileName = fileName
                    };

                    await _dataContext.ZipFileInfo.AddAsync(zipFileInfo);
                    await _dataContext.SaveChangesAsync();
                }
                catch
                {
                    message = "Error occurred while saving to database";
                }
            }
            catch
            {
                message = "Error occurred while decrypting file hierarchy.";
            }
            return message;
        }
    }
}
