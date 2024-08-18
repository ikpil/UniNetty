// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Tests.Performance.Utilities
{
    using UniNetty.Common.Concurrency;

    public class TaskCompletionSourceFinishedSignal : IReadFinishedSignal
    {
        readonly TaskCompletionSource tcs;

        public TaskCompletionSourceFinishedSignal(TaskCompletionSource tcs)
        {
            this.tcs = tcs;
        }

        public void Signal() => this.tcs.TryComplete();

        public bool Finished => this.tcs.Task.IsCompleted;
    }
}