// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Microbench.Http
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;
    using UniNetty.Buffers;
    using UniNetty.Codecs.Http;
    using UniNetty.Common;

    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [BenchmarkCategory("Http")]
    public class HttpRequestEncoderInsertBenchmark
    {
        string uri;
        HttpRequestEncoder encoder;

        [GlobalSetup]
        public void GlobalSetup()
        {
            ResourceLeakDetector.Level = ResourceLeakDetector.DetectionLevel.Disabled;
            this.uri = "http://localhost?eventType=CRITICAL&from=0&to=1497437160327&limit=10&offset=0";
            this. encoder = new HttpRequestEncoder();
        }

        [Benchmark]
        public IByteBuffer EncodeInitialLine()
        {
            IByteBuffer buffer = Unpooled.Buffer(100);
            try
            {
                this.encoder.EncodeInitialLine(buffer, new DefaultHttpRequest(HttpVersion.Http11, 
                    HttpMethod.Get,this.uri));
                return buffer;
            }
            finally
            {
                buffer.Release();
            }
        }
    }
}
