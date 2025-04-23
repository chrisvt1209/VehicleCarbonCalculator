using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarbonCalculator.Api.Converters;

public class BooleanConverter : JsonConverter<bool>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(bool);
    }

    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return (reader.GetString() ?? "Nee") == "Ja";
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value ? "Ja" : "Nee");
    }
}
