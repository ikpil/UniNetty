// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Common.Internal
{
    public abstract class AbstractQueue<T> : IQueue<T>
    {
        public abstract bool TryEnqueue(T item);

        public abstract bool TryDequeue(out T item);

        public abstract bool TryPeek(out T item);

        public abstract int Count { get; }

        public abstract bool IsEmpty { get; }

        public abstract void Clear();
    }
}