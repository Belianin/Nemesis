using System.Collections.Generic;

namespace Nemesis.Rooms;

public class Corridor
{
    public IReadOnlySet<int> Numbers { get; set; }
    public DoorStatus DoorStatus { get; set; }
    public bool HasNoise { get; set; }
    public Room From { get; set; }
    public Room To { get; set; }
}