// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Buffers.Tests
{
    using Xunit;

    public class UnsafeDirectByteBufferTest : AbstractByteBufferTests
    {
        protected override IByteBuffer NewBuffer(int length, int maxCapacity)
        {
            IByteBuffer buffer = this.NewDirectBuffer(length, maxCapacity);
            Assert.Equal(0, buffer.WriterIndex);
            return buffer;
        }

        protected IByteBuffer NewDirectBuffer(int length, int maxCapacity) =>
            new UnpooledUnsafeDirectByteBuffer(UnpooledByteBufferAllocator.Default, length, maxCapacity);
    }
}
