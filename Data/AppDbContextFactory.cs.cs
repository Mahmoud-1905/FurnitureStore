using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FurnitureStore.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=.\\SQLEXPRESS;Database=NEWUserRoles;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}