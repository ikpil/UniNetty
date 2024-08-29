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
        // discard
        var discard = new ExampleSetting(ExampleType.Discard);
        discard.Set(context.RunDiscardServer, context.RunDiscardClient);
        discard.SetIp(ip);
        discard.SetPort(8000);
        discard.SetSize(256);

        // echo
        var echo = new ExampleSetting(ExampleType.Echo);
        echo.Set(context.RunEchoServer, context.RunEchoClient);
        echo.SetIp(ip);
        echo.SetPort(8010);
        echo.SetSize(256);

        // factorial
        var factorial = new ExampleSetting(ExampleType.Echo);
        factorial.Set(context.RunFactorialServer, context.RunFactorialClient);
        factorial.SetIp(ip);
        factorial.SetPort(8020);
        factorial.SetSize(256);
        factorial.SetCount(100);

        // http
        var http = new ExampleSetting(ExampleType.HttpServer);
        http.Set(context.RunHelloHttpServer, context.RunHelloHttpClient);
        http.SetIp(ip);
        http.SetPort(8080);


        //
        Examples[ExampleType.Discard.Index] = discard;
        Examples[ExampleType.Echo.Index] = echo;
        Examples[ExampleType.Factorial.Index] = factorial;
        Examples[ExampleType.HttpServer.Index] = http;
    }
}