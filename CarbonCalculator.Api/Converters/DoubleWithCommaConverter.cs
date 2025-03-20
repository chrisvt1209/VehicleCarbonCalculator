using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarbonCalculator.Api.Converters;

public class DoubleWithCommaConverter : JsonConverter<double>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(double);
    }

    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            if (double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
            if (double.TryParse(stringValue, NumberStyles.Any, new CultureInfo("nl-NL"), out result))
            {
                return result;
            }
        }
        return reader.GetDouble();
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}
