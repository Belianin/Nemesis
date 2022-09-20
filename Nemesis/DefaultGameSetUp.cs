using System.Collections.Generic;
using System.Linq;
using Nemesis.Aliens;

namespace Nemesis;

public static class DefaultGameSetUp
{
    private static AlienTokenBag GetFilledAlienTokenBag(int playersCount)
    {
        var bag = GetEmptyAlienTokenBag();
        
        bag.TryPutToken(AlienTokenType.Empty);
        bag.TryPutTokens(AlienTokenType.Larva, 4);
        bag.TryPutToken(AlienTokenType.Creeper);
        bag.TryPutTokens(AlienTokenType.Adult, 3 + playersCount);

        return bag;
    }
    
    private static AlienTokenBag GetEmptyAlienTokenBag()
    {
        var tokens = new List<AlienToken>();

        void AddTokens(AlienTokenType tokenType, int attention, int count)
        {
            tokens.AddRange(Enumerable.Repeat(new AlienToken(tokenType, attention), count));
        }
        
        AddTokens(AlienTokenType.Larva, 1, 8);
        AddTokens(AlienTokenType.Creeper, 1, 3);
        AddTokens(AlienTokenType.Adult, 4, 3);
        AddTokens(AlienTokenType.Adult, 2, 4);
        AddTokens(AlienTokenType.Adult, 3, 5);
        
        tokens.AddRange(new []
        {
            new AlienToken(AlienTokenType.Empty, 1),
            new AlienToken(AlienTokenType.Breeder, 3),
            new AlienToken(AlienTokenType.Breeder, 4),
            new AlienToken(AlienTokenType.Queen, 4)
        });

        return new AlienTokenBag(tokens);
    }
}