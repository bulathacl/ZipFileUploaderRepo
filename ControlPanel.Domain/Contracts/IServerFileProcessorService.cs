using System.Threading.Tasks;

namespace ControlPanel.Domain.Contracts
{
    public interface IServerFileProcessorService
    {
        Task<string> DecryptAndSaveDetailsAsync(byte[] chiper, string fileName);
    }
}
