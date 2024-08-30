using System;
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

        // discard
        public IDisposable RunDiscardServer(ExampleSetting setting)
        {
            var server = new DiscardServer();
            _ = server.StartAsync(Cert, setting.Port);

            return ExampleSupport.Shared.CreateDisposer(() =>
            {
                _ = server.StopAsync();
            });
        }

        public IDisposable RunDiscardClient(ExampleSetting setting)
        {
            var client = new DiscardClient();
            client.RunClientAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Size).Wait();

            return null;
        }


        // echo
        public IDisposable RunEchoServer(ExampleSetting setting)
        {
            var server = new EchoServer();
            server.RunServerAsync(Cert, setting.Port).Wait();
            return null;
        }

        public IDisposable RunEchoClient(ExampleSetting setting)
        {
            var client = new EchoClient();
            client.RunClientAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Size).Wait();
            return null;
        }

        // factorial
        public IDisposable RunFactorialServer(ExampleSetting setting)
        {
            var server = new FactorialServer();
            server.RunServerAsync(Cert, setting.Port).Wait();
            return null;
        }

        public IDisposable RunFactorialClient(ExampleSetting setting)
        {
            var client = new FactorialClient();
            client.RunClientAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Count).Wait();
            return null;
        }

        // QuoteOfTheMoment
        public IDisposable QuoteOfTheMomentServer(ExampleSetting setting)
        {
            var server = new QuoteOfTheMomentServer();
            server.RunServerAsync(setting.Port).Wait();
            return null;
        }

        public IDisposable RunQuoteOfTheMomentClient(ExampleSetting setting)
        {
            var client = new QuoteOfTheMomentClient();
            client.RunClientAsync(setting.Port).Wait();
            return null;
        }

        // secure
        public IDisposable RunSecureChatServer(ExampleSetting setting)
        {
            var server = new SecureChatServer();
            server.RunServerAsync(Cert, setting.Port).Wait();
            return null;
        }

        public IDisposable RunSecureChatClient(ExampleSetting setting)
        {
            var client = new SecureChatClient();
            client.RunClientAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port).Wait();
            return null;
        }

        // 
        public IDisposable RunTelnetServer(ExampleSetting setting)
        {
            var server = new TelnetServer();
            server.RunServerAsync(Cert, setting.Port).Wait();
            return null;
        }


        public IDisposable RunTelnetClient(ExampleSetting setting)
        {
            var client = new TelnetClient();
            client.RunClientAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port).Wait();
            return null;
        }


        // websocket
        public IDisposable RunWebSocketServer(ExampleSetting setting)
        {
            var server = new WebSocketServer();
            server.RunServerAsync(Cert, setting.Port).Wait();
            return null;
        }

        public IDisposable RunWebSocketClient(ExampleSetting setting)
        {
            var client = new WebSocketClient();
            client.RunClientAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Path).Wait();
            return null;
        }


        // http
        public IDisposable RunHelloHttpServer(ExampleSetting setting)
        {
            var server = new HelloHttpServer();
            server.RunServerAsync(Cert, setting.Port).Wait();
            return null;
        }

        public IDisposable RunHelloHttpClient(ExampleSetting setting)
        {
            ExampleSupport.Shared.OpenUrl($"https://{setting.Ip}:{setting.Port}/json");
            return null;
        }
    }
}