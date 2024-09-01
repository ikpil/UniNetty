// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using UniNetty.Common.Internal.Logging;

namespace UniNetty.Examples.Discard.Server
{

    using System;
    using UniNetty.Transport.Channels;

    public class DiscardServerHandler : SimpleChannelInboundHandler<object>
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<DiscardServerHandler>();

        protected override void ChannelRead0(IChannelHandlerContext context, object message)
        {
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e)
        {
            Logger.Info("{0}", e.ToString());
            ctx.CloseAsync();
        }
    }
}