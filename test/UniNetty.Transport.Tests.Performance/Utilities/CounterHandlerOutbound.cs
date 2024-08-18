// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Tests.Performance.Utilities
{
    using System.Threading.Tasks;
    using UniNetty.Transport.Channels;
    using NBench;

    class CounterHandlerOutbound : ChannelHandlerAdapter
    {
        readonly Counter throughput;

        public CounterHandlerOutbound(Counter throughput)
        {
            this.throughput = throughput;
        }

        public override Task WriteAsync(IChannelHandlerContext context, object message)
        {
            this.throughput.Increment();
            return context.WriteAsync(message);
        }

        public override bool IsSharable => true;
    }
}