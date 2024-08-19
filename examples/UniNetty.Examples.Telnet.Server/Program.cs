// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Examples.Telnet.Server
{
    using System;
    using System.IO;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using UniNetty.Codecs;
    using UniNetty.Handlers.Logging;
    using UniNetty.Handlers.Tls;
    using UniNetty.Transport.Bootstrapping;
    using UniNetty.Transport.Channels;
    using UniNetty.Transport.Channels.Sockets;
    using UniNetty.Examples.Common;

    class Program
    {


        static void Main()
        {
            ExampleHelper.SetConsoleLogger();

            var server = new TelnetServer();
            server.RunServerAsync(ServerSettings.Cert, ServerSettings.Port).Wait();
        }
    }
}