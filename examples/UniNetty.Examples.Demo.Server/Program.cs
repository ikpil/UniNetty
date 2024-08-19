using System;
using System.Runtime;
using System.Runtime.InteropServices;
using UniNetty.Common;
using UniNetty.Examples.Common;
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
}