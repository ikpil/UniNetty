// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UniNetty.Buffers;
using UniNetty.Common.Internal.Logging;
using UniNetty.Transport.Bootstrapping;
using UniNetty.Transport.Channels;
using UniNetty.Transport.Channels.Sockets;

namespace UniNetty.Examples.QuoteOfTheMoment.Client
{
    public class QuoteOfTheMomentClient
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<QuoteOfTheMomentClient>();
        private MultithreadEventLoopGroup _group;

        public async Task StartAsync(int port)
        {
            _group = new MultithreadEventLoopGroup();

            var bootstrap = new Bootstrap();
            bootstrap
                .Group(_group)
                .Channel<SocketDatagramChannel>()
                .Option(ChannelOption.SoBroadcast, true)
                .Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    channel.Pipeline.AddLast("Quote", new QuoteOfTheMomentClientHandler());
                }));

            IChannel clientChannel = await bootstrap.BindAsync(IPEndPoint.MinPort);

            Logger.Info("Sending broadcast QOTM");

            // Broadcast the QOTM request to port.
            byte[] bytes = Encoding.UTF8.GetBytes("QOTM?");
            IByteBuffer buffer = Unpooled.WrappedBuffer(bytes);
            await clientChannel.WriteAndFlushAsync(
                new DatagramPacket(
                    buffer,
                    new IPEndPoint(IPAddress.Broadcast, port)));

            Logger.Info("Waiting for response.");

            await Task.Delay(5000);
            Logger.Info("Waiting for response time 5000 completed. Closing client channel.");

            await clientChannel.CloseAsync();
        }

        public async Task StopAsync()
        {
            await _group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
        }
    }
}