// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using UniNetty.Common.Internal.Logging;

namespace UniNetty.Examples.QuoteOfTheMoment.Client
{
    using System;
    using System.Text;
    using UniNetty.Transport.Channels;
    using UniNetty.Transport.Channels.Sockets;

    public class QuoteOfTheMomentClientHandler : SimpleChannelInboundHandler<DatagramPacket>
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<QuoteOfTheMomentClientHandler>();

        protected override void ChannelRead0(IChannelHandlerContext ctx, DatagramPacket packet)
        {
            Logger.Info($"Client Received => {packet}");

            if (!packet.Content.IsReadable())
            {
                return;
            }

            string message = packet.Content.ToString(Encoding.UTF8);
            if (!message.StartsWith("QOTM: "))
            {
                return;
            }

            Logger.Info($"Quote of the Moment: {message.Substring(6)}");
            ctx.CloseAsync();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Logger.Info("Exception: " + exception);
            context.CloseAsync();
        }
    }
}