using System;

namespace UniNetty.Examples.DemoSupports
{
    public class ExampleSetting
    {
        public readonly ExampleType Example;

        public string Ip { get; private set; }
        public int Port { get; private set; }
        public int Size { get; private set; }
        public int Count { get; private set; }
        public string Path { get; private set; }

        public bool IsRunningServer => null != _stopServer;
        private Func<ExampleSetting, IDisposable> _runServer;
        private IDisposable _stopServer;

        public bool IsRunningClient => null != _stopClient;
        private Func<ExampleSetting, IDisposable> _runClient;
        private IDisposable _stopClient;

        public ExampleSetting(ExampleType example)
        {
            Example = example;
        }

        public void SetServer(Func<ExampleSetting, IDisposable> runServer)
        {
            _runServer = runServer;
        }

        public void SetClient(Func<ExampleSetting, IDisposable> runClient)
        {
            _runClient = runClient;
        }

        public void SetIp(string ip)
        {
            Ip = ip;
        }

        public void SetPort(int port)
        {
            Port = port;
        }

        public void SetSize(int size)
        {
            Size = size;
        }

        public void SetCount(int count)
        {
            Count = count;
        }

        public void SetPath(string path)
        {
            Path = path;
        }

        public void ToggleServer()
        {
            if (null == _stopServer)
            {
                _stopServer = _runServer?.Invoke(this);
            }
            else
            {
                _stopServer.Dispose();
                _stopServer = null;
            }
        }

        public void ToggleClient()
        {
            if (null == _stopClient)
            {
                _stopClient = _runClient?.Invoke(this);
            }
            else
            {
                _stopClient.Dispose();
                _stopClient = null;
            }
        }
    }
}