namespace Nemesis.Rooms;

public struct ExploreToken
{
    public int LootCount { get; }
    public ExplorerTokenType Type { get; }
    
    public ExploreToken(ExplorerTokenType type, int lootCount)
    {
        Type = type;
        LootCount = lootCount;
    }
}