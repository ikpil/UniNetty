// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.Tests.WebSockets
{
    using UniNetty.Buffers;
    using UniNetty.Codecs.Http.WebSockets;
    using UniNetty.Transport.Channels.Embedded;
    using Xunit;

    public sealed class WebSocket00FrameEncoderTest
    {
        // Test for https://github.com/netty/netty/issues/2768
        [Fact]
        public void MultipleWebSocketCloseFrames()
        {
            var channel = new EmbeddedChannel(new WebSocket00FrameEncoder());
            Assert.True(channel.WriteOutbound(new CloseWebSocketFrame()));
            Assert.True(channel.WriteOutbound(new CloseWebSocketFrame()));
            Assert.True(channel.Finish());
            AssertCloseWebSocketFrame(channel);
            AssertCloseWebSocketFrame(channel);
            Assert.Null(channel.ReadOutbound<object>());
        }

        static void AssertCloseWebSocketFrame(EmbeddedChannel channel)
        {
            var buf = channel.ReadOutbound<IByteBuffer>();
            Assert.Equal(2, buf.ReadableBytes);
            Assert.Equal((byte)0xFF, buf.ReadByte());
            Assert.Equal((byte)0x00, buf.ReadByte());
            buf.Release();
        }
    }
}
