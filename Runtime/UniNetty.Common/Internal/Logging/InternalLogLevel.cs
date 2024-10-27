// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Common.Internal.Logging
{
    /// <summary>
    /// The log level that <see cref="IInternalLogger"/> can log at.
    /// </summary>
    public enum InternalLogLevel
    {
        /// <summary>
        ///     'TRACE' log level.
        /// </summary>
        TRACE,

        /// <summary>
        ///     'DEBUG' log level.
        /// </summary>
        DEBUG,

        /// <summary>
        ///     'INFO' log level.
        /// </summary>
        INFO,

        /// <summary>
        ///     'WARN' log level.
        /// </summary>
        WARN,

        /// <summary>
        ///     'ERROR' log level.
        /// </summary>
        ERROR
    }
}