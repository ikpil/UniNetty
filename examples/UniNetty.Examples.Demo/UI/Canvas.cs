using System.Collections.Immutable;

namespace UniNetty.Examples.Demo.UI;

public class Canvas
{
    private ImmutableArray<IView> _view;

    public Canvas()
    {
        _view = ImmutableArray<IView>.Empty;
    }
    
    public void AddView(IView view)
    {
        _view = _view.Add(view);
    }
    
    public void Draw(double dt)
    {
        foreach (var view in _view)
        {
            view.Draw(dt);
        }
    }
}