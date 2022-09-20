using System;
using System.Collections.Generic;

namespace Nemesis.Common;

public static class EnumerableExtensions
{
    public static T Choose<T>(this IList<T> elements)
    {
        var random = new Random();

        return elements[random.Next(elements.Count)];
    }
}