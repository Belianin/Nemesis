using System.Collections.Generic;

namespace Nemesis;

public static class Maps
{
    public static MapLayout Default => new()
    {
        Rooms = new Dictionary<int, RoomTier>
        {
            [1] = RoomTier.Cockpit,
            [2] = RoomTier.FirstTier,
            [3] = RoomTier.FirstTier,
            [4] = RoomTier.FirstTier,
            [5] = RoomTier.FirstTier,
            [6] = RoomTier.SecondTier,
            [7] = RoomTier.SecondTier,
            [8] = RoomTier.SecondTier,
            [9] = RoomTier.FirstTier,
            [10] = RoomTier.FirstTier,
            [11] = RoomTier.Hibernatorium,
            [12] = RoomTier.FirstTier,
            [13] = RoomTier.FirstTier,
            [14] = RoomTier.SecondTier,
            [15] = RoomTier.SecondTier,
            [16] = RoomTier.FirstTier,
            [17] = RoomTier.FirstTier,
            [18] = RoomTier.FirstTier,
            [19] = RoomTier.FirstEngine,
            [20] = RoomTier.SecondEngine,
            [21] = RoomTier.ThirdEngine
        },
        Corridors = new CorridorLayout[]
        {
            new(1, 2, 3),
            new(1, 3, 1, 2),
            new(1, 4, 4),
            new(2, 0, 1, 2),
            new(2, 6, 4),
            new(3, 7, 3, 4),
            new(4, 8, 1),
            new(4, 0, 2, 3),
            new(7, 6, 1),
            new(7, 8, 2),
            new(6, 5, 3),
            new(5, 10, 1, 2),
            new(1, 0, 4),
            new(6, 11, 2),
            new(8, 9, 4),
            new(9, 0, 3),
            new(9, 12, 1, 2),
            new(8, 11, 3),
            new(10, 13, 3, 4),
            new(12, 16, 3, 4),
            new(11, 14, 4),
            new(11, 15, 1),
            new(13, 14, 1),
            new(14, 17, 2),
            new(2, 0, 2),
            new(15, 18, 3),
            new(15, 16, 2), 
            new(15, 0, 4),
            new(13, 19, 2),
            new(19, 0, 3, 4),
            new(19, 17, 1),
            new(17, 20, 3, 4),
            new(20, 18, 1, 2),
            new(18, 21, 4),
            new(16, 21, 1),
            new(21, 0, 2, 3)
        }
    };
}