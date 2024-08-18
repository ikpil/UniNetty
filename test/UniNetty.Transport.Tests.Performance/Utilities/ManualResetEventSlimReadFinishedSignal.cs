// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Tests.Performance.Utilities
{
    using System.Threading;

    public class ManualResetEventSlimReadFinishedSignal : IReadFinishedSignal
    {
        readonly ManualResetEventSlim manualResetEventSlim;

        public ManualResetEventSlimReadFinishedSignal(ManualResetEventSlim manualResetEventSlim)
        {
            this.manualResetEventSlim = manualResetEventSlim;
        }

        public void Signal() => this.manualResetEventSlim.Set();

        public bool Finished => this.manualResetEventSlim.IsSet;
    }
}