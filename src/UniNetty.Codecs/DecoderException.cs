// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs
{
    using System;

    public class DecoderException : CodecException
    {
        public DecoderException(string message)
            : base(message)
        {
        }

        public DecoderException(Exception cause)
            : base(null, cause)
        {
        }

        public DecoderException(string message, Exception cause)
            : base(message, cause)
        {
        }
    }
}