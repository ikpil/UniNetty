// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Channels
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using UniNetty.Buffers;
    using UniNetty.Common.Utilities;

    public interface IChannel : IAttributeMap, IComparable<IChannel>
    {
        IChannelId Id { get; }

        IByteBufferAllocator Allocator { get; }

        IEventLoop EventLoop { get; }

        IChannel Parent { get; }

        bool Open { get; }

        bool Active { get; }

        bool Registered { get; }

        /// <summary>
        ///     Return the <see cref="ChannelMetadata" /> of the <see cref="IChannel" /> which describe the nature of the
        ///     <see cref="IChannel" />.
        /// </summary>
        ChannelMetadata Metadata { get; }

        EndPoint LocalAddress { get; }

        EndPoint RemoteAddress { get; }

        bool IsWritable { get; }

        IChannelUnsafe Unsafe { get; }

        IChannelPipeline Pipeline { get; }

        IChannelConfiguration Configuration { get; }

        Task CloseCompletion { get; }

        Task DeregisterAsync();

        Task BindAsync(EndPoint localAddress);

        Task ConnectAsync(EndPoint remoteAddress);

        Task ConnectAsync(EndPoint remoteAddress, EndPoint localAddress);

        Task DisconnectAsync();

        Task CloseAsync();

        // todo: make these available through separate interface to hide them from public API on channel

        IChannel Read();

        Task WriteAsync(object message);

        IChannel Flush();

        Task WriteAndFlushAsync(object message);
    }
}