using System;
using System.Collections;
using System.Collections.Generic;

namespace UniNetty.Common.Collections.Immutable
{
    public class UniImmutableDictionary<TKey, TValue> :
        ICollection<KeyValuePair<TKey, TValue>>,
        IEnumerable<KeyValuePair<TKey, TValue>>,
        IEnumerable,
        IDictionary<TKey, TValue>,
        IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
        IReadOnlyDictionary<TKey, TValue>
    {
        public static readonly UniImmutableDictionary<TKey, TValue> Empty = new UniImmutableDictionary<TKey, TValue>(new Dictionary<TKey, TValue>());

        private readonly Dictionary<TKey, TValue> _items;

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => _items.Keys;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => _items.Values;

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => _items.Keys;

        ICollection<TValue> IDictionary<TKey, TValue>.Values => _items.Values;

        int IReadOnlyCollection<KeyValuePair<TKey, TValue>>.Count => _items.Count;

        int ICollection<KeyValuePair<TKey, TValue>>.Count => _items.Count;

        public bool IsReadOnly => true;

        public UniImmutableDictionary(IDictionary<TKey, TValue> items)
        {
            _items = new Dictionary<TKey, TValue>(items);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException("Immutable Dictionary");
        }

        public void Clear()
        {
            throw new NotSupportedException("Immutable Dictionary");
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _items.ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            foreach (var knp in _items)
            {
                array[arrayIndex++] = knp;
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException("Immutable Dictionary");
        }


        public void Add(TKey key, TValue value)
        {
            throw new NotSupportedException("Immutable Dictionary");
        }

        bool IDictionary<TKey, TValue>.ContainsKey(TKey key)
        {
            return _items.ContainsKey(key);
        }

        bool IReadOnlyDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
            return _items.TryGetValue(key, out value);
        }

        public bool Remove(TKey key)
        {
            throw new NotSupportedException("Immutable Dictionary");
        }

        bool IReadOnlyDictionary<TKey, TValue>.ContainsKey(TKey key)
        {
            return _items.ContainsKey(key);
        }

        bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
            return _items.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get => _items[key];
            set => throw new NotSupportedException("Immutable Dictionary");
        }
    }
}