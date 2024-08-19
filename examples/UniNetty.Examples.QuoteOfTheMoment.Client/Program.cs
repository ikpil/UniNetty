// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Examples.QuoteOfTheMoment.Client
{
    using System;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using UniNetty.Buffers;
    using UniNetty.Transport.Bootstrapping;
    using UniNetty.Transport.Channels;
    using UniNetty.Transport.Channels.Sockets;
    using UniNetty.Examples.Common;

    class Program
    {
  
        static void Main()
        {
            ExampleHelper.SetConsoleLogger();

            var client = new QuoteOfTheMomentClient();
            client.RunClientAsync(ClientSettings.Port).Wait();
        }
    }
}