using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UniNetty.Handlers.Logging;
using UniNetty.Handlers.Tls;
using UniNetty.Transport.Bootstrapping;
using UniNetty.Transport.Channels;
using UniNetty.Transport.Channels.Sockets;

namespace UniNetty.Examples.Factorial.Client
{
    public class FactorialClient
    {
        public async Task RunClientAsync(X509Certificate2 cert, IPAddress host, int port, int count)
        {
            var group = new MultithreadEventLoopGroup();

            try
            {
                var bootstrap = new Bootstrap();
                bootstrap
                    .Group(group)
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

                IChannel bootstrapChannel = await bootstrap.ConnectAsync(new IPEndPoint(host, port));

                // Get the handler instance to retrieve the answer.
                var handler = (FactorialClientHandler)bootstrapChannel.Pipeline.Last();

                // Print out the answer.
                Console.WriteLine("Factorial of {0} is: {1}", count.ToString(), handler.GetFactorial().ToString());

                Console.ReadLine();

                await bootstrapChannel.CloseAsync();
            }
            finally
            {
                group.ShutdownGracefullyAsync().Wait(1000);
            }
        }
    }
}