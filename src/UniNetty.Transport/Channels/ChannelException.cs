// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Channels
{
    using System;

    public class ChannelException : Exception
    {
        public ChannelException()
        {
        }

        public ChannelException(string message)
            : base(message)
        {
        }

        public ChannelException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ChannelException(Exception innerException)
            : base(null, innerException)
        {
        }
    }
}