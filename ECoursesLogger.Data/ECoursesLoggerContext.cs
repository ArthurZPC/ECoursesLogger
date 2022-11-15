using ECoursesLogger.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECoursesLogger.Data
{
    public class ECoursesLoggerContext : DbContext
    {
        public DbSet<CommandMessage> CommandMessages => Set<CommandMessage>();

        public ECoursesLoggerContext(DbContextOptions<ECoursesLoggerContext> options) : base(options) { }
    }
}
