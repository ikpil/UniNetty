// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs
{
    using System;

    public class PrematureChannelClosureException : CodecException
    {
        public PrematureChannelClosureException(string message)
            : this(message, null)
        {
        }

        public PrematureChannelClosureException(Exception exception)
            : this(null, exception)
        {
        }

        public PrematureChannelClosureException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
