using System.Collections.Immutable;
using System.Numerics;

namespace UniNetty.Examples.Demo.UI;

public class Canvas
{
    public Vector2 Size;
    private ImmutableArray<IView> _view;

    public Canvas()
    {
        _view = ImmutableArray<IView>.Empty;
    }

    public void ResetSize(Vector2 size)
    {
        Size = size;
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