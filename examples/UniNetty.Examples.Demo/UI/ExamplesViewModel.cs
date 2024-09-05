using System.Linq;
using UniNetty.Examples.DemoSupports;

namespace UniNetty.Examples.Demo.UI;

public class ExamplesViewModel
{
    public readonly ExampleSetting[] Examples;

    public ExamplesViewModel(ExampleContext context)
    {
        Examples = new ExampleSetting[ExampleType.Values.Count];

        Examples[ExampleType.None.Index] = null;

        var ip = ExampleSupport.Shared.GetPrivateIp();

        // Discard
        var discard = new ExampleSetting(ExampleType.Discard);
        discard.SetServer(context.RunDiscardServer);
        discard.SetClient(context.RunDiscardClient);
        discard.SetIp(ip);
        discard.SetPort(8000);
        discard.SetSize(256);

        // Echo
        var echo = new ExampleSetting(ExampleType.Echo);
        echo.SetServer(context.RunEchoServer);
        echo.SetClient(context.RunEchoClient);
        echo.SetIp(ip);
        echo.SetPort(8010);
        echo.SetSize(256);

        // Factorial
        var factorial = new ExampleSetting(ExampleType.Factorial);
        factorial.SetServer(context.RunFactorialServer);
        factorial.SetClient(context.RunFactorialClient);
        factorial.SetIp(ip);
        factorial.SetPort(8020);
        factorial.SetSize(256);
        factorial.SetCount(100);

        // QuoteOfTheMoment
        var quoteOfTheMoment = new ExampleSetting(ExampleType.QuoteOfTheMoment);
        quoteOfTheMoment.SetServer(context.QuoteOfTheMomentServer);
        quoteOfTheMoment.SetClient(context.RunQuoteOfTheMomentClient);
        quoteOfTheMoment.SetIp(ip);
        quoteOfTheMoment.SetPort(8030);

        // SecureChat
        var secureChat = new ExampleSetting(ExampleType.SecureChat);
        secureChat.SetServer(context.RunSecureChatServer);
        secureChat.SetClient(context.RunSecureChatClient);
        secureChat.SetIp(ip);
        secureChat.SetPort(8040);

        // telnet
        var telnet = new ExampleSetting(ExampleType.Telnet);
        telnet.SetServer(context.RunTelnetServer);
        telnet.SetClient(context.RunTelnetClient);
        telnet.SetIp(ip);
        telnet.SetPort(8050);
        telnet.SetSize(256);

        var webSocket = new ExampleSetting(ExampleType.WebSocket);
        webSocket.SetServer(context.RunWebSocketServer);
        webSocket.SetClient(context.RunWebSocketClient);
        webSocket.SetIp(ip);
        webSocket.SetPort(8060);
        webSocket.SetPath("/websocket");

        // http
        var http = new ExampleSetting(ExampleType.HttpServer);
        http.SetServer(context.RunHelloHttpServer);
        http.SetClient(context.RunHelloHttpClient);
        http.SetIp(ip);
        http.SetPort(8080);


        //
        Examples[ExampleType.Discard.Index] = discard;
        Examples[ExampleType.Echo.Index] = echo;
        Examples[ExampleType.Factorial.Index] = factorial;
        Examples[ExampleType.QuoteOfTheMoment.Index] = quoteOfTheMoment;
        Examples[ExampleType.SecureChat.Index] = secureChat;
        Examples[ExampleType.Telnet.Index] = telnet;
        Examples[ExampleType.WebSocket.Index] = webSocket;
        Examples[ExampleType.HttpServer.Index] = http;
    }
}