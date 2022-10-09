namespace Nemesis.Api.Lobbies;

public abstract class LobbyEvent
{
    public abstract string Type { get; }
}

public class MessageEvent : LobbyEvent
{
    public override string Type => "Message";

    public string Author { get; set; }
    public string Message { get; set; }
}