using System;
using System.Collections.Generic;
using System.Linq;

namespace Nemesis.Common;

public static class EnumerableExtensions
{
    public static T Choose<T>(this IList<T> elements)
    {
        var random = new Random();

        return elements[random.Next(elements.Count)];
    }
    
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> elements)
    {
        var random = new Random();

        return elements.OrderBy(x => random.Next());
    }
}