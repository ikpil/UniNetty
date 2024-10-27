// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Common.Concurrency
{
    using System;
    using System.Threading;

    sealed class ActionScheduledAsyncTask : ScheduledAsyncTask
    {
        readonly Action action;

        public ActionScheduledAsyncTask(AbstractScheduledEventExecutor executor, Action action, PreciseTimeSpan deadline, CancellationToken cancellationToken)
            : base(executor, deadline, new TaskCompletionSource(), cancellationToken)
        {
            this.action = action;
        }

        protected override void Execute() => this.action();
    }
}