using System.Collections.Generic;

namespace UniNetty.Common.Collections.Immutable
{
    public static class UniImmutableArrays
    {
        public static UniImmutableArray<T> CreateRange<T>(params T[] items)
        {
            return new UniImmutableArray<T>(items);
            
        }
        public static UniImmutableArray<T> ToUniImmutableArray<T>(this ICollection<T> collection)
        {
            return new UniImmutableArray<T>(collection);
        }
    }
}