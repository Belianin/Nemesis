using Nemesis.Rooms;

namespace Nemesis;

public static class DefaultRooms
{
    public static RoomDescription Hibernatorium => new(RoomType.Hibernatorium, RoomLootType.None, false);
    
    public static RoomDescription EvacuationSectionA => new(RoomType.EvacuationSectionA, RoomLootType.Any, false); 
    public static RoomDescription EvacuationSectionB => new(RoomType.EvacuationSectionB, RoomLootType.Any, false);
    public static RoomDescription Armory => new(RoomType.Armory, RoomLootType.Red, false);
    public static RoomDescription CommsRoom => new(RoomType.CommsRoom, RoomLootType.Yellow, true);
    public static RoomDescription Storage => new(RoomType.Storage, RoomLootType.Red, false);
    public static RoomDescription Surgery => new(RoomType.Surgery, RoomLootType.Green, false);
    public static RoomDescription FireControlSystem => new(RoomType.FireControlSystem, RoomLootType.Yellow, true);
    public static RoomDescription Generator => new(RoomType.Generator, RoomLootType.Yellow, true);
    public static RoomDescription EmergencyRoom => new(RoomType.EmergencyRoom, RoomLootType.Green, false);
    public static RoomDescription Laboratory => new(RoomType.Laboratory, RoomLootType.Green, true);
    public static RoomDescription Nest => new(RoomType.Nest, RoomLootType.None, false);

    public static RoomDescription[] FirstTierRooms => new[]
    {
        EvacuationSectionA,
        EvacuationSectionB,
        Armory,
        CommsRoom,
        Storage,
        Surgery,
        FireControlSystem,
        Generator,
        EmergencyRoom,
        Laboratory,
        Nest
    };
    
    public static RoomDescription AirlockControl => new(RoomType.AirlockControl, RoomLootType.Yellow, false);
    public static RoomDescription ShowerRoom => new(RoomType.ShowerRoom, RoomLootType.Any, false);
    public static RoomDescription CommandCenter => new(RoomType.CommandCenter, RoomLootType.Red, true);
    public static RoomDescription RoomCoveredWithSlime => new(RoomType.RoomCoveredWithSlime, RoomLootType.None, false);
    public static RoomDescription EngineControlRoom => new(RoomType.EngineControlRoom, RoomLootType.Yellow, true);
    public static RoomDescription Canteen => new(RoomType.Canteen, RoomLootType.Green, false);
    public static RoomDescription Cabins => new(RoomType.Cabins, RoomLootType.Any, false);
    public static RoomDescription HatchControlSystem => new(RoomType.HatchControlSystem, RoomLootType.Any, false);
    public static RoomDescription MonitoringRoom => new(RoomType.MonitoringRoom, RoomLootType.Red, true);

    public static RoomDescription[] SecondTierRooms => new[]
    {
        AirlockControl,
        ShowerRoom,
        CommandCenter,
        RoomCoveredWithSlime,
        EngineControlRoom,
        Canteen,
        Cabins,
        HatchControlSystem,
        MonitoringRoom
    };
}

public enum RoomType2
{
    EvacuationSectionA,
    EvacuationSectionB,
    Armory,
    /// <summary>
    /// Радиорубка
    /// </summary>
    CommsRoom,
    /// <summary>
    /// Лазарет
    /// </summary>
    EmergencyRoom,
    FireControlSystem,
    Generator,
    Laboratory,
    Nest,
    Storage,
    Surgery,
    
    AirlockControl,
    /// <summary>
    /// Каюты
    /// </summary>
    Cabins,
    Canteen,
    CommandCenter,
    EngineControlRoom,
    /// <summary>
    /// Система блокировки капсул
    /// </summary>
    HatchControlSystem,
    MonitoringRoom,
    RoomCoveredWithSlime,
    ShowerRoom,
    
    Cockpit,
    Hibernatorium,
    Engine1,
    Engine2,
    Engine3
}