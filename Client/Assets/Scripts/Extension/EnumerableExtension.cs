using System;
using System.Collections.Generic;
using System.Linq;

namespace Extension
{
    public static class EnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            collection.Select((x, i) => new { index = i, data = x }).ToList().ForEach(x => action(x.data, x.index));
        }
    }
}
