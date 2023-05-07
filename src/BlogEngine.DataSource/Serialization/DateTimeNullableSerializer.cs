using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlogEngine.DataSource.Serialization;
public class DateTimeNullableSerializer : JsonConverter<DateTime?>
{
    private readonly string _format;

    public DateTimeNullableSerializer(string format)
    {
        _format = format;
    }

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        DateTime? result = null;

        if (DateTime.TryParseExact(reader.GetString()!, _format, null, DateTimeStyles.None, out var resultDateTime))
        {
            result = resultDateTime;
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString(_format));
    }
}
