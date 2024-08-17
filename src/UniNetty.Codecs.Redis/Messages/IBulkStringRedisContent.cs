﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Redis.Messages
{
    using UniNetty.Buffers;

    public interface IBulkStringRedisContent : IRedisMessage, IByteBufferHolder
    {
    }
}