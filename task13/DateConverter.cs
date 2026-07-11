using System.Text.Json;
using System.Text.Json.Serialization;

public class DateConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string dateString = reader.GetString();
        return DateTime.ParseExact(dateString, "dd.MM.yyyy", null);
    }
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("dd.MM.yyyy"));
    }
}