// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using UniNetty.Common.Internal.Logging;

namespace UniNetty.Examples.Factorial.Server
{
    using System;
    using System.Numerics;
    using UniNetty.Transport.Channels;

    public class FactorialServerHandler : SimpleChannelInboundHandler<BigInteger>
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<FactorialServerHandler>();

        BigInteger lastMultiplier = new BigInteger(1);
        BigInteger factorial = new BigInteger(1);

        protected override void ChannelRead0(IChannelHandlerContext ctx, BigInteger msg)
        {
            this.lastMultiplier = msg;
            this.factorial *= msg;
            ctx.WriteAndFlushAsync(this.factorial);
        }

        public override void ChannelInactive(IChannelHandlerContext ctx) => Logger.Info("UniNetty.Examples.Factorial of {0} is: {1}", this.lastMultiplier, this.factorial);

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e) => ctx.CloseAsync();
    }
}