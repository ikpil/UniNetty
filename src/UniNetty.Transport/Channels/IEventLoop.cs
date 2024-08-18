// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Channels
{
    using UniNetty.Common.Concurrency;

    /// <summary>
    /// <see cref="IEventExecutor"/> specialized to handle I/O operations of assigned <see cref="IChannel"/>s.
    /// </summary>
    public interface IEventLoop : IEventLoopGroup, IEventExecutor
    {
        /// <summary>
        /// Parent <see cref="IEventLoopGroup"/>.
        /// </summary>
        new IEventLoopGroup Parent { get; }
    }
}