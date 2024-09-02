// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UniNetty.Codecs.Http;
using UniNetty.Common.Internal.Logging;
using UniNetty.Handlers.Tls;
using UniNetty.Transport.Bootstrapping;
using UniNetty.Transport.Channels;
using UniNetty.Transport.Channels.Sockets;

namespace UniNetty.Examples.WebSockets.Server
{
    public class WebSocketServer
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<WebSocketServer>();

        private MultithreadEventLoopGroup _bossGroup;
        private MultithreadEventLoopGroup _workGroup;
        private IChannel _channel;

        public async Task StartAsync(X509Certificate2 cert, int port)
        {
            _bossGroup = new MultithreadEventLoopGroup(1);
            _workGroup = new MultithreadEventLoopGroup();

            var bootstrap = new ServerBootstrap();
            bootstrap.Group(_bossGroup, _workGroup);
            bootstrap.Channel<TcpServerSocketChannel>();

            bootstrap
                .Option(ChannelOption.SoBacklog, 8192)
                .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    if (cert != null)
                    {
                        pipeline.AddLast(TlsHandler.Server(cert));
                    }

                    pipeline.AddLast(new HttpServerCodec());
                    pipeline.AddLast(new HttpObjectAggregator(65536));
                    pipeline.AddLast(new WebSocketServerHandler(null != cert));
                }));

            _channel = await bootstrap.BindAsync(port);

            Logger.Info("Open your web browser and navigate to "
                        + $"{(null != cert ? "https" : "http")}"
                        + $"://127.0.0.1:{port}/");
            Logger.Info("Listening on "
                        + $"{(null != cert ? "wss" : "ws")}"
                        + $"://127.0.0.1:{port}/websocket");
        }

        public async Task StopAsync()
        {
            await _channel.CloseAsync();
            await _workGroup.ShutdownGracefullyAsync();
            await _bossGroup.ShutdownGracefullyAsync();
        }
    }
}