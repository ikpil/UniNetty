// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using UniNetty.Common.Internal.Logging;

namespace UniNetty.Examples.WebSockets.Client
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using UniNetty.Codecs.Http;
    using UniNetty.Codecs.Http.WebSockets;
    using UniNetty.Common.Concurrency;
    using UniNetty.Common.Utilities;
    using UniNetty.Transport.Channels;
    using TaskCompletionSource = UniNetty.Common.Concurrency.TaskCompletionSource;

    public class WebSocketClientHandler : SimpleChannelInboundHandler<object>
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<WebSocketClientHandler>();

        readonly WebSocketClientHandshaker handshaker;
        readonly TaskCompletionSource completionSource;

        public WebSocketClientHandler(WebSocketClientHandshaker handshaker)
        {
            this.handshaker = handshaker;
            this.completionSource = new TaskCompletionSource();
        }

        public Task HandshakeCompletion => this.completionSource.Task;

        public override void ChannelActive(IChannelHandlerContext ctx) => 
            this.handshaker.HandshakeAsync(ctx.Channel).LinkOutcome(this.completionSource);

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            Logger.Info("WebSocket Client disconnected!");
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, object msg)
        {
            IChannel ch = ctx.Channel;
            if (!this.handshaker.IsHandshakeComplete)
            {
                try
                {
                    this.handshaker.FinishHandshake(ch, (IFullHttpResponse)msg);
                    Logger.Info("WebSocket Client connected!");
                    this.completionSource.TryComplete();
                }
                catch (WebSocketHandshakeException e)
                {
                    Logger.Info("WebSocket Client failed to connect");
                    this.completionSource.TrySetException(e);
                }

                return;
            }


            if (msg is IFullHttpResponse response)
            {
                throw new InvalidOperationException(
                    $"Unexpected FullHttpResponse (getStatus={response.Status}, content={response.Content.ToString(Encoding.UTF8)})");
            }

            if (msg is TextWebSocketFrame textFrame)
            {
                Logger.Info($"WebSocket Client received message: {textFrame.Text()}");
            }
            else if (msg is PongWebSocketFrame)
            {
                Logger.Info("WebSocket Client received pong");
            }
            else if (msg is CloseWebSocketFrame)
            {
                Logger.Info("WebSocket Client received closing");
                ch.CloseAsync();
            }
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
        {
            Logger.Info("Exception: " + exception);
            this.completionSource.TrySetException(exception);
            ctx.CloseAsync();
        }
    }
}
