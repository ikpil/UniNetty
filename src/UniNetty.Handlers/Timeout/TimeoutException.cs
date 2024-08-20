// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;

namespace UniNetty.Handlers.Timeout
{
    public class TimeoutException : IOException
    {
        public TimeoutException(string message)
            :base(message)
        {
        }

        public TimeoutException()
        {
        }
    }
}

