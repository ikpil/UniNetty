using System;
using System.Threading.Tasks;
using UniNetty.Handlers.Logging;
using UniNetty.Transport.Bootstrapping;
using UniNetty.Transport.Channels;
using UniNetty.Transport.Channels.Sockets;

namespace UniNetty.Examples.QuoteOfTheMoment.Server
{
    public class QuoteOfTheMomentServer
    {
        public async Task RunServerAsync(int port)
        {
            var group = new MultithreadEventLoopGroup();
            try
            {
                var bootstrap = new Bootstrap();
                bootstrap
                    .Group(group)
                    .Channel<SocketDatagramChannel>()
                    .Option(ChannelOption.SoBroadcast, true)
                    .Handler(new LoggingHandler("SRV-LSTN"))
                    .Handler(new ActionChannelInitializer<IChannel>(channel =>
                    {
                        channel.Pipeline.AddLast("Quote", new QuoteOfTheMomentServerHandler());
                    }));

                IChannel boundChannel = await bootstrap.BindAsync();
                Console.WriteLine("Press any key to terminate the server.");
                Console.ReadLine();

                await boundChannel.CloseAsync();
            }
            finally
            {
                await group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            }
        }
    }
}