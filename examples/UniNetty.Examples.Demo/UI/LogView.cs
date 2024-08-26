using Serilog;

namespace UniNetty.Examples.Demo.UI;

public class LogView : IView
{
    private static readonly ILogger Logger = Log.ForContext<LogView>();
    
    private readonly Canvas _canvas;
    
    public LogView(Canvas canvas)
    {
        _canvas = canvas;
    }

    public void Draw(double dt)
    {
    }
}