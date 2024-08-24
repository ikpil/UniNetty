namespace UniNetty.Examples.Demo.UI;

public interface IView
{
    void Bind(Canvas canvas);
    void Update(double dt);
    void Draw(double dt);
}