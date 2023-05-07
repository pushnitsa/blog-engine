using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlogEngine.DataSource.Serialization;
public class DateTimeSerializer : JsonConverter<DateTime>
{
    private readonly string _format;

    public DateTimeSerializer(string format)
    {
        _format = format;
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var result = DateTime.ParseExact(reader.GetString()!, _format, null);

        return result;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_format));
    }
}
