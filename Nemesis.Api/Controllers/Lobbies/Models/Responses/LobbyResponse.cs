using Nemesis.Api.Lobbies;

namespace Nemesis.Api.Controllers.Lobbies.Models.Respones;

public class LobbyResponse
{
    public string Id { get; set; }
    public string Title { get; set; }
    public int PlayersCount { get; set; }
    public string Host { get; set; }

    public LobbyResponse(Lobby lobby)
    {
        Id = lobby.Id;
        Title = lobby.Title;
        PlayersCount = lobby.Players.Count;
        Host = lobby.Host;
    }
}