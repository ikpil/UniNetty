namespace UniNetty.Examples.DemoSupports
{
    public class ExampleSetting
    {
        public readonly ExampleContext _context;
        public readonly ExampleType Example;
        public int Port;

        public ExampleSetting(ExampleContext context, ExampleType example)
        {
            _context = context;
            Example = example;
            Port = example.Port;
        }

        public void RunServer()
        {
        }

        public void RunClient()
        {
        }
    }
}