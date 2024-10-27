// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Serilog;
using UniNetty.Common.Internal.Logging;
using UniNetty.Examples.Demo.Logging;
using UniNetty.Examples.Demo.Logging.Sinks;
using UniNetty.Examples.DemoSupports;

namespace UniNetty.Examples.Demo;

public static class Program
{
    private static void InitializeLogger()
    {
        var format = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj} [{ThreadName}:{ThreadId}]{NewLine}{Exception}";
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .WriteTo.Async(c => c.LogMessageBroker(outputTemplate: format))
            .WriteTo.Async(c => c.Console(outputTemplate: format))
            .WriteTo.Async(c => c.File(
                "logs/log.log",
                rollingInterval: RollingInterval.Hour,
                rollOnFileSizeLimit: true,
                retainedFileCountLimit: null,
                outputTemplate: format)
            )
            .CreateLogger();

        InternalLoggerFactory.DefaultFactory = new DemoLoggerFactory();
    }

    private static void InitializeWorkingDirectory()
    {
        var path = DirectoryUtils.SearchFile("LICENSE");
        if (!string.IsNullOrEmpty(path))
        {
            var workingDirectory = Path.GetDirectoryName(path) ?? string.Empty;
            workingDirectory = Path.GetFullPath(workingDirectory);
            Directory.SetCurrentDirectory(workingDirectory);
        }
    }
    
    public static void Main(string[] args)
    {
        Thread.CurrentThread.Name ??= "main";
        InitializeWorkingDirectory();
        InitializeLogger();

        var demo = new UniNettyDemo();
        demo.Initialize();
        demo.Start();
    }
}