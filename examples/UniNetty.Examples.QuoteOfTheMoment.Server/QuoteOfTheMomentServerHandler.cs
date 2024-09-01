// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using UniNetty.Common.Internal.Logging;

namespace UniNetty.Examples.QuoteOfTheMoment.Server
{
    using System;
    using System.Text;
    using UniNetty.Buffers;
    using UniNetty.Transport.Channels;
    using UniNetty.Transport.Channels.Sockets;

    public class QuoteOfTheMomentServerHandler : SimpleChannelInboundHandler<DatagramPacket>
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<QuoteOfTheMomentServerHandler>();

        static readonly Random Random = new Random();

        // Quotes from Mohandas K. Gandhi:
        static readonly string[] Quotes =
        {
            "Where there is love there is life.",
            "First they ignore you, then they laugh at you, then they fight you, then you win.",
            "Be the change you want to see in the world.",
            "The weak can never forgive. Forgiveness is the attribute of the strong.",
        };

        static string NextQuote()
        {
            int quoteId = Random.Next(Quotes.Length);
            return Quotes[quoteId];
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, DatagramPacket packet)
        {
            Logger.Info($"Server Received => {packet}");

            if (!packet.Content.IsReadable())
            {
                return;
            }

            string message = packet.Content.ToString(Encoding.UTF8);
            if (message != "QOTM?")
            {
                return;
            }

            byte[] bytes = Encoding.UTF8.GetBytes("QOTM: " + NextQuote());
            IByteBuffer buffer = Unpooled.WrappedBuffer(bytes);
            ctx.WriteAsync(new DatagramPacket(buffer, packet.Sender));
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Logger.Info("Exception: " + exception);
            context.CloseAsync();
        }
    }
}