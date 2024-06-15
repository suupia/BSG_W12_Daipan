#nullable enable
using System;
using System.Collections.Generic;

namespace Daipan.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<TAccumulate> Scan<TSource, TAccumulate>(
        this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
    {
        yield return seed;
        TAccumulate accumulation = seed;
        foreach (var item in source)
        {
            accumulation = func(accumulation, item);
            yield return accumulation;
        }
    }
}