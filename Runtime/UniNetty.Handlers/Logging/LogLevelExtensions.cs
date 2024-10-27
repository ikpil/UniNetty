// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Handlers.Logging
{
    using UniNetty.Common.Internal.Logging;

    public static class LogLevelExtensions
    {
        public static InternalLogLevel ToInternalLevel(this LogLevel level) => (InternalLogLevel)level;
    }
}