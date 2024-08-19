// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

namespace UniNetty.Examples.Common
{
    public static class ServerSettings
    {
        public static bool IsSsl
        {
            get
            {
                string ssl = ExampleHelper.Configuration["ssl"];
                return !string.IsNullOrEmpty(ssl) && bool.Parse(ssl);
            }
        }

        public static X509Certificate2 Cert => !IsSsl
            ? null
            : ExampleHelper.LoadCertificate2();

        public static int Port => int.Parse(ExampleHelper.Configuration["port"]);
    }
}