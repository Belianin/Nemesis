namespace Nemesis.Api.Lobbies;

public static class GameIdGenerator
{
    public static string GenerateId() => Guid.NewGuid().ToString()[..4];
}