using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UniNetty.Codecs.Http;
using UniNetty.Handlers.Tls;
using UniNetty.Transport.Bootstrapping;
using UniNetty.Transport.Channels;
using UniNetty.Transport.Channels.Sockets;

namespace UniNetty.Examples.HttpServer
{
    public class HttpServer
    {
        public async Task RunServerAsync(X509Certificate2 cert, int port)
        {
            var group = new MultithreadEventLoopGroup(1);
            var workGroup = new MultithreadEventLoopGroup();

            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap.Group(group, workGroup);
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
                        pipeline.AddLast("handler", new HelloServerHandler());
                    }));

                IChannel bootstrapChannel = await bootstrap.BindAsync(port);

                Console.WriteLine($"Open your web browser and navigate to ");
                Console.WriteLine($"{(null != cert ? "https" : "http")}://127.0.0.1:{port}/plaintext");
                Console.WriteLine($"{(null != cert ? "https" : "http")}://127.0.0.1:{port}/json");

                Console.ReadLine();

                await bootstrapChannel.CloseAsync();
            }
            finally
            {
                group.ShutdownGracefullyAsync().Wait();
            }
        }
    }
}