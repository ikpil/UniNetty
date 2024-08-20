// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime;
using System.Runtime.InteropServices;
using UniNetty.Common;
using UniNetty.Examples.Common;
using UniNetty.Examples.Discard.Server;
using UniNetty.Examples.Echo.Server;
using UniNetty.Examples.Factorial.Server;
using UniNetty.Examples.HttpServer;
using UniNetty.Examples.QuoteOfTheMoment.Server;
using UniNetty.Examples.SecureChat.Server;
using UniNetty.Examples.Telnet.Server;
using UniNetty.Examples.WebSockets.Server;

namespace UniNetty.Examples.Demo.Server;

public static class Program
{
    static void Main()
    {
        ExampleHelper.SetConsoleLogger();

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

        Console.WriteLine($"Server garbage collection : {(GCSettings.IsServerGC ? "Enabled" : "Disabled")}");
        Console.WriteLine($"Current latency mode for garbage collection: {GCSettings.LatencyMode}");
        Console.WriteLine("\n");

        RunWebSocketServer();
        RunTelnetServer();
        RunSecureChatServer();
        QuoteOfTheMomentServer();
        RunHelloHttpServer();
        RunFactorialServer();
        RunEchoServer();
        RunDiscardServer();
    }

    static void RunWebSocketServer()
    {
        var server = new WebSocketServer();
        server.RunServerAsync(ServerSettings.Cert, ServerSettings.Port).Wait();
    }

    static void RunTelnetServer()
    {
        var server = new TelnetServer();
        server.RunServerAsync(ServerSettings.Cert, ServerSettings.Port).Wait();
    }

    static void RunSecureChatServer()
    {
        var server = new SecureChatServer();
        server.RunServerAsync(ServerSettings.Cert, ServerSettings.Port).Wait();
    }

    static void QuoteOfTheMomentServer()
    {
        var server = new QuoteOfTheMomentServer();
        server.RunServerAsync(ServerSettings.Port).Wait();
    }

    static void RunHelloHttpServer()
    {
        var server = new HelloHttpServer();
        server.RunServerAsync(ServerSettings.Cert, ServerSettings.Port).Wait();
    }

    static void RunFactorialServer()
    {
        var server = new FactorialServer();
        server.RunServerAsync(ServerSettings.Cert, ServerSettings.Port).Wait();
    }

    static void RunEchoServer()
    {
        var server = new EchoServer();
        server.RunServerAsync(ServerSettings.Cert, ServerSettings.Port).Wait();
    }

    static void RunDiscardServer()
    {
        var server = new DiscardServer();
        server.RunServerAsync(ServerSettings.Cert, ServerSettings.Port).Wait();
    }
}