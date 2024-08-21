// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.Tests.WebSockets.Extensions
{
    using System.Collections.Generic;
    using UniNetty.Codecs.Http.WebSockets;
    using UniNetty.Codecs.Http.WebSockets.Extensions;
    using UniNetty.Transport.Channels;

    static class WebSocketExtensionTestUtil
    {
        public static IHttpRequest NewUpgradeRequest(string ext)
        {
            var req = new DefaultHttpRequest(
                HttpVersion.Http11,
                HttpMethod.Get,
                "/chat");

            req.Headers.Set(HttpHeaderNames.Host, "server.example.com");
            req.Headers.Set(HttpHeaderNames.Upgrade, HttpHeaderValues.Websocket.ToString().ToLower());
            req.Headers.Set(HttpHeaderNames.Connection, "Upgrade");
            req.Headers.Set(HttpHeaderNames.Origin, "http://example.com");
            if (ext != null)
            {
                req.Headers.Set(HttpHeaderNames.SecWebsocketExtensions, ext);
            }

            return req;
        }

        public static IHttpResponse NewUpgradeResponse(string ext)
        {
            var res = new DefaultHttpResponse(
                HttpVersion.Http11,
                HttpResponseStatus.SwitchingProtocols);

            res.Headers.Set(HttpHeaderNames.Host, "server.example.com");
            res.Headers.Set(HttpHeaderNames.Upgrade, HttpHeaderValues.Websocket.ToString().ToLower());
            res.Headers.Set(HttpHeaderNames.Connection, "Upgrade");
            res.Headers.Set(HttpHeaderNames.Origin, "http://example.com");
            if (ext != null)
            {
                res.Headers.Set(HttpHeaderNames.SecWebsocketExtensions, ext);
            }

            return res;
        }

        internal class DummyEncoder : WebSocketExtensionEncoder
        {
            public override void Encode(IChannelHandlerContext ctx, WebSocketFrame msg, List<object> ouput)
            {
                // unused
            }
        }

        internal class DummyDecoder : WebSocketExtensionDecoder
        {
            public override void Decode(IChannelHandlerContext ctx, WebSocketFrame msg, List<object> output)
            {
                // unused
            }
        }

        internal class Dummy2Encoder : WebSocketExtensionEncoder
        {
            public override void Encode(IChannelHandlerContext ctx, WebSocketFrame msg, List<object> ouput)
            {
                // unused
            }
        }

        internal class Dummy2Decoder : WebSocketExtensionDecoder
        {
            public override void Decode(IChannelHandlerContext ctx, WebSocketFrame msg, List<object> output)
            {
                // unused
            }
        }
    }
}
