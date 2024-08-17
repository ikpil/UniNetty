﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Buffers.Tests
{
    using System;
    using UniNetty.Common;

    public class AdvancedLeakAwareByteBufferTests : SimpleLeakAwareByteBufferTests
    {
        protected override Type ByteBufferType => typeof(AdvancedLeakAwareByteBuffer);

        protected override IByteBuffer Wrap(IByteBuffer buffer, IResourceLeakTracker tracker) => new AdvancedLeakAwareByteBuffer(buffer, tracker);
    }
}
