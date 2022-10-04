using System;
using System.Collections.Generic;
using System.Linq;
using Nemesis.Aliens;
using Nemesis.Cards;
using Nemesis.Rooms;

namespace Nemesis;

public class Game
{
    private Dictionary<int, Room> rooms;
    private IReadOnlyList<Player> players;
    private bool isGameOver;
    
    private int turnCounter = 15;
    
    private bool isSelfDestructionOn;
    private int selfDesctructionCounter = 7;

    private AlienTokenBag alienTokenBag;

    private Deck<AttackCard> attackCards;

    public Game(
        IEnumerable<Player> players,
        AlienTokenBag alienTokenBag,
        Dictionary<int, Room> rooms,
        Deck<AttackCard> attackCards)
    {
        this.alienTokenBag = alienTokenBag;
        this.players = players.ToArray();
        this.rooms = rooms;
        this.attackCards = attackCards;

        if (this.players.Count is 0 or > 5)
            throw new ArgumentException($"Player count must be between 1 and 5");
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
            var players = room.Value.Creatures.OfType<Player>().ToArray();
            if (players.Length == 0)
                continue;

            var aliens = room.Value.Creatures.OfType<Alien>().ToArray();
            if (aliens.Length == 0)
                continue;

            foreach (var alien in aliens)
            {
                var player = players.OrderBy(x => x.Сards.Count).First();

                var alienAttackCard = attackCards.TakeCard();
                
                if (alienAttackCard.IsAlienAttack(alien))
                {
                    //player.TakeDamage(alienAttackCard.Damage);
                }
            }
        }
    }

    private void DealDamageToAliensByFire()
    {
        foreach (var room in rooms.Where(r => r.Value.IsBurning))
        {
            foreach (var alien in room.Value.Creatures.OfType<Alien>())
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
        var token = alienTokenBag.TakeToken();

        switch (token.Type)
        {
            case AlienType.Larva:
                alienTokenBag.TryPutToken(AlienType.Adult);
                break;
            case AlienType.Creeper:
                alienTokenBag.TryPutToken(AlienType.Breeder);
                break;
            case AlienType.Adult:
            case AlienType.Breeder:
                alienTokenBag.PutToken(token);
                DoNoiseCheckForAllPlayers();
                break;
            case AlienType.Queen:
                ProcessQueenToken(token);
                break;
            case AlienType.Empty:
                alienTokenBag.TryPutToken(AlienType.Adult);
                alienTokenBag.PutToken(token);
                break;
        }
    }

    private void ProcessQueenToken(AlienToken token)
    {
        var nest = rooms.FirstOrDefault(); // todo where roomType = Nest;
        if (nest.Value.Creatures.OfType<Player>().Any())
        {
            alienTokenBag.PutToken(token);
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