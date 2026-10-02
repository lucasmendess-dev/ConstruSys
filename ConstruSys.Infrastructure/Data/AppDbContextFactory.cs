using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConstruSys.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<AppDbContext> optionsBuilder =
                new DbContextOptionsBuilder<AppDbContext>();

            const string connectionString =
                "Server=localhost\\SQLEXPRESS;Database=ConstruSysDb;Trusted_Connection=True;TrustServerCertificate=True;";
	

            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}