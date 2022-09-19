using System;
using System.Collections.Generic;
using System.Linq;

namespace Nemesis;

public class Game
{
    private ICollection<Room> rooms;
    private IReadOnlyList<Player> players;
    private bool isGameOver;
    
    private int turnCounter = 15;
    
    private bool isSelfDestructionOn;
    private int selfDesctructionCounter = 7;

    private List<AlienToken> aliensBag  = new();

    public Game(IEnumerable<Player> players)
    {
        this.players = players.ToArray();
        if (this.players.Count == 0 || this.players.Count > 5)
            throw new ArgumentException($"Player count must be between 1 and 5");
        
        aliensBag.AddRange(new []
        {
            new AlienToken(AlienTokenType.Empty, 0),
            new AlienToken(AlienTokenType.Truten, 1)
            // todo
        });
        
        aliensBag.AddRange(this.players.Select(x => new AlienToken(AlienTokenType.Truten, new Random().Next(2) + 1)));
    }

    public void Play()
    {
        while (isGameOver)
        {
            DoPlayerTurn();
            DoAlienTurn();
        }
    }

    private void DoPlayerTurn()
    {
        
    }

    private void DoAlienTurn()
    {
        var isGameOverNow = MoveTimeCounters();
        if (isGameOverNow)
            return;
        
        AttackByAliens();
        DealDamageToAliensByFire();
        PlayEventCard();
        TakeAlienTokenFromBag();
    }

    private bool MoveTimeCounters()
    {
        var isGameOverNow = false;
        
        turnCounter--;
        if (turnCounter == 0)
        {
            isGameOverNow = true;
        }

        if (isSelfDestructionOn)
        {
            selfDesctructionCounter--;
            if (selfDesctructionCounter == 0)
            {
                isGameOverNow = true;
            }
        }

        return isGameOverNow;
    }

    private void AttackByAliens()
    {
        foreach (var room in rooms)
        {
            var players = room.Creatures.OfType<Player>().ToArray();
            if (players.Length == 0)
                continue;

            var aliens = room.Creatures.OfType<Alien>().ToArray();
            if (aliens.Length == 0)
                continue;

            foreach (var alien in aliens)
            {
                var player = players.OrderBy(x => x.Сards.Count).First();

                var alienAttackCard = new AlienAttackCard();
                
                if (alienAttackCard.IsAttacking(alien))
                {
                    player.TakeDamage(alienAttackCard.Damage);
                }
            }
        }
    }

    private void DealDamageToAliensByFire()
    {
        foreach (var room in rooms.Where(r => r.IsBurning))
        {
            foreach (var alien in room.Creatures.OfType<Alien>())
            {
                alien.TakeDamage(1);
            }
        }
    }

    private void PlayEventCard()
    {
        // move aliens
        // play event
    }

    private void TakeAlienTokenFromBag()
    {
        var token = aliensBag[new Random().Next(aliensBag.Count)];

        switch (token.Type)
        {
            case AlienTokenType.Lichinka:
                aliensBag.Remove(token);
                aliensBag.Add(new AlienToken(AlienTokenType.Truten, 2));
                break;
            case AlienTokenType.Polzyn:
                aliensBag.Remove(token);
                aliensBag.Add(new AlienToken(AlienTokenType.Hunter, 3));
                break;
            case AlienTokenType.Truten:
            case AlienTokenType.Hunter:
                DoNoiseCheckForAllPlayers();
                break;
            case AlienTokenType.Queen:
                ProcessQueenToken(token);
                break;
            case AlienTokenType.Empty:
                aliensBag.Add(new AlienToken(AlienTokenType.Truten, 2));
                break;
        }
    }

    private void ProcessQueenToken(AlienToken token)
    {
        var nest = rooms.FirstOrDefault(); // todo where roomType = Nest;
        if (nest.Creatures.OfType<Player>().Count() > 0)
        {
            aliensBag.Remove(token);
            // todo spawn queen;
        }
        else
        {
            // todo add one egg to aliens nest
        }
    }

    private void DoNoiseCheckForAllPlayers()
    {
        foreach (var player in players.Where(x =>
                 {
                     // todo not in battle
                     return true;
                 }))
        {
            DoNoiseCheck(player);
        }
    }

    private void DoNoiseCheck(Player player)
    {
        
    }
}

public class Room
{
    public IReadOnlyCollection<Corridor> Corridors { get; }
    public IReadOnlyCollection<Creature> Creatures { get; }
    public bool IsBroken { get; }
    public bool IsBurning { get; }
}

public class Corridor
{
    public IReadOnlySet<int> Numbers { get; }
    public DoorStatus DoorStatus { get; }
    public bool HasNoise { get; }
}

public enum DoorStatus
{
    Open,
    Closed,
    Broken
}

public abstract class Creature
{
    public abstract void TakeDamage(int damage);
}

public class Player : Creature
{
    public ICollection<Card> Сards { get; }
    public override void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }
}

public class Alien : Creature
{
    public override void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }
}

public class Card
{
    
}

public class AlienAttackCard
{
    public int Damage { get; set; }

    public bool IsAttacking(Alien alien)
    {
        throw new NotImplementedException();
    }
}

public enum AlienTokenType
{
    Empty,
    Lichinka,
    Polzyn,
    Truten,
    Hunter,
    Queen
}

public struct AlienToken
{
    public AlienTokenType Type { get; set; }
    public int Attention { get; set; }

    public AlienToken(AlienTokenType type, int attention)
    {
        Type = type;
        Attention = attention;
    }
}