using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UniNetty.Buffers;
using UniNetty.Transport.Bootstrapping;
using UniNetty.Transport.Channels;
using UniNetty.Transport.Channels.Sockets;

namespace UniNetty.Examples.QuoteOfTheMoment.Client
{
    public class QuoteOfTheMomentClient
    {
        public async Task RunClientAsync(int port)
        {
            var group = new MultithreadEventLoopGroup();

            try
            {
                var bootstrap = new Bootstrap();
                bootstrap
                    .Group(group)
                    .Channel<SocketDatagramChannel>()
                    .Option(ChannelOption.SoBroadcast, true)
                    .Handler(new ActionChannelInitializer<IChannel>(channel =>
                    {
                        channel.Pipeline.AddLast("Quote", new QuoteOfTheMomentClientHandler());
                    }));

                IChannel clientChannel = await bootstrap.BindAsync(IPEndPoint.MinPort);

                Console.WriteLine("Sending broadcast QOTM");

                // Broadcast the QOTM request to port.
                byte[] bytes = Encoding.UTF8.GetBytes("QOTM?");
                IByteBuffer buffer = Unpooled.WrappedBuffer(bytes);
                await clientChannel.WriteAndFlushAsync(
                    new DatagramPacket(
                        buffer,
                        new IPEndPoint(IPAddress.Broadcast, port)));

                Console.WriteLine("Waiting for response.");

                await Task.Delay(5000);
                Console.WriteLine("Waiting for response time 5000 completed. Closing client channel.");

                await clientChannel.CloseAsync();
            }
            finally
            {
                await group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            }
        }

    }
}