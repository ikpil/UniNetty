// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Microbench.Concurrency
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;
    using UniNetty.Common;

    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [BenchmarkCategory("Concurrency")]
    public class FastThreadLocalBenchmark
    {
        ThreadLocalArray threadLocalArray;

        [GlobalSetup]
        public void GlobalSetup()
        {
            this.threadLocalArray = new ThreadLocalArray(new byte[128]);
        }

        [Benchmark]
        public byte[] Get() => this.threadLocalArray.Value;

        [GlobalCleanup]
        public void GlobalCleanup() => FastThreadLocal.Destroy();

        sealed class ThreadLocalArray : FastThreadLocal<byte[]>
        {
            readonly byte[] array;

            public ThreadLocalArray(byte[] array)
            {
                this.array = array;
            }

            protected override byte[] GetInitialValue() => this.array;
        }
    }
}
