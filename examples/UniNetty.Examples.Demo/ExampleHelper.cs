// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using UniNetty.Logging;
using UniNetty.Common.Internal.Logging;

namespace UniNetty.Examples.Demo
{
    public static class ExampleHelper
    {
        static ExampleHelper()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(ProcessDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static string ProcessDirectory => AppContext.BaseDirectory;

        public static IConfigurationRoot Configuration { get; }

        public static void SetConsoleLogger()
        {
            // InternalLoggerFactory.DefaultFactory = LoggerFactory.Create(builder =>
            // {
            //     builder.AddConsole();
            //     var logConf = Configuration.GetSection("Logging");
            //     if (logConf.Exists())
            //     {
            //         builder.AddConfiguration(logConf);
            //     }
            // });
        }

        public static X509Certificate2 LoadCertificate2()
        {
            var pfx = Path.Combine(ExampleHelper.ProcessDirectory, "dotnetty.com.pfx");
            return new X509Certificate2(pfx, "password");
        }
    }
}