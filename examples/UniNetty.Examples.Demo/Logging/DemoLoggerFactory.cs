using UniNetty.Logging;

namespace UniNetty.Examples.Demo.Logging;

public class DemoLoggerFactory : ILoggerFactory
{
    public ILogger CreateLogger(string categoryName)
    {
        var l = Serilog.Log.ForContext("Name", categoryName);
        return new DemoLogger(l);
    }

    public void AddProvider(ILoggerProvider provider)
    {
        // ...
    }

    public void Dispose()
    {
        // ...
    }
}