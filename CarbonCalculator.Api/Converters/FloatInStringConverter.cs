using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarbonCalculator.Api.Converters;

public class FloatInStringConverter : JsonConverter<float>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(float);
    }

    public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return float.Parse(reader.GetString() ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
