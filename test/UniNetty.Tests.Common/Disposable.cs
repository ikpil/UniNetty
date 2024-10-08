// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Tests.Common
{
    using System;
    using System.Threading;

    public sealed class Disposable : IDisposable
    {
        Action disposeAction;

        public Disposable(Action disposeAction)
        {
            this.disposeAction = disposeAction;
        }

        public void Dispose()
        {
            Action action = Interlocked.Exchange(ref this.disposeAction, null);
            action?.Invoke();
        }
    }
}