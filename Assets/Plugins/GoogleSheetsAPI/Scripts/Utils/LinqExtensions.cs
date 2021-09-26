﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace GoogleSheetsAPI.Utils
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }

        // Return the first item when the list is of length one and otherwise returns default
        public static TSource OnlyOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            Assert.IsNotNull(source);

            return source.Count() > 1 ? default(TSource) : source.FirstOrDefault();
        }

        // These are more efficient than Count() in cases where the size of the collection is not known
        public static bool HasAtLeast<T>(this IEnumerable<T> enumerable, int amount)
        {
            return enumerable.Take(amount).Count() == amount;
        }

        public static bool HasMoreThan<T>(this IEnumerable<T> enumerable, int amount)
        {
            return enumerable.HasAtLeast(amount + 1);
        }

        public static bool HasLessThan<T>(this IEnumerable<T> enumerable, int amount)
        {
            return enumerable.HasAtMost(amount - 1);
        }

        public static bool HasAtMost<T>(this IEnumerable<T> enumerable, int amount)
        {
            return enumerable.Take(amount + 1).Count() <= amount;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }

        public static IEnumerable<T> GetDuplicates<T>(this IEnumerable<T> list)
        {
            return list.GroupBy(x => x).Where(x => x.Skip(1).Any()).Select(x => x.Key);
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> list, T item)
        {
            return Except<>(list, item.Yield());
        }

        public static bool ContainsItem<T>(this IEnumerable<T> list, T value)
        {
            // Use object.Equals to support null values
            return list.Where(x => object.Equals(x, value)).Any();
        }
    }
}