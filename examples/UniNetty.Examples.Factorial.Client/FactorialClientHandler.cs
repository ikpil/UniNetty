// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Examples.Factorial.Client
{
    using System.Collections.Concurrent;
    using System.Numerics;
    using UniNetty.Transport.Channels;

    public class FactorialClientHandler : SimpleChannelInboundHandler<BigInteger>
    {
        private int _count;
        IChannelHandlerContext ctx;
        int receivedMessages;
        int next = 1;
        readonly BlockingCollection<BigInteger> answer = new BlockingCollection<BigInteger>();

        public FactorialClientHandler(int count)
        {
            _count = count;
        }
            

        public BigInteger GetFactorial() => this.answer.Take();

        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            this.ctx = ctx;
            this.SendNumbers();
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, BigInteger msg)
        {
            this.receivedMessages++;
            if (this.receivedMessages == _count)
            {
                ctx.CloseAsync().ContinueWith(t => this.answer.Add(msg));
            }
        }

        void SendNumbers()
        {
            for (int i = 0; (i < 4096) && (this.next <= _count); i++)
            {
                this.ctx.WriteAsync(new BigInteger(this.next));
                this.next++;
            }
            this.ctx.Flush();
        }
    }
}