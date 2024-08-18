// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.Tests.WebSockets.Extensions.Compression
{
    using System.Collections.Generic;
    using UniNetty.Codecs.Http.WebSockets.Extensions;
    using UniNetty.Codecs.Http.WebSockets.Extensions.Compression;
    using Xunit;

    using static Http.WebSockets.Extensions.Compression.DeflateFrameServerExtensionHandshaker;

    public sealed class DeflateFrameServerExtensionHandshakerTest
    {
        [Fact]
        public void NormalHandshake()
        {
            var handshaker = new DeflateFrameServerExtensionHandshaker();
            IWebSocketServerExtension extension = handshaker.HandshakeExtension(
                new WebSocketExtensionData(DeflateFrameExtension, new Dictionary<string, string>()));

            Assert.NotNull(extension);
            Assert.Equal(WebSocketRsv.Rsv1, extension.Rsv);
            Assert.IsType<PerFrameDeflateDecoder>(extension.NewExtensionDecoder());
            Assert.IsType<PerFrameDeflateEncoder>(extension.NewExtensionEncoder());
        }

        [Fact]
        public void WebkitHandshake()
        {
            var handshaker = new DeflateFrameServerExtensionHandshaker();
            IWebSocketServerExtension extension = handshaker.HandshakeExtension(
                new WebSocketExtensionData(XWebkitDeflateFrameExtension, new Dictionary<string, string>()));

            Assert.NotNull(extension);
            Assert.Equal(WebSocketRsv.Rsv1, extension.Rsv);
            Assert.IsType<PerFrameDeflateDecoder>(extension.NewExtensionDecoder());
            Assert.IsType<PerFrameDeflateEncoder>(extension.NewExtensionEncoder());
        }

        [Fact]
        public void FailedHandshake()
        {
            var handshaker = new DeflateFrameServerExtensionHandshaker();
            var parameters = new Dictionary<string, string>
            {
                { "unknown", "11" }
            };
            IWebSocketServerExtension extension = handshaker.HandshakeExtension(
                new WebSocketExtensionData(DeflateFrameExtension, parameters));

            Assert.Null(extension);
        }
    }
}
