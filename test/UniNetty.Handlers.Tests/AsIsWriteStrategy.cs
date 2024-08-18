// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Handlers.Tests
{
    using System;
    using System.Threading.Tasks;
    using UniNetty.Buffers;
    using UniNetty.Common.Utilities;
    using UniNetty.Transport.Channels.Embedded;

    class AsIsWriteStrategy : IWriteStrategy
    {
        public Task WriteToChannelAsync(IEmbeddedChannel channel, ArraySegment<byte> input)
        {
            channel.WriteInbound(Unpooled.WrappedBuffer(input.Array, input.Offset, input.Count));
            return TaskEx.Completed;
        }

        public override string ToString() => "as-is";
    }
}