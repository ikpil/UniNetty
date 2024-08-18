// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WebSockets.Server
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using UniNetty.Codecs.Http;
    using UniNetty.Common;
    using UniNetty.Handlers.Tls;
    using UniNetty.Transport.Bootstrapping;
    using UniNetty.Transport.Channels;
    using UniNetty.Transport.Channels.Sockets;
    using Examples.Common;

    class Program
    {
        static Program()
        {
            ResourceLeakDetector.Level = ResourceLeakDetector.DetectionLevel.Disabled;
        }

        static async Task RunServerAsync()
        {
            Console.WriteLine(
                $"\n{RuntimeInformation.OSArchitecture} {RuntimeInformation.OSDescription}"
                + $"\n{RuntimeInformation.ProcessArchitecture} {RuntimeInformation.FrameworkDescription}"
                + $"\nProcessor Count : {Environment.ProcessorCount}\n");

            Console.WriteLine("Transport type : Socket");

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
            }

            Console.WriteLine($"Server garbage collection : {(GCSettings.IsServerGC ? "Enabled" : "Disabled")}");
            Console.WriteLine($"Current latency mode for garbage collection: {GCSettings.LatencyMode}");
            Console.WriteLine("\n");

            IEventLoopGroup bossGroup;
            IEventLoopGroup workGroup;
            bossGroup = new MultithreadEventLoopGroup(1);
            workGroup = new MultithreadEventLoopGroup();

            X509Certificate2 tlsCertificate = null;
            if (ServerSettings.IsSsl)
            {
                tlsCertificate = new X509Certificate2(Path.Combine(ExampleHelper.ProcessDirectory, "dotnetty.com.pfx"), "password");
            }

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
                        if (tlsCertificate != null)
                        {
                            pipeline.AddLast(TlsHandler.Server(tlsCertificate));
                        }

                        pipeline.AddLast(new HttpServerCodec());
                        pipeline.AddLast(new HttpObjectAggregator(65536));
                        pipeline.AddLast(new WebSocketServerHandler());
                    }));

                int port = ServerSettings.Port;
                IChannel bootstrapChannel = await bootstrap.BindAsync(IPAddress.Loopback, port);

                Console.WriteLine("Open your web browser and navigate to "
                                  + $"{(ServerSettings.IsSsl ? "https" : "http")}"
                                  + $"://127.0.0.1:{port}/");
                Console.WriteLine("Listening on "
                                  + $"{(ServerSettings.IsSsl ? "wss" : "ws")}"
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

        static void Main() => RunServerAsync().Wait();
    }
}