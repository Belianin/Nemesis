using System;
using System.Collections.Generic;
using System.Linq;
using Nemesis.Aliens;
using Nemesis.Common;
using Nemesis.Rooms;

namespace Nemesis;

public static class DefaultGameSetUp
{
    private static Dictionary<int, Room> GenerateMap()
    {
        return FillMap(Maps.Default); 
    }

    private static Dictionary<int, Room> FillMap(MapLayout layout)
    {
        var firstTierRooms = new Stack<RoomDescription>(DefaultRooms.FirstTierRooms.Shuffle());
        var secondTierRooms = new Stack<RoomDescription>(DefaultRooms.SecondTierRooms.Shuffle());

        Stack<ExploreToken> explorationTokens = new();

        var rooms = new Dictionary<int, Room>();

        foreach (var (id, roomTier) in layout.Rooms)
        {
            var roomDescription = roomTier switch
            {
                RoomTier.Cockpit => null,
                RoomTier.FirstEngine => null,
                RoomTier.SecondEngine => null,
                RoomTier.ThirdEngine => null,
                RoomTier.Hibernatorium => DefaultRooms.Hibernatorium,
                RoomTier.FirstTier => firstTierRooms.Pop(),
                RoomTier.SecondTier => secondTierRooms.Pop()
            };

            var room = new Room(id, roomDescription);
            
            if (roomTier.IsExplorationTokenNeeded())
                room.PutExploreToken(explorationTokens.Pop());

            rooms[id] = new Room(id, roomDescription);
        }

        foreach (var corridorLayout in layout.Corridors)
        {
            rooms[corridorLayout.FirstRoomId].ConnectTo(rooms[corridorLayout.SecondRoomId], corridorLayout.Numbers);
        }

        return rooms;
    }

    private static AlienTokenBag GetFilledAlienTokenBag(int playersCount)
    {
        var bag = GetEmptyAlienTokenBag();
        
        bag.TryPutToken(AlienTokenType.Empty);
        bag.TryPutTokens(AlienTokenType.Larva, 4);
        bag.TryPutToken(AlienTokenType.Creeper);
        bag.TryPutTokens(AlienTokenType.Adult, 3 + playersCount);

        return bag;
    }
    
    private static AlienTokenBag GetEmptyAlienTokenBag()
    {
        var tokens = new List<AlienToken>();

        void AddTokens(AlienTokenType tokenType, int attention, int count)
        {
            tokens.AddRange(Enumerable.Repeat(new AlienToken(tokenType, attention), count));
        }
        
        AddTokens(AlienTokenType.Larva, 1, 8);
        AddTokens(AlienTokenType.Creeper, 1, 3);
        AddTokens(AlienTokenType.Adult, 4, 3);
        AddTokens(AlienTokenType.Adult, 2, 4);
        AddTokens(AlienTokenType.Adult, 3, 5);
        
        tokens.AddRange(new []
        {
            new AlienToken(AlienTokenType.Empty, 1),
            new AlienToken(AlienTokenType.Breeder, 3),
            new AlienToken(AlienTokenType.Breeder, 4),
            new AlienToken(AlienTokenType.Queen, 4)
        });

        return new AlienTokenBag(tokens);
    }
}