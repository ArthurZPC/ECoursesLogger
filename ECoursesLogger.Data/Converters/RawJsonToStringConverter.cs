using System.Text.Json;
using System.Text.Json.Serialization;

namespace ECoursesLogger.Data.Converters
{
    public class RawJsonToStringConverter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonDocument.ParseValue(ref reader).RootElement.ToString();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            
        }
    }
}
