using System;
using System.Security.Cryptography.X509Certificates;

namespace UniNetty.Examples.DemoSupports
{
    public class UniNettyExampleSetting
    {
        public readonly UniNettyExampleType Example;

        public string Ip { get; private set; }
        public int Port { get; private set; }
        public int Size { get; private set; }
        public int Count { get; private set; }
        public string Path { get; private set; }
        public X509Certificate2 Cert { get; private set; }

        public UniNettyExampleSetting(UniNettyExampleType example)
        {
            Example = example;
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

        public void SetCertification(X509Certificate2 cert)
        {
            Cert = cert;
        }
    }
}