// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Common.Internal
{
    public interface IQueue<T>
    {
        bool TryEnqueue(T item);

        bool TryDequeue(out T item);

        bool TryPeek(out T item);

        int Count { get; }

        bool IsEmpty { get; }

        void Clear();
    }
}