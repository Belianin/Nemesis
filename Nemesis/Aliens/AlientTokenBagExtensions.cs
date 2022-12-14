namespace Nemesis.Aliens;

public static class AlienTokenBagExtensions
{
    public static void TryPutTokens(this AlienTokenBag bag, AlienType type, int count)
    {
        for (var i = 0; i < count; i++)
            bag.TryPutToken(type);
    }
}