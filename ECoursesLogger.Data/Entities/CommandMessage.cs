using ECoursesLogger.Data.Converters;
using System.Text.Json.Serialization;

namespace ECoursesLogger.Data.Entities
{
    public class CommandMessage : Entity
    {
        public string CommandName { get; set; } = string.Empty;
        public string CommandType { get; set; } = string.Empty;

        [JsonConverter(typeof(RawJsonToStringConverter))]
        public string CommandContent { get; set; } = string.Empty;

        public DateTime ExecutedAt { get; set; }
    }
}
