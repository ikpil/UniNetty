// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.Tests.WebSockets
{
    using UniNetty.Codecs.Http.WebSockets;
    using UniNetty.Transport.Channels;
    using Moq;
    using Xunit;

    public sealed class WebSocket08FrameDecoderTest
    {
        [Fact]
        public void ChannelInactive()
        {
            var decoder = new WebSocket08FrameDecoder(true, true, 65535, false);
            var ctx = new Mock<IChannelHandlerContext>(MockBehavior.Strict);
            ctx.Setup(x => x.FireChannelInactive()).Returns(ctx.Object);

            decoder.ChannelInactive(ctx.Object);
            ctx.Verify(x => x.FireChannelInactive(), Times.Once);
        }
    }
}
