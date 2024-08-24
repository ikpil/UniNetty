using System.Net;
using System.Security.Cryptography.X509Certificates;
using UniNetty.Examples.Discard.Client;
using UniNetty.Examples.Discard.Server;
using UniNetty.Examples.Echo.Client;
using UniNetty.Examples.Echo.Server;
using UniNetty.Examples.Factorial.Client;
using UniNetty.Examples.Factorial.Server;
using UniNetty.Examples.HttpServer;
using UniNetty.Examples.QuoteOfTheMoment.Client;
using UniNetty.Examples.QuoteOfTheMoment.Server;
using UniNetty.Examples.SecureChat.Client;
using UniNetty.Examples.SecureChat.Server;
using UniNetty.Examples.Telnet.Client;
using UniNetty.Examples.Telnet.Server;
using UniNetty.Examples.WebSockets.Client;
using UniNetty.Examples.WebSockets.Server;

namespace UniNetty.Examples.DemoSupports
{
    public class DemoContext
    {
        public X509Certificate2 Cert { get; private set; }

        public void SetCertificate(X509Certificate2 cert)
        {
            Cert = cert;
        }

        public void RunWebSocketClient(string host, int port, string path)
        {
            var client = new WebSocketClient();
            client.RunClientAsync(Cert, IPAddress.Parse(host), port, path).Wait();
        }

        public void RunTelentClient(string host, int port)
        {
            var client = new TelnetClient();
            client.RunClientAsync(Cert, IPAddress.Parse(host), port).Wait();
        }

        public void RunSecureChatClient(string host, int port)
        {
            var client = new SecureChatClient();
            client.RunClientAsync(Cert, IPAddress.Parse(host), port).Wait();
        }

        public void RunQuoteOfTheMomentClient(int port)
        {
            var client = new QuoteOfTheMomentClient();
            client.RunClientAsync(port).Wait();
        }

        public void RunFactorialClient(string host, int port, int count)
        {
            var client = new FactorialClient();
            client.RunClientAsync(Cert, IPAddress.Parse(host), port, count).Wait();
        }

        public void RunEchoClient(string host, int port, int size)
        {
            var client = new EchoClient();
            client.RunClientAsync(Cert, IPAddress.Parse(host), port, size).Wait();
        }

        public void RunDiscardClient(string host, int port, int size)
        {
            var client = new DiscardClient();
            client.RunClientAsync(Cert, IPAddress.Parse(host), port, size).Wait();
        }


        public void RunWebSocketServer(int port)
        {
            var server = new WebSocketServer();
            server.RunServerAsync(Cert, port).Wait();
        }

        public void RunTelnetServer(int port)
        {
            var server = new TelnetServer();
            server.RunServerAsync(Cert, port).Wait();
        }

        public void RunSecureChatServer(int port)
        {
            var server = new SecureChatServer();
            server.RunServerAsync(Cert, port).Wait();
        }

        public void QuoteOfTheMomentServer(int port)
        {
            var server = new QuoteOfTheMomentServer();
            server.RunServerAsync(port).Wait();
        }

        public void RunHelloHttpServer(int port)
        {
            var server = new HelloHttpServer();
            server.RunServerAsync(Cert, port).Wait();
        }

        public void RunFactorialServer(int port)
        {
            var server = new FactorialServer();
            server.RunServerAsync(Cert, port).Wait();
        }

        public void RunEchoServer(int port)
        {
            var server = new EchoServer();
            server.RunServerAsync(Cert, port).Wait();
        }

        public void RunDiscardServer(int port)
        {
            var server = new DiscardServer();
            server.RunServerAsync(Cert, port).Wait();
        }
    }
}