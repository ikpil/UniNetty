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

        public async Task RunServerAsync(X509Certificate2 cert, int port)
        {
            var bossGroup = new MultithreadEventLoopGroup(1);
            var workGroup = new MultithreadEventLoopGroup();

            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap.Group(bossGroup, workGroup);
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

                IChannel bootstrapChannel = await bootstrap.BindAsync(port);

                Logger.Info("Open your web browser and navigate to "
                                  + $"{(null != cert ? "https" : "http")}"
                                  + $"://127.0.0.1:{port}/");
                Logger.Info("Listening on "
                                  + $"{(null != cert ? "wss" : "ws")}"
                                  + $"://127.0.0.1:{port}/websocket");
                Console.ReadLine();

                await bootstrapChannel.CloseAsync();
            }
            finally
            {
                workGroup.ShutdownGracefullyAsync().Wait();
                bossGroup.ShutdownGracefullyAsync().Wait();
            }
        }
    }
}