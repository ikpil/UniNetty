// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Microbench
{
    using System;
    using BenchmarkDotNet.Running;
    using UniNetty.Microbench.Allocators;
    using UniNetty.Microbench.Buffers;
    using UniNetty.Microbench.Codecs;
    using UniNetty.Microbench.Common;
    using UniNetty.Microbench.Concurrency;
    using UniNetty.Microbench.Headers;
    using UniNetty.Microbench.Http;
    using UniNetty.Microbench.Internal;

    class Program
    {
        static readonly Type[] BenchmarkTypes =
        {
            typeof(PooledHeapByteBufferAllocatorBenchmark),
            typeof(UnpooledHeapByteBufferAllocatorBenchmark),

            typeof(ByteBufferBenchmark),
            typeof(PooledByteBufferBenchmark),
            typeof(UnpooledByteBufferBenchmark),
            typeof(PooledByteBufferBenchmark),
            typeof(ByteBufUtilBenchmark),

            typeof(DateFormatterBenchmark),

            typeof(AsciiStringBenchmark),

            typeof(FastThreadLocalBenchmark),
            typeof(SingleThreadEventExecutorBenchmark),

            typeof(HeadersBenchmark),

            typeof(ClientCookieDecoderBenchmark),
            typeof(HttpRequestDecoderBenchmark),
            typeof(HttpRequestEncoderInsertBenchmark),
            typeof(WriteBytesVsShortOrMediumBenchmark),

            typeof(PlatformDependentBenchmark)
        };

        static void Main(string[] args)
        {
            var switcher = new BenchmarkSwitcher(BenchmarkTypes);

            if (args == null || args.Length == 0)
            {
                switcher.RunAll();
            }
            else
            {
                switcher.Run(args);
            }
        }
    }
}
