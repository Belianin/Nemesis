using System;
using System.Collections.Generic;
using System.Linq;
using Nemesis.Common;

namespace Nemesis.Aliens;

public class AlienTokenBag
{
    private readonly List<AlienToken> tokensInBag = new();
    private readonly List<AlienToken> tokensSupply;
    
    public AlienTokenBag(IEnumerable<AlienToken> tokens)
    {
        tokensSupply = tokens.ToList();
    }

    public AlienToken TakeToken()
    {
        if (tokensSupply.Count == 0)
            throw new Exception("No tokens left");
        
        var tokenToTake = tokensInBag.Choose();
        
        tokensInBag.Remove(tokenToTake);
        tokensSupply.Add(tokenToTake);

        return tokenToTake;
    }

    public void TryPutToken(AlienTokenType tokenType)
    {
        if (!TryFindMatchingTokenInSupply(t => t.Type == tokenType, out var tokenToPut))
            return;
        
        tokensSupply.Remove(tokenToPut);
        tokensInBag.Add(tokenToPut);
    }
    
    public void PutToken(AlienToken token)
    {
        if (!TryFindMatchingTokenInSupply(t => t.Equals(token), out var tokenToPut))
            return;
        
        tokensSupply.Remove(tokenToPut);
        tokensInBag.Add(tokenToPut);
    }

    private bool TryFindMatchingTokenInSupply(Func<AlienToken, bool> selector, out AlienToken token)
    {
        token = default;
        
        var matchingTokens = tokensSupply.Where(selector).ToArray();
        
        if (matchingTokens.Length == 0)
            return false;

        token = matchingTokens.Choose();

        return true;
    }
}