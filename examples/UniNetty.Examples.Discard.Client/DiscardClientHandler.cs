// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace UniNetty.Examples.Discard.Client
{
    using System;
    using UniNetty.Buffers;
    using UniNetty.Transport.Channels;
    using Examples.Common;

    public class DiscardClientHandler : SimpleChannelInboundHandler<object>
    {
        private Random _random;
        IChannelHandlerContext ctx;
        byte[] array;

        public DiscardClientHandler()
        {
            _random = new Random((int)DateTime.UtcNow.Ticks);
        }

        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            this.array = new byte[ClientSettings.Size];
            this.ctx = ctx;

            // Send the initial messages.
            this.GenerateTraffic();
        }

        protected override void ChannelRead0(IChannelHandlerContext context, object message)
        {
            // Server is supposed to send nothing, but if it sends something, discard it.
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception e)
        {
            Console.WriteLine("{0}", e.ToString());
            this.ctx.CloseAsync();
        }
        

        async void GenerateTraffic()
        {
            try
            {

                lock (_random)
                {
                    _random.NextBytes(array);
                }
                
                IByteBuffer buffer = Unpooled.WrappedBuffer(this.array);
                // Flush the outbound buffer to the socket.
                // Once flushed, generate the same amount of traffic again.
                await this.ctx.WriteAndFlushAsync(buffer);
                await Task.Delay(1000);
                this.GenerateTraffic();
            }
            catch
            {
                await this.ctx.CloseAsync();
            }
        }
    }
}