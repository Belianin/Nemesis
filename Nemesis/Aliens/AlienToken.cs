using System;

namespace Nemesis.Aliens;

public struct AlienToken
{
    public AlienType Type { get; set; }
    public int Attention { get; set; }

    public AlienToken(AlienType type, int attention)
    {
        Type = type;
        Attention = attention;
    }

    public bool Equals(AlienToken other)
    {
        return Type == other.Type && Attention == other.Attention;
    }

    public override bool Equals(object? obj)
    {
        return obj is AlienToken other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int) Type, Attention);
    }
}