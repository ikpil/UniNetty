// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Channels
{
    using System.Net;
    using System.Threading.Tasks;

    public interface IChannelUnsafe
    {
        IRecvByteBufAllocatorHandle RecvBufAllocHandle { get; }

        Task RegisterAsync(IEventLoop eventLoop);

        Task DeregisterAsync();

        Task BindAsync(EndPoint localAddress);

        Task ConnectAsync(EndPoint remoteAddress, EndPoint localAddress);

        Task DisconnectAsync();

        Task CloseAsync();

        void CloseForcibly();

        void BeginRead();

        Task WriteAsync(object message);

        void Flush();

        ChannelOutboundBuffer OutboundBuffer { get; }
    }
}