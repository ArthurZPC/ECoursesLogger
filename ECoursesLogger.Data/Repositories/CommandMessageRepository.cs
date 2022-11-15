using ECoursesLogger.Data.Entities;
using ECoursesLogger.Data.Interfaces;

namespace ECoursesLogger.Data.Repositories
{
    public class CommandMessageRepository : ICommandMessageRepository
    {
        private readonly ECoursesLoggerContext _context;

        public CommandMessageRepository(ECoursesLoggerContext context)
        {
            _context = context;
        }

        public async Task Create(CommandMessage commandMessage)
        {
            _context.Add(commandMessage);

            await _context.SaveChangesAsync();
        }
    }
}
