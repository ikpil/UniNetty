// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Handlers.Timeout
{
    public sealed class ReadTimeoutException : TimeoutException
    {
        public readonly static ReadTimeoutException Instance = new ReadTimeoutException();

        public ReadTimeoutException(string message)
            :base(message)
        {
        }

        public ReadTimeoutException()
        {
        }
    }
}

