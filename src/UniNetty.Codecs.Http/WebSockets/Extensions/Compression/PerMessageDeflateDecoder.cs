// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.WebSockets.Extensions.Compression
{
    using System.Collections.Generic;
    using UniNetty.Transport.Channels;

    class PerMessageDeflateDecoder : DeflateDecoder
    {
        bool compressing;

        public PerMessageDeflateDecoder(bool noContext)
            : base(noContext)
        {
        }

        public override bool AcceptInboundMessage(object msg) => 
            ((msg is TextWebSocketFrame || msg is BinaryWebSocketFrame) 
                && (((WebSocketFrame)msg).Rsv & WebSocketRsv.Rsv1) > 0) 
            || (msg is ContinuationWebSocketFrame && this.compressing);

        protected override int NewRsv(WebSocketFrame msg) => 
            (msg.Rsv & WebSocketRsv.Rsv1) > 0 ? msg.Rsv ^ WebSocketRsv.Rsv1 : msg.Rsv;

        protected override bool AppendFrameTail(WebSocketFrame msg) => msg.IsFinalFragment;

        public override void Decode(IChannelHandlerContext ctx, WebSocketFrame msg, List<object> output)
        {
            base.Decode(ctx, msg, output);

            if (msg.IsFinalFragment)
            {
                this.compressing = false;
            }
            else if (msg is TextWebSocketFrame || msg is BinaryWebSocketFrame)
            {
                this.compressing = true;
            }
        }
    }
}
