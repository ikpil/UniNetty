using UniNetty.Examples.DemoSupports;

namespace UniNetty.Examples.Demo.UI.Primitives;

public class ExampleSetting
{
    public ExampleType Example;
    public int Port;

    public ExampleSetting(ExampleType example)
    {
        Example = example;
        Port = example.Port;
    }
}

