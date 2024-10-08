// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Microbench.Http
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;
    using UniNetty.Codecs.Http.Cookies;
    using UniNetty.Common;

    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [BenchmarkCategory("Http")]
    public class ClientCookieDecoderBenchmark
    {
        const string CookieString = 
            "__Host-user_session_same_site=fgfMsM59vJTpZg88nxqKkIhgOt0ADF8LX8wjMMbtcb4IJMufWCnCcXORhbo9QMuyiybdtx; " 
            + "path=/; expires=Mon, 28 Nov 2016 13:56:01 GMT; secure; HttpOnly";

        [GlobalSetup]
        public void GlobalSetup()
        {
            ResourceLeakDetector.Level = ResourceLeakDetector.DetectionLevel.Disabled;
        }

        [Benchmark]
        public ICookie DecodeCookieWithRfc1123ExpiresField() => ClientCookieDecoder.StrictDecoder.Decode(CookieString);
    }
}
