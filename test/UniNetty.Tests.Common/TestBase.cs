// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Tests.Common
{
    using UniNetty.Common.Internal.Logging;
    using Xunit.Abstractions;

    public abstract class TestBase
    {
        protected readonly ITestOutputHelper Output;

        protected TestBase(ITestOutputHelper output)
        {
            this.Output = output;
            InternalLoggerFactory.DefaultFactory.AddProvider(new XUnitOutputLoggerProvider(output));
        }
    }
}