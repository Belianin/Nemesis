using System.Collections.Generic;
using System.Linq;
using Nemesis.Common;

namespace Nemesis.Cards;

public class Deck<T>
{
    private readonly List<T> discarded;
    private Stack<T> deck;
    
    public Deck(IEnumerable<T> cards)
    {
        deck = new Stack<T>(cards.Shuffle());
        discarded = new List<T>();
    }

    public void ShuffleDiscarded()
    {
        deck = new Stack<T>(deck.Concat(discarded).Shuffle());
        discarded.Clear();
    }

    public T TakeCard()
    {
        if (deck.Count == 0)
            ShuffleDiscarded();

        var card = deck.Pop();
        discarded.Add(card);

        return card;
    }
}