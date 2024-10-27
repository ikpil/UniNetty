// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UniNetty.Codecs.Http;
using UniNetty.Common.Internal.Logging;
using UniNetty.Handlers.Tls;
using UniNetty.Transport.Bootstrapping;
using UniNetty.Transport.Channels;
using UniNetty.Transport.Channels.Sockets;

namespace UniNetty.Examples.HttpServer
{
    public class HelloHttpServer
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<HelloHttpServer>();

        private MultithreadEventLoopGroup _group;
        private MultithreadEventLoopGroup _workGroup;
        private IChannel _channel;

        public async Task StartAsync(X509Certificate2 cert, int port)
        {
            _group = new MultithreadEventLoopGroup(1);
            _workGroup = new MultithreadEventLoopGroup();

            var bootstrap = new ServerBootstrap();
            bootstrap.Group(_group, _workGroup);
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

                    pipeline.AddLast("encoder", new HttpResponseEncoder());
                    pipeline.AddLast("decoder", new HttpRequestDecoder(4096, 8192, 8192, false));
                    pipeline.AddLast("handler", new HelloHttpServerHandler());
                }));

            _channel = await bootstrap.BindAsync(port);

            Logger.Info($"Open your web browser and navigate to ");
            Logger.Info($"{(null != cert ? "https" : "http")}://127.0.0.1:{port}/plaintext");
            Logger.Info($"{(null != cert ? "https" : "http")}://127.0.0.1:{port}/json");
        }

        public async Task StopAsync()
        {
            await _channel.CloseAsync();
            await _group.ShutdownGracefullyAsync();
        }
    }
}