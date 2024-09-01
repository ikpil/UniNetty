// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using UniNetty.Common.Internal.Logging;

namespace UniNetty.Examples.Echo.Client
{
    using System;
    using System.Text;
    using UniNetty.Buffers;
    using UniNetty.Transport.Channels;

    public class EchoClientHandler : ChannelHandlerAdapter
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<EchoClientHandler>();

        private readonly IByteBuffer initialMessage;
        private int _size;

        public EchoClientHandler(int size)
        {
            _size = size;
            this.initialMessage = Unpooled.Buffer(size);
            byte[] messageBytes = Encoding.UTF8.GetBytes("Hello world");
            this.initialMessage.WriteBytes(messageBytes);
        }

        public override void ChannelActive(IChannelHandlerContext context) => context.WriteAndFlushAsync(this.initialMessage);

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var byteBuffer = message as IByteBuffer;
            if (byteBuffer != null)
            {
                Logger.Info("Received from server: " + byteBuffer.ToString(Encoding.UTF8));
            }

            context.WriteAsync(message);
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Logger.Info("Exception: " + exception);
            context.CloseAsync();
        }
    }
}