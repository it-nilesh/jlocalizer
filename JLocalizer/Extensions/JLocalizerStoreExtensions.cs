using System.Collections.Generic;

namespace JLocalizer
{
    public static class JLocalizerStoreExtensions
    {
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out TValue obj) ? obj : default;
        }

        public static TValue Get<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out TValue obj) ? obj : default;
        }
    }
}
