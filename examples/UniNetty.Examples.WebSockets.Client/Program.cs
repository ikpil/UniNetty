// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Examples.WebSockets.Client
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using UniNetty.Buffers;
    using UniNetty.Codecs.Http;
    using UniNetty.Codecs.Http.WebSockets;
    using UniNetty.Codecs.Http.WebSockets.Extensions.Compression;
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

            var client = new WebSocketClient();
            client.RunClientAsync(ClientSettings.Cert, ClientSettings.Host, ClientSettings.Port, ExampleHelper.Configuration["path"]).Wait();
        }
    }
}