using System.Collections.Immutable;
using System.Numerics;
using UniNetty.Examples.DemoSupports;

namespace UniNetty.Examples.Demo.UI;

public class Canvas
{
    public Vector2 Size;
    private ImmutableArray<IView> _view;

    public readonly ExampleContext Context;

    public Canvas(ExampleContext context)
    {
        Context = context;
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
    
    public void Update(double dt)
    {
        foreach (var view in _view)
        {
            view.Update(dt);
        }
    }


    public void Draw(double dt)
    {
        foreach (var view in _view)
        {
            view.Draw(dt);
        }
    }
}