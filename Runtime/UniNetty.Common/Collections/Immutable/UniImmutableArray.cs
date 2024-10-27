using System;
using System.Collections;
using System.Collections.Generic;

namespace UniNetty.Common.Collections.Immutable
{
    public readonly struct UniImmutableArray<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        public static readonly UniImmutableArray<T> Empty = new UniImmutableArray<T>(Array.Empty<T>());

        private readonly List<T> _items;

        public UniImmutableArray(ICollection<T> items)
        {
            _items = new List<T>(items);
        }

        public int Count => _items.Count;
        public bool IsReadOnly => true;

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            throw new NotSupportedException("immutable array");
        }

        public void Clear()
        {
            throw new NotSupportedException("immutable array");
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException("immutable array");
        }

        public int IndexOf(T item)
        {
            return _items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            throw new NotSupportedException("immutable array");
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException("immutable array");
        }

        public T this[int index]
        {
            get => _items[index];
            set => throw new NotSupportedException("immutable array");
        }
    }
}