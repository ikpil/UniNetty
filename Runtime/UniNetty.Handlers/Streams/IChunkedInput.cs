// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Handlers.Streams
{
    using UniNetty.Buffers;

    public interface IChunkedInput<out T>
    {
        bool IsEndOfInput { get; }

        void Close();

        T ReadChunk(IByteBufferAllocator allocator);

        long Length { get; }

        long Progress { get; }
    }
}
