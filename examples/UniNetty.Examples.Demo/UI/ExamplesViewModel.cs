using System.Linq;
using UniNetty.Examples.DemoSupports;

namespace UniNetty.Examples.Demo.UI;

public class ExamplesViewModel
{
    public readonly ExampleSetting[] Examples;

    public ExamplesViewModel(ExampleContext context)
    {
        Examples = new ExampleSetting[ExampleType.Values.Count];

        Examples[ExampleType.Discard.Index] = new ExampleSetting(ExampleType.Discard);
        Examples[ExampleType.Discard.Index].Set(context.RunDiscardServer, context.RunDiscardClient);
    }
}