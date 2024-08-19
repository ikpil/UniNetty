using System;
using System.Runtime;
using System.Runtime.InteropServices;
using UniNetty.Common;
using UniNetty.Examples.Common;
using UniNetty.Examples.WebSockets.Server;

namespace UniNetty.Examples.Demo.Server;

class Program
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

        var server = new WebSocketServer();
        server.RunServerAsync(ServerSettings.Cert, ServerSettings.Port).Wait();
    }
}