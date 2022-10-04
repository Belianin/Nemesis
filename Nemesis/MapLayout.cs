using System;
using System.Collections.Generic;
using System.Linq;

namespace Nemesis;

public class MapLayout
{
    public Dictionary<int, RoomTier> Rooms { get; set; }
    public ICollection<CorridorLayout> Corridors { get; set; }
}

public struct CorridorLayout
{
    public IReadOnlySet<int> Numbers { get; }
    public int FirstRoomId { get; }
    public int SecondRoomId { get; }

    public CorridorLayout(int firstRoomId, int secondRoomId, params int[] numbers)
    {
        if (numbers.Length == 0)
            throw new ArgumentException(nameof(numbers));
        
        Numbers = numbers.ToHashSet();
        FirstRoomId = firstRoomId;
        SecondRoomId = secondRoomId;
    }
}

public enum RoomTier
{
    Cockpit,
    Hibernatorium,
    FirstEngine,
    SecondEngine,
    ThirdEngine,
    FirstTier,
    SecondTier
}

public static class RoomTierExtensions
{
    public static bool IsExplorationTokenNeeded(this RoomTier tier) => tier is RoomTier.FirstTier or RoomTier.SecondTier;
}