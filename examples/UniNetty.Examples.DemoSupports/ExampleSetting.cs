﻿using System;

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

        private Func<ExampleSetting, IDisposable> _runServer;
        private Func<ExampleSetting, IDisposable> _runClient;

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