using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
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
    public static class UniNettyExampleSupports
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance(typeof(UniNettyExampleSupports));

        public static void OpenUrl(string url)
        {
            try
            {
                // OS에 따라 다른 명령 실행
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var psi = new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true };
                    Process.Start(psi);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"Error opening web browser: {ex.Message}");
            }
        }

        public static string GetPrivateIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return string.Empty;
        }

        // discard
        public static IDisposable RunDiscardServer(UniNettyExampleSetting setting)
        {
            var server = new DiscardServer();
            _ = server.StartAsync(setting.Cert, setting.Port);

            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }

        public static IDisposable RunDiscardClient(UniNettyExampleSetting setting)
        {
            var client = new DiscardClient();
            _ = client.StartAsync(setting.Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Size);

            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = client.StopAsync();
            });
        }


        // echo
        public static IDisposable RunEchoServer(UniNettyExampleSetting setting)
        {
            var server = new EchoServer();
            _ = server.StartAsync(setting.Cert, setting.Port);

            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }

        public static IDisposable RunEchoClient(UniNettyExampleSetting setting)
        {
            var client = new EchoClient();
            _ = client.StartAsync(setting.Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Size);
            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = client.StopAsync();
            });
        }

        // factorial
        public static IDisposable RunFactorialServer(UniNettyExampleSetting setting)
        {
            var server = new FactorialServer();
            _ = server.StartAsync(setting.Cert, setting.Port);

            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }

        public static IDisposable RunFactorialClient(UniNettyExampleSetting setting)
        {
            var client = new FactorialClient();
            _ = client.StartAsync(setting.Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Count);


            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = client.StopAsync();
            });
        }

        // QuoteOfTheMoment
        public static IDisposable QuoteOfTheMomentServer(UniNettyExampleSetting setting)
        {
            var server = new QuoteOfTheMomentServer();
            _ = server.StartAsync(setting.Port);
            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }

        public static IDisposable RunQuoteOfTheMomentClient(UniNettyExampleSetting setting)
        {
            var client = new QuoteOfTheMomentClient();
            _ = client.StartAsync(setting.Port);

            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = client.StopAsync();
            });
        }

        // secure
        public static IDisposable RunSecureChatServer(UniNettyExampleSetting setting)
        {
            var server = new SecureChatServer();
            _ = server.StartAsync(setting.Cert, setting.Port);
            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }

        public static IDisposable RunSecureChatClient(UniNettyExampleSetting setting)
        {
            var client = new SecureChatClient();
            _ = client.StartAsync(setting.Cert, IPAddress.Parse(setting.Ip), setting.Port);
            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = client.StopAsync();
            });
        }

        // 
        public static IDisposable RunTelnetServer(UniNettyExampleSetting setting)
        {
            var server = new TelnetServer();
            _ = server.StartAsync(setting.Cert, setting.Port);

            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }


        public static IDisposable RunTelnetClient(UniNettyExampleSetting setting)
        {
            var client = new TelnetClient();
            _ = client.StartAsync(setting.Cert, IPAddress.Parse(setting.Ip), setting.Port);
            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = client.StopAsync();
            });
        }


        // websocket
        public static IDisposable RunWebSocketServer(UniNettyExampleSetting setting)
        {
            var server = new WebSocketServer();
            _ = server.StartAsync(setting.Cert, setting.Port);
            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }

        public static IDisposable RunWebSocketClient(UniNettyExampleSetting setting)
        {
            var client = new WebSocketClient();
            _ = client.StartAsync(setting.Cert, IPAddress.Parse(setting.Ip), setting.Port, setting.Path);

            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = client.StopAsync();
            });
        }

        // http
        public static IDisposable RunHelloHttpServer(UniNettyExampleSetting setting)
        {
            var server = new HelloHttpServer();
            _ = server.StartAsync(setting.Cert, setting.Port);

            return UniNettyAnonymousDisposer.Create(() =>
            {
                _ = server.StopAsync();
            });
        }

        public static IDisposable RunHelloHttpClient(UniNettyExampleSetting setting)
        {
            OpenUrl($"https://{setting.Ip}:{setting.Port}/json");

            return null;
        }

        public static Dictionary<UniNettyExampleType, UniNettyExample> CreateDefaultExamples(X509Certificate2 cert, string ip)
        {
            // Discard
            var discardSettings = new UniNettyExampleSetting(UniNettyExampleType.Discard);
            discardSettings.SetIp(ip);
            discardSettings.SetPort(8000);
            discardSettings.SetSize(256);
            discardSettings.SetCertification(cert);

            var discard = new UniNettyExample();
            discard.SetSetting(discardSettings);
            discard.SetServer(RunDiscardServer);
            discard.SetClient(RunDiscardClient);

            // Echo
            var echoSetting = new UniNettyExampleSetting(UniNettyExampleType.Echo);
            echoSetting.SetIp(ip);
            echoSetting.SetPort(8010);
            echoSetting.SetSize(256);
            echoSetting.SetCertification(cert);
            
            var echo = new UniNettyExample();
            echo.SetSetting(echoSetting);
            echo.SetServer(RunEchoServer);
            echo.SetClient(RunEchoClient);
            
            // Factorial
            var factorialSetting = new UniNettyExampleSetting(UniNettyExampleType.Factorial);
            factorialSetting.SetIp(ip);
            factorialSetting.SetPort(8020);
            factorialSetting.SetSize(256);
            factorialSetting.SetCount(100);
            factorialSetting.SetCertification(cert);
            
            var factorial = new UniNettyExample();
            factorial.SetSetting(factorialSetting);
            factorial.SetServer(RunFactorialServer);
            factorial.SetClient(RunFactorialClient);
            
            // QuoteOfTheMoment
            var quoteOfTheMomentSetting = new UniNettyExampleSetting(UniNettyExampleType.QuoteOfTheMoment);
            quoteOfTheMomentSetting.SetIp(ip);
            quoteOfTheMomentSetting.SetPort(8030);
            quoteOfTheMomentSetting.SetCertification(cert);
            
            var quoteOfTheMoment = new UniNettyExample();
            quoteOfTheMoment.SetSetting(quoteOfTheMomentSetting);
            quoteOfTheMoment.SetServer(QuoteOfTheMomentServer);
            quoteOfTheMoment.SetClient(RunQuoteOfTheMomentClient);
            
            // SecureChat
            var secureChatSetting = new UniNettyExampleSetting(UniNettyExampleType.SecureChat);
            secureChatSetting.SetIp(ip);
            secureChatSetting.SetPort(8040);
            secureChatSetting.SetCertification(cert);
            
            var secureChat = new UniNettyExample();
            secureChat.SetSetting(secureChatSetting);
            secureChat.SetServer(RunSecureChatServer);
            secureChat.SetClient(RunSecureChatClient);
            
            // telnet
            var telnetSetting = new UniNettyExampleSetting(UniNettyExampleType.Telnet);
            telnetSetting.SetIp(ip);
            telnetSetting.SetPort(8050);
            telnetSetting.SetSize(256);
            telnetSetting.SetCertification(cert);
            
            var telnet = new UniNettyExample();
            telnet.SetSetting(telnetSetting);
            telnet.SetServer(RunTelnetServer);
            telnet.SetClient(RunTelnetClient);
            
            // websocket
            var webSocketSetting = new UniNettyExampleSetting(UniNettyExampleType.WebSocket);
            webSocketSetting.SetIp(ip);
            webSocketSetting.SetPort(8060);
            webSocketSetting.SetPath("/websocket");
            webSocketSetting.SetCertification(cert);
            
            var webSocket = new UniNettyExample();
            webSocket.SetSetting(webSocketSetting);
            webSocket.SetServer(RunWebSocketServer);
            webSocket.SetClient(RunWebSocketClient);
            
            // http
            var httpSetting = new UniNettyExampleSetting(UniNettyExampleType.HttpServer);
            httpSetting.SetIp(ip);
            httpSetting.SetPort(8080);
            httpSetting.SetCertification(cert);
            
            var http = new UniNettyExample();
            http.SetSetting(httpSetting);
            http.SetServer(RunHelloHttpServer);
            http.SetClient(RunHelloHttpClient);
            
            var examples = new Dictionary<UniNettyExampleType, UniNettyExample>();
            examples[UniNettyExampleType.None] = null;
            examples[UniNettyExampleType.Discard] = discard;
            examples[UniNettyExampleType.Echo] = echo;
            examples[UniNettyExampleType.Factorial] = factorial;
            examples[UniNettyExampleType.QuoteOfTheMoment] = quoteOfTheMoment;
            examples[UniNettyExampleType.SecureChat] = secureChat;
            examples[UniNettyExampleType.Telnet] = telnet;
            examples[UniNettyExampleType.WebSocket] = webSocket;
            examples[UniNettyExampleType.HttpServer] = http;

            return examples;
        }
    }
}