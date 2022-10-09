using System.Text;

namespace Nemesis.Api.Lobbies;

public abstract class PlayerEvent
{
    public abstract string Type { get; }
}

public class PlayerMessageEvent : PlayerEvent
{
    public override string Type => "Message";

    public string Message { get; set; }
}