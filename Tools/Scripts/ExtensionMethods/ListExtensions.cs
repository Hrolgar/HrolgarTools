using System;
using System.Collections.Generic;
using System.Linq;
using SysRandom = System.Random;
using URandom = UnityEngine.Random;

namespace hrolgarUllr.ExtensionMethods
{
    public static class ListExtensions
    {
        /// <summary>
        /// Returns a random item from a generic list of elements of type T.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="myList">The list from which a random item should be returned.</param>
        /// <returns>A random item from the list.</returns>
        public static T GetRandomItem<T>(this IList<T> myList) => myList[URandom.Range(0, myList.Count)];
        
        /// <summary>
        /// Sorts a generic list or array of elements of type T which implements IComparable&lt;T&gt;.
        /// </summary>
        /// <typeparam name="T">The element type that implements IComparable&lt;T&gt;.</typeparam>
        /// <param name="list">The list or array to sort.</param>
        /// <param name="ascending">Sort order, ascending if true (default), descending if false.</param>
        /// <returns>The sorted list.</returns>
        public static IList<T> MySort<T>(this IEnumerable<T> list, bool ascending = true) where T : IComparable<T>
        {
            var sortedList = list.ToList();
            if (ascending)
                sortedList.Sort((a, b) => a.CompareTo(b));
            else
                sortedList.Sort((a, b) => b.CompareTo(a));
            return sortedList;
        }
        
        /// <summary>
        /// Shuffles the elements in a generic list of type T in place using the Fisher-Yates shuffle algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to be shuffled.</param>
        /// <returns>The shuffled list.</returns>
        public static IEnumerable<T> Shuffle<T>(this IList<T> list)
        {
            var rng =  new SysRandom();
            var n = list.Count;
            while (n > 1) 
            {
                n--;
                var k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
            return list;
        }
        
        /// <summary>
        /// Returns a list containing the last 'count' elements from the given list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The input list to retrieve elements from.</param>
        /// <param name="count">The number of elements to retrieve, starting from the end of the list.</param>
        /// <returns>A list of the last 'count' elements from the input list.</returns>
        public static IList<T> TakeLast<T>(this IList<T> list, int count) => list.Skip(Math.Max(0, list.Count - count)).ToList();
        
        /// <summary>
        /// Interleaves elements of two lists into a single list of type T.
        /// </summary>
        /// <typeparam name="T">The type of elements in the lists, which can be any type.</typeparam>
        /// <param name="list1">The first list to interleave with the second list.</param>
        /// <param name="list2">The second list to interleave with the first list.</param>
        /// <returns>The interleaved list of elements from both input lists.</returns>
        public static IEnumerable<T> InterLeave<T>(this IList<T> list1, IList<T> list2)
        {
            var interleavedList = new List<T>();
            int i = 0, j = 0;
            while (i < list1.Count || j < list2.Count)
            {
                if (i < list1.Count)
                    interleavedList.Add(list1[i++]);
                if (j < list2.Count)
                    interleavedList.Add(list2[j++]);
            }
            return interleavedList;
        }
        
        /// <summary>
        /// Returns a list of distinct elements of the given enumerable list, based on the result of a key selector function.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <typeparam name="TKey">The type of the result of the key selector function.</typeparam>
        /// <param name="list">The enumerable list to return the distinct elements from.</param>
        /// <param name="keySelector">A function to extract a key from each element in the list.</param>
        /// <returns>A list of distinct elements of the original list, based on the result of the key selector function.</returns>
        public static IList<T> DistinctBy<T, TKey>(this IEnumerable<T> list, Func<T, TKey> keySelector)
            => list.GroupBy(keySelector).Select(g => g.First()).ToList();
    }
}
