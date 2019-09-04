using ControlPanel.Domain;
using ControlPanel.Domain.Contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ControlPanel.Data
{
    public class DataContext : IdentityDbContext, IDataContext
    {
        /// <summary>
        /// This constructor is used by unit tests.
        /// </summary>
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<ZipFileInfo> ZipFileInfo { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
