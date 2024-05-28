using System.Collections.Generic;

namespace Line_Code__5_2_
{
    public static class EnumerableExtensions
    {
        public static string ToStringJoin<T>(this IEnumerable<T> enumerable)
        {
            return string.Join(string.Empty, enumerable);
        }
    }
}