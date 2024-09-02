// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UniNetty.Handlers.Logging;
using UniNetty.Handlers.Tls;
using UniNetty.Transport.Bootstrapping;
using UniNetty.Transport.Channels;
using UniNetty.Transport.Channels.Sockets;

namespace UniNetty.Examples.Factorial.Server
{
    public class FactorialServer
    {
        private MultithreadEventLoopGroup _bossGroup;
        private MultithreadEventLoopGroup _workerGroup;
        private IChannel _channel;

        public async Task StartAsync(X509Certificate2 cert, int port)
        {
            _bossGroup = new MultithreadEventLoopGroup(1);
            _workerGroup = new MultithreadEventLoopGroup();
            var bootstrap = new ServerBootstrap();
            bootstrap
                .Group(_bossGroup, _workerGroup)
                .Channel<TcpServerSocketChannel>()
                .Option(ChannelOption.SoBacklog, 100)
                .Handler(new LoggingHandler("LSTN"))
                .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    if (cert != null)
                    {
                        pipeline.AddLast(TlsHandler.Server(cert));
                    }

                    pipeline.AddLast(new LoggingHandler("CONN"));
                    pipeline.AddLast(new NumberEncoder(), new BigIntegerDecoder(), new FactorialServerHandler());
                }));

            _channel = await bootstrap.BindAsync(port);
        }

        public async Task StopAsync()
        {
            await _channel.CloseAsync();
            await Task.WhenAll(_bossGroup.ShutdownGracefullyAsync(), _workerGroup.ShutdownGracefullyAsync());
        }
    }
}