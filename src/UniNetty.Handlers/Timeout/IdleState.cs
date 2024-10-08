// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Handlers.Timeout
{
    using System;

    /// <summary>
    /// An <see cref="Enum"/> that represents the idle state of a <see cref="UniNetty.Transport.Channels.IChannel"/>.
    /// </summary>
    public enum IdleState
    {
        /// <summary>
        /// No data was received for a while.
        /// </summary>
        ReaderIdle,

        /// <summary>
        /// No data was sent for a while.
        /// </summary>
        WriterIdle,

        /// <summary>
        /// No data was either received or sent for a while.
        /// </summary>
        AllIdle
    }
}

