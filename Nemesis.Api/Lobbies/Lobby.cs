namespace Nemesis.Api.Lobbies;

public class Lobby
{
    public string Id { get; set; }
    public string Title { get; set; }
    public List<string> Players { get; set; }

    public event EventHandler<LobbyEvent> OnEvent;

    public void AddPlayer(string player)
    {
        Players.Add(player);
    }
}