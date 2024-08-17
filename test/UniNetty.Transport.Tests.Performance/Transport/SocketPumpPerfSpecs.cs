﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Tests.Performance.Transport
{
    using UniNetty.Transport.Channels;
    using UniNetty.Transport.Channels.Sockets;

    public sealed class SocketPumpPerfSpecs : AbstractPumpPerfSpecs<TcpServerSocketChannel, TcpSocketChannel>
    {
        protected override IEventLoopGroup NewServerGroup() => new MultithreadEventLoopGroup(1);

        protected override IEventLoopGroup NewWorkerGroup(IEventLoopGroup serverGroup) => new MultithreadEventLoopGroup(1);
    }
}
