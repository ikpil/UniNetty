using UniNetty.Examples.Common;
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
}