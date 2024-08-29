using System.Linq;
using UniNetty.Examples.DemoSupports;

namespace UniNetty.Examples.Demo.UI;

public class ExamplesViewModel
{
    public ExampleSetting[] Examples;

    public ExamplesViewModel(ExampleContext context)
    {
        Examples = ExampleType.Values
            .Select(x => new ExampleSetting(context, x))
            .ToArray();
    }
}