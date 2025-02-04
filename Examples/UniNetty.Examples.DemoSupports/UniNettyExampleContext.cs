using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;


namespace UniNetty.Examples.DemoSupports
{
    public class UniNettyExampleContext
    {
        public X509Certificate2 Cert { get; private set; }

        public void SetCertificate(X509Certificate2 cert)
        {
            Cert = cert;
        }
    }
}