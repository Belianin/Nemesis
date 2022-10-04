using System;
using System.Collections.Generic;
using System.Linq;

namespace Nemesis.Rooms;

public class Room
{
    public int Id { get; }
    public RoomDescription Description { get; }
    private readonly List<Corridor> corridors = new();
    public IReadOnlyCollection<Corridor> Corridors => corridors;
    public IReadOnlyCollection<Creature> Creatures { get; }
    public bool IsBroken { get; set; }
    public bool IsBurning { get; set;}
    public int LootCounter { get; set; }
    public bool IsExplored { get; private set; }
    public ExploreToken ExploreToken { get; private set; }

    public Room(int id, RoomDescription description)
    {
        Id = id;
        Description = description;
    }

    public void PutExploreToken(ExploreToken token)
    {
        ExploreToken = token;
        LootCounter = token.LootCount;
    }

    public void Explore()
    {
        if (IsExplored)
            throw new InvalidOperationException("Already explored");
        
        IsExplored = true;
        switch (ExploreToken.Type)
        {
            case ExplorerTokenType.Fire:
                IsBurning = true;
                break;
            case ExplorerTokenType.Broken:
                IsBroken = true;
                break;
        }
    }
    
    public Room ConnectTo(Room rootToConnect, IEnumerable<int> numbers)
    {
        var corridor = new Corridor
        {
            Numbers = numbers.ToHashSet(),
            From = this,
            To = rootToConnect
        };
        
        corridors.Add(corridor);

        return this;
    }
}