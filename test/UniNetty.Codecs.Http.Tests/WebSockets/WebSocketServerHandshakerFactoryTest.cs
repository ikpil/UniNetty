// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.Tests.WebSockets
{
    using UniNetty.Codecs.Http.WebSockets;
    using UniNetty.Common.Utilities;
    using UniNetty.Transport.Channels.Embedded;
    using Xunit;

    public class WebSocketServerHandshakerFactoryTest
    {
        [Fact]
        public void UnsupportedVersion()
        {
            var ch = new EmbeddedChannel();
            WebSocketServerHandshakerFactory.SendUnsupportedVersionResponse(ch);
            ch.RunPendingTasks();
            var response = ch.ReadOutbound<IFullHttpResponse>();
            Assert.NotNull(response);

            Assert.Equal(HttpResponseStatus.UpgradeRequired, response.Status);
            Assert.True(response.Headers.TryGet(HttpHeaderNames.SecWebsocketVersion, out ICharSequence value));
            Assert.Equal(WebSocketVersion.V13.ToHttpHeaderValue(), value);
            Assert.True(HttpUtil.IsContentLengthSet(response));
            Assert.Equal(0, HttpUtil.GetContentLength(response));

            ReferenceCountUtil.Release(response);
            Assert.False(ch.Finish());
        }
    }
}
