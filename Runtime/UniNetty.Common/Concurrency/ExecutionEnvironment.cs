// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Common.Concurrency
{
    using System;

    public static class ExecutionEnvironment
    {
        [ThreadStatic]
        static IEventExecutor currentExecutor;

        public static bool TryGetCurrentExecutor(out IEventExecutor executor)
        {
            executor = currentExecutor;
            return executor != null;
        }

        internal static void SetCurrentExecutor(IEventExecutor executor) => currentExecutor = executor;
    }
}