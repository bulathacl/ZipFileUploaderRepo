using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ControlPanel.Domain.Contracts
{
    public interface IDataContext
    {
        DbSet<ZipFileInfo> ZipFileInfo { get; set; }

        Task<int> SaveChangesAsync();
    }
}
