// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace UniNetty.Examples.Common
{
    using System;
    using UniNetty.Common.Internal.Logging;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public static class ExampleHelper
    {
        static ExampleHelper()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(ProcessDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static string ProcessDirectory
        {
            get
            {
#if NETSTANDARD2_0 || NETCOREAPP3_1_OR_GREATER || NET5_0_OR_GREATER
                return AppContext.BaseDirectory;
#else
                return AppDomain.CurrentDomain.BaseDirectory;
#endif
            }
        }

        public static IConfigurationRoot Configuration { get; }

        public static void SetConsoleLogger()
        {
            InternalLoggerFactory.DefaultFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                var logConf = Configuration.GetSection("Logging");
                if (logConf.Exists())
                {
                    builder.AddConfiguration(logConf);
                }
            });
        }

        public static X509Certificate2 LoadCertificate2()
        {
            var pfx = Path.Combine(ExampleHelper.ProcessDirectory, "dotnetty.com.pfx");
            return new X509Certificate2(pfx, "password");
        }
    }
}