// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs
{
    using System;

    public class EncoderException : CodecException
    {
        public EncoderException(string message)
            : base(message)
        {
        }

        public EncoderException(Exception innerException)
            : base(null, innerException)
        {
        }

        public EncoderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}