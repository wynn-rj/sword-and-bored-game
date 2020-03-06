using System;
using System.Collections.Generic;

namespace SwordAndBored.Utilities
{
    static class ClassExtenders
    {
        public static IEnumerable<R> EnumerateOverValues<T, R>(this IEnumerable<T> enumerable, Func<T, R> operation)
        {
            foreach (T item in enumerable)
            {
                yield return operation(item);
            }
        }
    }
}
