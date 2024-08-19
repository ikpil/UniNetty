// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Examples.HttpServer
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using UniNetty.Codecs.Http;
    using UniNetty.Common;
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

            // test
            ResourceLeakDetector.Level = ResourceLeakDetector.DetectionLevel.Disabled;

            Console.WriteLine(
                $"\n{RuntimeInformation.OSArchitecture} {RuntimeInformation.OSDescription}"
                + $"\n{RuntimeInformation.ProcessArchitecture} {RuntimeInformation.FrameworkDescription}"
                + $"\nProcessor Count : {Environment.ProcessorCount}\n");

            Console.WriteLine("Transport type : Socket");

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
            }

            Console.WriteLine($"Server garbage collection: {GCSettings.IsServerGC}");
            Console.WriteLine($"Current latency mode for garbage collection: {GCSettings.LatencyMode}");

            var server = new HttpServer();
            server.RunServerAsync(ServerSettings.Cert, ServerSettings.Port).Wait();
        }
    }
}