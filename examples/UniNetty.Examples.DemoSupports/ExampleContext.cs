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
    public class ExampleContext
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


        // discard
        public void RunDiscardServer(ExampleSetting setting)
        {
            var server = new DiscardServer();
            server.RunServerAsync(Cert, setting.Port).Wait();
        }

        public void RunDiscardClient(ExampleSetting setting)
        {
            var client = new DiscardClient();
            client.RunClientAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Size).Wait();
        }


        // echo
        public void RunEchoServer(ExampleSetting setting)
        {
            var server = new EchoServer();
            server.RunServerAsync(Cert, setting.Port).Wait();
        }

        public void RunEchoClient(ExampleSetting setting)
        {
            var client = new EchoClient();
            client.RunClientAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Size).Wait();
        }


        // factorial
        public void RunFactorialServer(ExampleSetting setting)
        {
            var server = new FactorialServer();
            server.RunServerAsync(Cert, setting.Port).Wait();
        }

        public void RunFactorialClient(ExampleSetting setting)
        {
            var client = new FactorialClient();
            client.RunClientAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Count).Wait();
        }
        
        // http
        public void RunHelloHttpServer(ExampleSetting setting)
        {
            var server = new HelloHttpServer();
            server.RunServerAsync(Cert, setting.Port).Wait();
        }

        public void RunHelloHttpClient(ExampleSetting setting)
        {
            ExampleSupport.Shared.OpenUrl($"https://{setting.Ip}:{setting.Port}/json");
        }

        public void RunWebSocketServer(ExampleSetting setting)
        {
            // var server = new WebSocketServer();
            // server.RunServerAsync(Cert, port).Wait();
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

    }
}