// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Microbench.Codecs
{
    using System;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;
    using UniNetty.Codecs;

    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [BenchmarkCategory("Codecs")]
    public class DateFormatterBenchmark
    {
        const string DateString = "Sun, 27 Nov 2016 19:18:46 GMT";
        readonly DateTime date = new DateTime(784111777000L);

        [Benchmark]
        public DateTime? ParseHttpHeaderDateFormatter() => DateFormatter.ParseHttpDate(DateString);

        [Benchmark]
        public string FormatHttpHeaderDateFormatter() => DateFormatter.Format(this.date);
    }
}
