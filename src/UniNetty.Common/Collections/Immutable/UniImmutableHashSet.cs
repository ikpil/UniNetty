using System;
using System.Collections;
using System.Collections.Generic;

namespace UniNetty.Common.Collections.Immutable
{
    public class UniImmutableHashSet<T> : ISet<T>, ICollection<T>, ICollection
    {
        public static readonly UniImmutableHashSet<T> Empty = new UniImmutableHashSet<T>(Array.Empty<T>());
        
        private readonly HashSet<T> _items;

        public UniImmutableHashSet(IEnumerable<T> collection)
        {
            _items = new HashSet<T>(collection);
            _items.TrimExcess();
        }

        public int Count => _items.Count;
        public bool IsSynchronized => true;
        public object SyncRoot => this;

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region ISet<T> Members

        bool ISet<T>.Add(T item)
        {
            throw new NotSupportedException("immutable hashset");
        }

        void ISet<T>.ExceptWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("immutable hashset");
        }

        void ISet<T>.IntersectWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("immutable hashset");
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return _items.SetEquals(other);
        }

        void ISet<T>.SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("immutable hashset");
        }

        void ISet<T>.UnionWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("immutable hashset");
        }

        #endregion

        #region ICollection<T> members

        bool ICollection<T>.IsReadOnly => true;

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException("immutable hashset");
        }

        void ICollection<T>.Clear()
        {
            throw new NotSupportedException("immutable hashset");
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException("immutable hashset");
        }

        #endregion

        #region ICollection Methods

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            foreach (T item in this)
            {
                array.SetValue(item, arrayIndex++);
            }
        }

        #endregion


        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return _items.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return _items.IsProperSubsetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return _items.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return _items.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return _items.Overlaps(other);
        }
    }
}