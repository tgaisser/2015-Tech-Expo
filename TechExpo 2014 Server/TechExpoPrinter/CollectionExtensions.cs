#region

using System.Collections.Generic;

#endregion

namespace TechExpoPrinter
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> instance, IEnumerable<T> collection)
        {
            foreach (var obj in collection)
            {
                instance.Add(obj);
            }
        }
    }
}