// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Buffers
{
    public interface IPoolSubpageMetric
    {
        /// Return the number of maximal elements that can be allocated out of the sub-page.
        int MaxNumElements { get; }

        /// Return the number of available elements to be allocated.
        int NumAvailable { get; }

        /// Return the size (in bytes) of the elements that will be allocated.
        int ElementSize { get; }

        /// Return the size (in bytes) of this page.
        int PageSize { get; }
    }
}