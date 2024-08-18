// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Tests.Performance.Utilities
{
    public class SimpleReadFinishedSignal : IReadFinishedSignal
    {
        public void Signal()
        {
            this.Finished = true;
        }

        public bool Finished { get; private set; }
    }
}