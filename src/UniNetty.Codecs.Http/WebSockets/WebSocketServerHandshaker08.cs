// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.WebSockets
{
    using System.Text;
    using UniNetty.Common.Utilities;

    public class WebSocketServerHandshaker08 : WebSocketServerHandshaker
    {
        public static readonly string Websocket08AcceptGuid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

        readonly bool allowExtensions;
        readonly bool allowMaskMismatch;

        public WebSocketServerHandshaker08(string webSocketUrl, string subprotocols, bool allowExtensions, int maxFramePayloadLength)
            : this(webSocketUrl, subprotocols, allowExtensions, maxFramePayloadLength, false)
        {
        }

        public WebSocketServerHandshaker08(string webSocketUrl, string subprotocols, bool allowExtensions, int maxFramePayloadLength,
            bool allowMaskMismatch)
            : base(WebSocketVersion.V08, webSocketUrl, subprotocols, maxFramePayloadLength)
        {
            this.allowExtensions = allowExtensions;
            this.allowMaskMismatch = allowMaskMismatch;
        }

        protected override IFullHttpResponse NewHandshakeResponse(IFullHttpRequest req, HttpHeaders headers)
        {
            var res = new DefaultFullHttpResponse(HttpVersion.Http11, HttpResponseStatus.SwitchingProtocols);

            if (headers != null)
            {
                res.Headers.Add(headers);
            }

            if (!req.Headers.TryGet(HttpHeaderNames.SecWebsocketKey, out ICharSequence key) 
                || key == null)
            {
                throw new WebSocketHandshakeException("not a WebSocket request: missing key");
            }
            string acceptSeed = key + Websocket08AcceptGuid;
            byte[] sha1 = WebSocketUtil.Sha1(Encoding.ASCII.GetBytes(acceptSeed));
            string accept = WebSocketUtil.Base64String(sha1);

            if (Logger.DebugEnabled)
            {
                Logger.Debug("WebSocket version 08 server handshake key: {}, response: {}", key, accept);
            }

            res.Headers.Add(HttpHeaderNames.Upgrade, HttpHeaderValues.Websocket);
            res.Headers.Add(HttpHeaderNames.Connection, HttpHeaderValues.Upgrade);
            res.Headers.Add(HttpHeaderNames.SecWebsocketAccept, accept);

            if (req.Headers.TryGet(HttpHeaderNames.SecWebsocketProtocol, out ICharSequence subprotocols) 
                && subprotocols != null)
            {
                string selectedSubprotocol = this.SelectSubprotocol(subprotocols.ToString());
                if (selectedSubprotocol == null)
                {
                    if (Logger.DebugEnabled)
                    {
                        Logger.Debug("Requested subprotocol(s) not supported: {}", subprotocols);
                    }
                }
                else
                {
                    res.Headers.Add(HttpHeaderNames.SecWebsocketProtocol, selectedSubprotocol);
                }
            }
            return res;
        }

        public override IWebSocketFrameDecoder NewWebsocketDecoder() =>  new WebSocket08FrameDecoder(
            true, this.allowExtensions, this.MaxFramePayloadLength, this.allowMaskMismatch);

        public override IWebSocketFrameEncoder NewWebSocketEncoder() => new WebSocket08FrameEncoder(false);
    }
}
