namespace Nemesis.Api.Lobbies;

public class LobbyService
{
    private Dictionary<string, Lobby> lobbies = new();

    public Lobby Create(string title)
    {
        var lobby = new Lobby
        {
            Title = title,
            Id = GameIdGenerator.GenerateId()
        };
        
        lobbies[lobby.Id] = (lobby);

        return lobby;
    }

    public ICollection<Lobby> GetLobbies()
    {
        return lobbies.Values;
    }

    public void AddPlayer(string lobbyId, string player)
    {
        lobbies[lobbyId].AddPlayer(player);
    }
}