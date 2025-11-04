using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rdw.ApiWrapper.Converters;

public class IntegerInStringConverter : JsonConverter<int>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(int);
    }

    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return int.Parse(reader.GetString() ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
