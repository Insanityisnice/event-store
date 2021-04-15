using System.Collections.Generic;

namespace System.Linq
{
    internal static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null) return true;
            return enumerable.Any() == false;
        }
    }
}