using System.Linq;
using UniNetty.Examples.Demo.UI.Primitives;
using UniNetty.Examples.DemoSupports;

namespace UniNetty.Examples.Demo.UI;

public class ExamplesViewModel
{
    public ExampleSetting[] Examples;

    public ExamplesViewModel()
    {
        Examples = ExampleType.Values
            .Select(x => new ExampleSetting(x))
            .ToArray();
    }
}