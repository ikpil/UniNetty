// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace UniNetty.Tests.Common
{
    using System.IO;
    using System.Security.Cryptography.X509Certificates;

    public static class TestResourceHelper
    {
        public static X509Certificate2 GetCertificate(string pfxFileName, string password)
        {
            var pfx = Path.Combine(AppContext.BaseDirectory, "resources", pfxFileName);
            using FileStream fs = new FileStream(pfx, FileMode.Open, FileAccess.Read, FileShare.Read);
            using MemoryStream ms = new MemoryStream();
            fs.CopyTo(ms);
            var raw = ms.ToArray();
            var cert = new X509Certificate2(raw, password);
            return cert;
        }

        public static X509Certificate2 GetTestCertificate()
        {
            return GetCertificate("uninetty.com.pfx", "password");
        }

        public static X509Certificate2 GetTestCertificate2()
        {
            return GetCertificate("contoso.com.pfx", "password");
        }
    }
}