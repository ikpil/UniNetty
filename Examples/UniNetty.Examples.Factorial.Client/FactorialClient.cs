// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UniNetty.Common.Internal.Logging;
using UniNetty.Handlers.Logging;
using UniNetty.Handlers.Tls;
using UniNetty.Transport.Bootstrapping;
using UniNetty.Transport.Channels;
using UniNetty.Transport.Channels.Sockets;

namespace UniNetty.Examples.Factorial.Client
{
    public class FactorialClient
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<FactorialClient>();

        private MultithreadEventLoopGroup _group;
        private IChannel _channel;

        public async Task StartAsync(X509Certificate2 cert, IPAddress host, int port, int count)
        {
            _group = new MultithreadEventLoopGroup();

            var bootstrap = new Bootstrap();
            bootstrap
                .Group(_group)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;

                    if (cert != null)
                    {
                        var targetHost = cert.GetNameInfo(X509NameType.DnsName, false);
                        pipeline.AddLast(new TlsHandler(stream => new SslStream(stream, true, (sender, certificate, chain, errors) => true), new ClientTlsSettings(targetHost)));
                    }

                    pipeline.AddLast(new LoggingHandler("CONN"));
                    pipeline.AddLast(new BigIntegerDecoder());
                    pipeline.AddLast(new NumberEncoder());
                    pipeline.AddLast(new FactorialClientHandler(count));
                }));

            _channel = await bootstrap.ConnectAsync(new IPEndPoint(host, port));

            // Get the handler instance to retrieve the answer.
            var handler = (FactorialClientHandler)_channel.Pipeline.Last();

            // Print out the answer.
            Logger.Info("Factorial of {0} is: {1}", count.ToString(), handler.GetFactorial().ToString());
        }

        public async Task StopAsync()
        {
            await _channel.CloseAsync();
            await _group.ShutdownGracefullyAsync();
        }
    }
}