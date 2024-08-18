// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Buffers
{
    public interface IPoolChunkMetric
    {
        /// Return the percentage of the current usage of the chunk.
        int Usage { get; }

        /// Return the size of the chunk in bytes, this is the maximum of bytes that can be served out of the chunk.
        int ChunkSize { get; }

        /// Return the number of free bytes in the chunk.
        int FreeBytes { get; }
    }
}