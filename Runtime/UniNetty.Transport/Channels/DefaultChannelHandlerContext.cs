// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Channels
{
    using System.Diagnostics.Contracts;
    using UniNetty.Common.Concurrency;

    sealed class DefaultChannelHandlerContext : AbstractChannelHandlerContext
    {
        public DefaultChannelHandlerContext(
            DefaultChannelPipeline pipeline, IEventExecutor executor, string name, IChannelHandler handler)
            : base(pipeline, executor, name, GetSkipPropagationFlags(handler))
        {
            Contract.Requires(handler != null);

            this.Handler = handler;
        }

        public override IChannelHandler Handler { get; }
    }
}