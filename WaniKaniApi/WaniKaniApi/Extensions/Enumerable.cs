using System.Collections.Generic;

namespace WaniKaniApi.Extensions
{
    public static class Enumerable
    {
        public static string Stringify<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null 
                ? ""
                : string.Join(",", enumerable);
        }
    }
}