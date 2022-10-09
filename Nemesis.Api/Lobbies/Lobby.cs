namespace Nemesis.Api.Lobbies;

public class Lobby
{
    public string Id { get; set; }
    public string Title { get; set; }
    public HashSet<string> Players { get; set; } = new();
    public string Host { get; set; }

    public event EventHandler<LobbyEvent> OnEvent;

    public void AddPlayer(string player)
    {
        Players.Add(player);
        OnEvent?.Invoke(this, new MessageEvent
        {
            Author = player,
            Message = "Joined"
        });
    }

    public void RemovePlayer(string player)
    {
        Players.Remove(player);
        OnEvent?.Invoke(this, new MessageEvent
        {
            Author = player,
            Message = "Left"
        });
    }

    public void Process(PlayerEvent playerEvent, string player)
    {
        if (playerEvent is PlayerMessageEvent message)
        {
            OnEvent?.Invoke(this, new MessageEvent
            {
                Author = player,
                Message = message.Message
            });
        }
    }
}