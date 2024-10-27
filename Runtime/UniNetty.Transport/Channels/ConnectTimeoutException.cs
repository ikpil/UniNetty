// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Channels
{
    using System.IO;

    public class ConnectTimeoutException : IOException
    {
        public ConnectTimeoutException(string message)
            : base(message)
        {
        }

        public ConnectTimeoutException()
        {
        }
    }
}