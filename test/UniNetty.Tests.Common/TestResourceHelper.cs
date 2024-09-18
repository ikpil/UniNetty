// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace UniNetty.Tests.Common
{
    using System.IO;
    using System.Reflection;
    using System.Security.Cryptography.X509Certificates;

    public static class TestResourceHelper
    {
        public static X509Certificate2 GetTestCertificate()
        {
            var pfx = Path.Combine(AppContext.BaseDirectory, "resources", "dotnetty.com.pfx");
            var cert = new X509Certificate2(pfx, "password");
            return cert;
        }

        public static X509Certificate2 GetTestCertificate2()
        {
            var pfx = Path.Combine(AppContext.BaseDirectory, "resources", "contoso.com.pfx");
            var cert = new X509Certificate2(pfx, "password");
            return cert;
        }
    }
}