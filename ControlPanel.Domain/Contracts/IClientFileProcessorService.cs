using System.IO.Compression;
using System.Threading.Tasks;

namespace ControlPanel.Domain.Contracts
{
    public interface IClientFileProcessorService
    {
        Task<string> EncryptAndPostAsync(ZipArchive zipArchive, string fileName);
    }
}
