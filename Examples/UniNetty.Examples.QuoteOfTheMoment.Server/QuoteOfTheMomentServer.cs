// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using UniNetty.Common.Internal.Logging;
using UniNetty.Handlers.Logging;
using UniNetty.Transport.Bootstrapping;
using UniNetty.Transport.Channels;
using UniNetty.Transport.Channels.Sockets;

namespace UniNetty.Examples.QuoteOfTheMoment.Server
{
    public class QuoteOfTheMomentServer
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<QuoteOfTheMomentServer>();

        private MultithreadEventLoopGroup _group;
        private IChannel _channel;

        public async Task StartAsync(int port)
        {
            _group = new MultithreadEventLoopGroup();
            var bootstrap = new Bootstrap();
            bootstrap
                .Group(_group)
                .Channel<SocketDatagramChannel>()
                .Option(ChannelOption.SoBroadcast, true)
                .Handler(new LoggingHandler("SRV-LSTN"))
                .Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    channel.Pipeline.AddLast("Quote", new QuoteOfTheMomentServerHandler());
                }));

            _channel = await bootstrap.BindAsync(port);
            //Logger.Info("Press any key to terminate the server.");
        }

        public async Task StopAsync()
        {
            await _channel.CloseAsync();
            await _group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
        }
    }
}