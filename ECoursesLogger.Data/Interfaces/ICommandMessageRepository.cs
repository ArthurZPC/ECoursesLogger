using ECoursesLogger.Data.Entities;

namespace ECoursesLogger.Data.Interfaces
{
    public interface ICommandMessageRepository
    {
        Task Create(CommandMessage commandMessage);
    }
}
