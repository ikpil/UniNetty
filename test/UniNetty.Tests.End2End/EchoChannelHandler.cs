// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Tests.End2End
{
    using System;
    using UniNetty.Transport.Channels;

    class EchoChannelHandler : ChannelHandlerAdapter
    {
        public override void ChannelRead(IChannelHandlerContext context, object message) => context.Channel.WriteAsync(message);

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Channel.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
        }
    }
}