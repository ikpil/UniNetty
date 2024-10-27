using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using UniNetty.Common.Internal.Logging;
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

            return AnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }

        public IDisposable RunDiscardClient(ExampleSetting setting)
        {
            var client = new DiscardClient();
            _ = client.StartAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Size);

            return AnonymousDisposer.Create(() =>
            {
                _ = client.StopAsync();
            });
        }


        // echo
        public IDisposable RunEchoServer(ExampleSetting setting)
        {
            var server = new EchoServer();
            _ = server.StartAsync(Cert, setting.Port);

            return AnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }

        public IDisposable RunEchoClient(ExampleSetting setting)
        {
            var client = new EchoClient();
            _ = client.StartAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Size);
            return AnonymousDisposer.Create(() =>
            {
                _ = client.StopAsync();
            });
        }

        // factorial
        public IDisposable RunFactorialServer(ExampleSetting setting)
        {
            var server = new FactorialServer();
            _ = server.StartAsync(Cert, setting.Port);

            return AnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }

        public IDisposable RunFactorialClient(ExampleSetting setting)
        {
            var client = new FactorialClient();
            _ = client.StartAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Count);


            return AnonymousDisposer.Create(() =>
            {
                _ = client.StopAsync();
            });
        }

        // QuoteOfTheMoment
        public IDisposable QuoteOfTheMomentServer(ExampleSetting setting)
        {
            var server = new QuoteOfTheMomentServer();
            _ = server.StartAsync(setting.Port);
            return AnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }

        public IDisposable RunQuoteOfTheMomentClient(ExampleSetting setting)
        {
            var client = new QuoteOfTheMomentClient();
            _ = client.StartAsync(setting.Port);

            return AnonymousDisposer.Create(() =>
            {
                _ = client.StopAsync();
            });
        }

        // secure
        public IDisposable RunSecureChatServer(ExampleSetting setting)
        {
            var server = new SecureChatServer();
            _ = server.StartAsync(Cert, setting.Port);
            return AnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }

        public IDisposable RunSecureChatClient(ExampleSetting setting)
        {
            var client = new SecureChatClient();
            _ = client.StartAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port);
            return AnonymousDisposer.Create(() =>
            {
                _ = client.StopAsync();
            });
        }

        // 
        public IDisposable RunTelnetServer(ExampleSetting setting)
        {
            var server = new TelnetServer();
            _ = server.StartAsync(Cert, setting.Port);

            return AnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }


        public IDisposable RunTelnetClient(ExampleSetting setting)
        {
            var client = new TelnetClient();
            _ = client.StartAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port);
            return AnonymousDisposer.Create(() =>
            {
                _ = client.StopAsync();
            });
        }


        // websocket
        public IDisposable RunWebSocketServer(ExampleSetting setting)
        {
            var server = new WebSocketServer();
            _ = server.StartAsync(Cert, setting.Port);
            return AnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }

        public IDisposable RunWebSocketClient(ExampleSetting setting)
        {
            var client = new WebSocketClient();
            _ = client.StartAsync(Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Path);

            return AnonymousDisposer.Create(() =>
            {
                _ = client.StopAsync();
            });
        }

        // http
        public IDisposable RunHelloHttpServer(ExampleSetting setting)
        {
            var server = new HelloHttpServer();
            _ = server.StartAsync(Cert, setting.Port);

            return AnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }

        public IDisposable RunHelloHttpClient(ExampleSetting setting)
        {
            ExampleSupport.Shared.OpenUrl($"https://{setting.Ip}:{setting.Port}/json");

            return null;
        }
    }
}