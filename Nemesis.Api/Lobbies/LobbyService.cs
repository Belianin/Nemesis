using System.Runtime.CompilerServices;

namespace Nemesis.Api.Lobbies;

public class LobbyService
{
    private Dictionary<string, Lobby> lobbies = new();

    public Lobby Create(string title, string host)
    {
        var lobby = new Lobby
        {
            Title = title,
            Id = GameIdGenerator.GenerateId(),
            Host = host
        };
        
        lobbies[lobby.Id] = (lobby);

        return lobby;
    }

    public Lobby GetLobby(string id)
    {
        return lobbies[id];
    }

    public ICollection<Lobby> GetLobbies()
    {
        return lobbies.Values;
    }
}