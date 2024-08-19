using UniNetty.Examples.Common;
using UniNetty.Examples.WebSockets.Client;

namespace UniNetty.Examples.Demo.Client;

class Program
{
    static void Main()
    {
        ExampleHelper.SetConsoleLogger();

        var client = new WebSocketClient();
        client.RunClientAsync(ClientSettings.Cert, ClientSettings.Host, ClientSettings.Port, ExampleHelper.Configuration["path"]).Wait();
    }
    

}