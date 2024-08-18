// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Handlers.Timeout
{
    public sealed class WriteTimeoutException : TimeoutException
    {
        public readonly static WriteTimeoutException Instance = new WriteTimeoutException();

        public WriteTimeoutException(string message)
            :base(message)
        {
        }

        public WriteTimeoutException()
        {
        }
    }
}

