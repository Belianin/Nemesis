using System;
using System.Collections.Generic;
using Nemesis.Aliens;

namespace Nemesis.Cards;

public class AttackCard
{
    public int Health { get; set; }
    public IReadOnlySet<AlienType> AttackingAliens { get; set; }

    public bool IsAlienAttack(Alien alien)
    {
        throw new NotImplementedException();
    }
}