using System;

namespace UniNetty.Examples.DemoSupports
{
    public class ExampleSetting
    {
        public readonly ExampleType Example;
        public int Port;

        private Action<ExampleSetting> _runServer;
        private Action<ExampleSetting> _runClient;

        public ExampleSetting(ExampleType example)
        {
            Example = example;
            Port = example.Port;
        }

        public void Set(Action<ExampleSetting> runServer, Action<ExampleSetting> runClient)
        {
            _runServer = runServer;
            _runClient = runClient;
        }

        public void RunServer()
        {
            _runServer?.Invoke(this);
        }

        public void RunClient()
        {
            _runClient?.Invoke(this);
        }
    }
}