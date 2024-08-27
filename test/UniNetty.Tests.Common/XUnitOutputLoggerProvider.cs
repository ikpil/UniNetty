// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Tests.Common
{
    using UniNetty.Logging;
    using Xunit.Abstractions;

    sealed class XUnitOutputLoggerProvider : ILoggerProvider
    {
        readonly ITestOutputHelper output;

        public XUnitOutputLoggerProvider(ITestOutputHelper output)
        {
            this.output = output;
        }

        public void Dispose()
        {
        }
        
        public ILogger CreateLogger(string categoryName) => new XUnitOutputLogger(categoryName, this.output);
    }
}