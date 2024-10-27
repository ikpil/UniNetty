// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using UniNetty.Common.Internal.Logging;

namespace UniNetty.Examples.SecureChat.Client
{
    using System;
    using UniNetty.Transport.Channels;

    public class SecureChatClientHandler : SimpleChannelInboundHandler<string>
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<SecureChatClientHandler>();

        protected override void ChannelRead0(IChannelHandlerContext contex, string msg) => Logger.Info(msg);

        public override void ExceptionCaught(IChannelHandlerContext contex, Exception e)
        {
            Logger.Info($"{DateTime.Now.Millisecond}");
            Logger.Info(e.StackTrace);
            contex.CloseAsync();
        }
    }
}