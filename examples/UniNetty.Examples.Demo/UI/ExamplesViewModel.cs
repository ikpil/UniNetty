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
        discard.Set(context.RunDiscardServer, context.RunDiscardClient);
        discard.SetIp(ip);
        discard.SetPort(8000);
        discard.SetSize(256);

        // Echo
        var echo = new ExampleSetting(ExampleType.Echo);
        echo.Set(context.RunEchoServer, context.RunEchoClient);
        echo.SetIp(ip);
        echo.SetPort(8010);
        echo.SetSize(256);

        // Factorial
        var factorial = new ExampleSetting(ExampleType.Factorial);
        factorial.Set(context.RunFactorialServer, context.RunFactorialClient);
        factorial.SetIp(ip);
        factorial.SetPort(8020);
        factorial.SetSize(256);
        factorial.SetCount(100);

        // QuoteOfTheMoment
        var quoteOfTheMoment = new ExampleSetting(ExampleType.QuoteOfTheMoment);
        quoteOfTheMoment.Set(context.RunFactorialServer, context.RunFactorialClient);
        quoteOfTheMoment.SetIp(ip);
        quoteOfTheMoment.SetPort(8030);

        // SecureChat
        var secureChat = new ExampleSetting(ExampleType.SecureChat);
        secureChat.Set(context.RunFactorialServer, context.RunFactorialClient);
        secureChat.SetIp(ip);
        secureChat.SetPort(8040);

        // telnet
        var telnet = new ExampleSetting(ExampleType.Telnet);
        telnet.Set(context.RunFactorialServer, context.RunFactorialClient);
        telnet.SetIp(ip);
        telnet.SetPort(8050);
        telnet.SetSize(256);

        var webSocket = new ExampleSetting(ExampleType.WebSocket);
        webSocket.Set(context.RunFactorialServer, context.RunFactorialClient);
        webSocket.SetIp(ip);
        webSocket.SetPort(8060);
        webSocket.SetPath("/websocket");

        // http
        var http = new ExampleSetting(ExampleType.HttpServer);
        http.Set(context.RunHelloHttpServer, context.RunHelloHttpClient);
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