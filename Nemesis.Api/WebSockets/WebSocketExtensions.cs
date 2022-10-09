using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Nemesis.Api.Lobbies;

namespace Nemesis.Api.WebSockets;

public static class WebSocketExtensions
{
    public static async Task SendAsync<T>(this WebSocket socket, T message)
    {
        await socket.SendAsync(Encoding.UTF8.GetBytes(JsonSerializer.Serialize((object) message, new JsonSerializerOptions(JsonSerializerDefaults.Web))), WebSocketMessageType.Text, true, CancellationToken.None);
    }
}

public class WebSocketListener<T>
{
    private readonly WebSocket socket;
    public event EventHandler<T> OnMessage;

    public WebSocketListener(WebSocket socket)
    {
        this.socket = socket;
    }

    public async Task ListenAsync(CancellationToken cancellationToken)
    {
        var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        jsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        jsonOptions.Converters.Add(new PlayerEventJsonConverter());

        var buffer = new ArraySegment<byte>(new byte[4096]);
        WebSocketReceiveResult result = null;

        while(!cancellationToken.IsCancellationRequested)
        {
            var memoryStream = new MemoryStream();
            do
            {
                result = await socket.ReceiveAsync(buffer, cancellationToken).ConfigureAwait(false);
                if (result.Count > 0)
                {
                    memoryStream.Write(buffer.Array, buffer.Offset, result.Count);
                }
                else
                {
                    break;
                }
            }
            while (!result.EndOfMessage);

            memoryStream.Position = 0;
            OnMessage?.Invoke(this, JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(memoryStream.ToArray()), jsonOptions));
        }
    }
}