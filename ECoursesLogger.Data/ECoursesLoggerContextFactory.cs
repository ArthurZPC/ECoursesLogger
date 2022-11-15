using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ECoursesLogger.Data
{
    public class ECoursesLoggerContextFactory : IDesignTimeDbContextFactory<ECoursesLoggerContext>
    {
        public ECoursesLoggerContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ECoursesLoggerContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!);

            return new ECoursesLoggerContext(optionsBuilder.Options);
        }
    }
}
