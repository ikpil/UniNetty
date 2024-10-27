// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UniNetty.Codecs;
using UniNetty.Codecs.Strings;
using UniNetty.Handlers.Tls;
using UniNetty.Transport.Bootstrapping;
using UniNetty.Transport.Channels;
using UniNetty.Transport.Channels.Sockets;

namespace UniNetty.Examples.SecureChat.Client
{
    public class SecureChatClient
    {
        private MultithreadEventLoopGroup _group;
        private IChannel _channel;

        public async Task StartAsync(X509Certificate2 cert, IPAddress host, int port)
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

                    pipeline.AddLast(new DelimiterBasedFrameDecoder(8192, Delimiters.LineDelimiter()));
                    pipeline.AddLast(new StringEncoder(), new StringDecoder(), new SecureChatClientHandler());
                }));

            _channel = await bootstrap.ConnectAsync(new IPEndPoint(host, port));

            for (;;)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                try
                {
                    await _channel.WriteAndFlushAsync(line + "\r\n");
                }
                catch
                {
                }

                if (string.Equals(line, "bye", StringComparison.OrdinalIgnoreCase))
                {
                    await _channel.CloseAsync();
                    break;
                }
            }
        }

        public async Task StopAsync()
        {
            await _channel.CloseAsync();
            await _group.ShutdownGracefullyAsync();
        }
    }
}