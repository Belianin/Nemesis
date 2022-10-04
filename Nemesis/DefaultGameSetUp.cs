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
        
        bag.TryPutToken(AlienType.Empty);
        bag.TryPutTokens(AlienType.Larva, 4);
        bag.TryPutToken(AlienType.Creeper);
        bag.TryPutTokens(AlienType.Adult, 3 + playersCount);

        return bag;
    }
    
    private static AlienTokenBag GetEmptyAlienTokenBag()
    {
        var tokens = new List<AlienToken>();

        void AddTokens(AlienType tokenType, int attention, int count)
        {
            tokens.AddRange(Enumerable.Repeat(new AlienToken(tokenType, attention), count));
        }
        
        AddTokens(AlienType.Larva, 1, 8);
        AddTokens(AlienType.Creeper, 1, 3);
        AddTokens(AlienType.Adult, 4, 3);
        AddTokens(AlienType.Adult, 2, 4);
        AddTokens(AlienType.Adult, 3, 5);
        
        tokens.AddRange(new []
        {
            new AlienToken(AlienType.Empty, 1),
            new AlienToken(AlienType.Breeder, 3),
            new AlienToken(AlienType.Breeder, 4),
            new AlienToken(AlienType.Queen, 4)
        });

        return new AlienTokenBag(tokens);
    }
}