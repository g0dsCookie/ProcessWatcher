using System.Collections.Generic;

namespace CookieProjects.ProcessWatcher.Extensions
{
    internal static class DictionaryExtension
    {
        internal static (IEnumerable<KeyValuePair<K, V>> added, IEnumerable<KeyValuePair<K, V>> deleted)
            GetFullDifferenceTo<K, V>(this IDictionary<K, V> left, IDictionary<K, V> right) =>
            (added: left.GetDifferenceTo(right), deleted: right.GetDifferenceTo(left));

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
