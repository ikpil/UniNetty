// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UniNetty.Handlers.Logging;
using UniNetty.Handlers.Tls;
using UniNetty.Transport.Bootstrapping;
using UniNetty.Transport.Channels;
using UniNetty.Transport.Channels.Sockets;

namespace UniNetty.Examples.Discard.Client
{
    public class DiscardClient
    {
        private MultithreadEventLoopGroup _group;
        private IChannel _channel;

        public async Task StartAsync(X509Certificate2 cert, IPAddress host, int port, int size)
        {
            _group = new MultithreadEventLoopGroup();

            string targetHost = null;
            if (null != cert)
            {
                targetHost = cert.GetNameInfo(X509NameType.DnsName, false);
            }

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
                        pipeline.AddLast(new TlsHandler(stream => new SslStream(stream, true, (sender, certificate, chain, errors) => true), new ClientTlsSettings(targetHost)));
                    }

                    pipeline.AddLast(new LoggingHandler());
                    pipeline.AddLast(new DiscardClientHandler(size));
                }));

            _channel = await bootstrap.ConnectAsync(new IPEndPoint(host, port));
        }

        public async Task StopAsync()
        {
            await _channel.CloseAsync();
            await _group.ShutdownGracefullyAsync();
        }
    }
}