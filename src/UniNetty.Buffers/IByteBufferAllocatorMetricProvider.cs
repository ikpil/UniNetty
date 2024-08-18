// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Buffers
{
    public interface IByteBufferAllocatorMetricProvider
    {
        /// <summary>
        /// Returns a <see cref="IByteBufferAllocatorMetric"/> for a <see cref="IByteBufferAllocator"/>
        /// </summary>
        IByteBufferAllocatorMetric Metric { get; }
    }
}
