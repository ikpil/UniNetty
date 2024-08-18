// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Buffers.Tests
{
    using Xunit;

    public class RetainedSlicedByteBufferTests : SlicedByteBufferTest
    {
        protected override IByteBuffer NewSlice(IByteBuffer buffer, int offset, int length)
        {
            IByteBuffer slice = buffer.RetainedSlice(offset, length);
            buffer.Release();
            Assert.Equal(buffer.ReferenceCount, slice.ReferenceCount);
            return slice;
        }
    }
}
