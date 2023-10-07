using Microsoft.EntityFrameworkCore;
using Avtobus1TestTask.Models;

namespace Avtobus1TestTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<URL_Statistics> URL_Statistics { get; set; }
    }
}
