// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using UniNetty.Examples.Common;
using UniNetty.Examples.Discard.Client;
using UniNetty.Examples.Echo.Client;
using UniNetty.Examples.Factorial.Client;
using UniNetty.Examples.QuoteOfTheMoment.Client;
using UniNetty.Examples.SecureChat.Client;
using UniNetty.Examples.Telnet.Client;
using UniNetty.Examples.WebSockets.Client;

namespace UniNetty.Examples.Demo.Client;

class Program
{
    static void Main()
    {
        ExampleHelper.SetConsoleLogger();

        RunWebSocketClient();
        RunTelentClient();
        RunSecureChatClient();
        RunQuoteOfTheMomentClient();
        RunFactorialClient();
        RunEchoClient();
    }

    static void RunWebSocketClient()
    {
        var client = new WebSocketClient();
        client.RunClientAsync(ClientSettings.Cert, ClientSettings.Host, ClientSettings.Port, ExampleHelper.Configuration["path"]).Wait();
    }

    static void RunTelentClient()
    {
        var client = new TelnetClient();
        client.RunClientAsync(ClientSettings.Cert, ClientSettings.Host, ClientSettings.Port).Wait();
    }

    static void RunSecureChatClient()
    {
        var client = new SecureChatClient();
        client.RunClientAsync(ClientSettings.Cert, ClientSettings.Host, ClientSettings.Port).Wait();
    }

    static void RunQuoteOfTheMomentClient()
    {
        var client = new QuoteOfTheMomentClient();
        client.RunClientAsync(ClientSettings.Port).Wait();
    }

    static void RunFactorialClient()
    {
        var client = new FactorialClient();
        client.RunClientAsync(ClientSettings.Cert, ClientSettings.Host, ClientSettings.Port, ClientSettings.Count).Wait();
    }

    static void RunEchoClient()
    {
        var client = new EchoClient();
        client.RunClientAsync(ClientSettings.Cert, ClientSettings.Host, ClientSettings.Port, ClientSettings.Size).Wait();
    }

    public static void RunDiscardClient()
    {
        var client = new DiscardClient();
        client.RunClientAsync(ClientSettings.Cert, ClientSettings.Host, ClientSettings.Port, ClientSettings.Size).Wait();
    }
}