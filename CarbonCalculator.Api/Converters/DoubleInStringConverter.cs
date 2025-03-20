using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarbonCalculator.Api.Converters;

public class DoubleInStringConverter : JsonConverter<double>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(double);
    }

    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return double.Parse(reader.GetString() ?? throw new InvalidOperationException());
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
