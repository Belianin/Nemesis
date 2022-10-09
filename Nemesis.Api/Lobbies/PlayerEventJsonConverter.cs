using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nemesis.Api.Lobbies;

public class PlayerEventJsonConverter : JsonConverter<PlayerEvent>
{
    public override PlayerEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDocument = JsonDocument.ParseValue(ref reader);
        
        var type = jsonDocument.RootElement.GetProperty("type").GetString();

        return type switch
        {
            "Message" => jsonDocument.Deserialize<PlayerMessageEvent>(new JsonSerializerOptions(JsonSerializerDefaults.Web))
        };
    }

    public override void Write(Utf8JsonWriter writer, PlayerEvent value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}