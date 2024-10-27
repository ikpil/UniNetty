using System.Collections.Generic;

namespace UniNetty.Common.Collections.Immutable
{
    public static class UniImmutableHashSets
    {
        public static UniImmutableHashSet<T> ToUniImmutableHashSet<T>(this ISet<T> collection)
        {
            return new UniImmutableHashSet<T>(collection);
        }
    }
}