using System;

namespace UniNetty.Examples.DemoSupports
{
    public class UniNettyExample
    {
        public UniNettyExampleSetting Setting { get; private set; }

        public bool IsRunningServer => null != _stopServer;
        private Func<UniNettyExampleSetting, IDisposable> _runServer;
        private IDisposable _stopServer;

        public bool IsRunningClient => null != _stopClient;
        private Func<UniNettyExampleSetting, IDisposable> _runClient;
        private IDisposable _stopClient;

        public void SetSetting(UniNettyExampleSetting setting)
        {
            Setting = setting;
        }

        public void SetServer(Func<UniNettyExampleSetting, IDisposable> runServer)
        {
            _runServer = runServer;
        }

        public void SetClient(Func<UniNettyExampleSetting, IDisposable> runClient)
        {
            _runClient = runClient;
        }
        
        public void Stop()
        {
            _stopServer?.Dispose();
            _stopServer = null;
            
            _stopClient?.Dispose();
            _stopClient = null;
        }

        public void ToggleServer()
        {
            if (null == _stopServer)
            {
                _stopServer = _runServer?.Invoke(Setting);
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
                _stopClient = _runClient?.Invoke(Setting);
            }
            else
            {
                _stopClient.Dispose();
                _stopClient = null;
            }
        }
    }
}