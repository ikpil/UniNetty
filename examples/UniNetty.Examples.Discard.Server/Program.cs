// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Examples.Discard.Server
{
    using System;
    using System.IO;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using UniNetty.Handlers.Logging;
    using UniNetty.Handlers.Tls;
    using UniNetty.Transport.Bootstrapping;
    using UniNetty.Transport.Channels;
    using UniNetty.Transport.Channels.Sockets;
    using Examples.Common;

    class Program
    {
        static async Task RunServerAsync()
        {
            ExampleHelper.SetConsoleLogger();

            var bossGroup = new MultithreadEventLoopGroup(1);
            var workerGroup = new MultithreadEventLoopGroup();
            X509Certificate2 tlsCertificate = null;
            if (ServerSettings.IsSsl)
            {
                tlsCertificate = new X509Certificate2(Path.Combine(ExampleHelper.ProcessDirectory, "dotnetty.com.pfx"), "password");
            }

            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap
                    .Group(bossGroup, workerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.SoBacklog, 100)
                    .Handler(new LoggingHandler("LSTN"))
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        if (tlsCertificate != null)
                        {
                            pipeline.AddLast(TlsHandler.Server(tlsCertificate));
                        }

                        pipeline.AddLast(new LoggingHandler("CONN"));
                        pipeline.AddLast(new DiscardServerHandler());
                    }));

                IChannel bootstrapChannel = await bootstrap.BindAsync(ServerSettings.Port);

                Console.ReadLine();

                await bootstrapChannel.CloseAsync();
            }
            finally
            {
                Task.WaitAll(bossGroup.ShutdownGracefullyAsync(), workerGroup.ShutdownGracefullyAsync());
            }
        }

        public static void Main() => RunServerAsync().Wait();
    }
}