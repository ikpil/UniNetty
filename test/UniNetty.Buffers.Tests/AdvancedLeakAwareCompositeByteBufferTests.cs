// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Buffers.Tests
{
    using System;
    using UniNetty.Common;

    public class AdvancedLeakAwareCompositeByteBufferTests : SimpleLeakAwareCompositeByteBufferTests
    {
        protected override IByteBuffer Wrap(CompositeByteBuffer buffer, IResourceLeakTracker tracker) => new AdvancedLeakAwareCompositeByteBuffer(buffer, tracker);

        protected override Type ByteBufferType => typeof(AdvancedLeakAwareByteBuffer);
    }
}
