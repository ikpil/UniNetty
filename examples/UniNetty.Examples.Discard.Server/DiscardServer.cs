// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UniNetty.Handlers.Logging;
using UniNetty.Handlers.Tls;
using UniNetty.Transport.Bootstrapping;
using UniNetty.Transport.Channels;
using UniNetty.Transport.Channels.Sockets;

namespace UniNetty.Examples.Discard.Server
{
    public class DiscardServer
    {
        private MultithreadEventLoopGroup _bossGroup;
        private MultithreadEventLoopGroup _workerGroup;
        private IChannel _listen;

        public async Task StartAsync(X509Certificate2 cert, int port)
        {
            _bossGroup = new MultithreadEventLoopGroup(1);
            _workerGroup = new MultithreadEventLoopGroup();

            try
            {
                var bootstrap = new ServerBootstrap()
                    .Group(_bossGroup, _workerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.SoBacklog, 100)
                    .Handler(new LoggingHandler("LSTN"))
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        if (null != cert)
                        {
                            pipeline.AddLast(TlsHandler.Server(cert));
                        }

                        pipeline.AddLast(new LoggingHandler("CONN"));
                        pipeline.AddLast(new DiscardServerHandler());
                    }));

                _listen = await bootstrap.BindAsync(port);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task StopAsync()
        {
            if (null == _listen)
                return;

            try
            {
                await _listen.CloseAsync();
                _listen = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Task.WaitAll(_bossGroup.ShutdownGracefullyAsync(), _workerGroup.ShutdownGracefullyAsync());
                _bossGroup = null;
                _workerGroup = null;
            }
        }
    }
}