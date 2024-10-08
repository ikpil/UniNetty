// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Buffers
{
    using System.Collections.Generic;

    public interface IPoolChunkListMetric : IEnumerable<IPoolChunkMetric>
    {
        /// Return the minimum usage of the chunk list before which chunks are promoted to the previous list.
        int MinUsage { get; }

        /// Return the maximum usage of the chunk list after which chunks are promoted to the next list.
        int MaxUsage { get; }
    }
}