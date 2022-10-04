using System;
using System.Collections.Generic;
using System.Linq;

namespace Nemesis.Rooms;

public class RoomDescription
{
    public RoomType Type { get; }
    public bool HasComputer { get; }
    public RoomLootType LootType { get; } 

    public RoomDescription(RoomType roomType, RoomLootType lootType, bool hasComputer)
    {
        Type = roomType;
        LootType = lootType;
        HasComputer = hasComputer;
    }
}