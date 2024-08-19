// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Examples.Echo.Client
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using UniNetty.Codecs;
    using UniNetty.Handlers.Logging;
    using UniNetty.Handlers.Tls;
    using UniNetty.Transport.Bootstrapping;
    using UniNetty.Transport.Channels;
    using UniNetty.Transport.Channels.Sockets;
    using UniNetty.Examples.Common;

    public class Program
    {
        static void Main()
        {
            ExampleHelper.SetConsoleLogger();
            var client = new EchoClient();
            client.RunClientAsync(ClientSettings.Cert, ClientSettings.Host, ClientSettings.Port).Wait();
        }
    }
}