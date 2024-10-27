// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Buffers
{
    public interface IByteBufferAllocatorMetric
    {
        /// <summary>
        /// Returns the number of bytes of heap memory used by a {@link ByteBufAllocator} or {@code -1} if unknown.
        /// </summary>
        long UsedHeapMemory { get; }

        /// <summary>
        ///  Returns the number of bytes of direct memory used by a {@link ByteBufAllocator} or {@code -1} if unknown.
        /// </summary>
        long UsedDirectMemory { get; }
    }
}
