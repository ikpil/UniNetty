// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using UniNetty.Examples.Common;

namespace UniNetty.Examples.Discard.Server
{
    public static class Program
    {
        public static void Main()
        {
            ExampleHelper.SetConsoleLogger();
            var server = new DiscardServer();
            server.RunServerAsync(ServerSettings.Cert, ServerSettings.Port).Wait();
        }
    }
}