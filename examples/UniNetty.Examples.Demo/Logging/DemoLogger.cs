using System;
using Serilog.Events;
using UniNetty.Logging;

namespace UniNetty.Examples.Demo.Logging;

public class DemoLogger : ILogger
{
    private readonly Serilog.ILogger _logger;

    public DemoLogger(Serilog.ILogger logger)
    {
        _logger = logger;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        var lv = FromLogLevel(logLevel);
        //throw new NotImplementedException();
        _logger.Write(lv, state?.ToString() ?? string.Empty);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        //throw new NotImplementedException();
        return true;
    }

    public LogEventLevel FromLogLevel(LogLevel logLevel)
    {
        switch (logLevel)
        {
            case LogLevel.Trace: return LogEventLevel.Verbose;
            case LogLevel.Debug: return LogEventLevel.Debug;
            case LogLevel.Information: return LogEventLevel.Information;
            case LogLevel.Warning: return LogEventLevel.Warning;
            case LogLevel.Error: return LogEventLevel.Error;
            case LogLevel.Critical: return LogEventLevel.Fatal;
            case LogLevel.None: return LogEventLevel.Verbose;
        }

        return LogEventLevel.Verbose;
    }
}