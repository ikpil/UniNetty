// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs
{
    using System;

    public class MessageAggregationException : InvalidOperationException
    {
        public MessageAggregationException(string message)
            : base(message)
        {
        }

        public MessageAggregationException(string message, Exception cause)
            : base(message, cause)
        {
        }
    }
}
