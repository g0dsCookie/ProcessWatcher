using System.Collections.Generic;

namespace CookieProjects.ProcessWatcher.Extensions
{
    internal static class DictionaryExtension
    {
        internal static (IEnumerable<KeyValuePair<K, V>> deleted, IEnumerable<KeyValuePair<K, V>> added)
            GetFullDifferenceTo<K, V>(this IDictionary<K, V> left, IDictionary<K, V> right) =>
            (deleted: left.GetDifferenceTo(right), added: right.GetDifferenceTo(left));

        internal static IEnumerable<KeyValuePair<K, V>> GetDifferenceTo<K, V>(this IDictionary<K, V> left, IDictionary<K, V> right)
        {
            foreach (var kvp in left)
            {
                if (!right.ContainsKey(kvp.Key))
                    yield return kvp;
            }
        }
    }
}
