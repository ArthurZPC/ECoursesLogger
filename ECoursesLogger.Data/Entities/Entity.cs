using System.ComponentModel.DataAnnotations;

namespace ECoursesLogger.Data.Entities
{
    public class Entity
    {
        [Required]
        public Guid Id { get; set; }
    }
}
