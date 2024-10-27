// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Compression
{
    using System;

    public class DecompressionException : DecoderException
    {
        public DecompressionException(string message) 
            : base(message)
        {
        }

        public DecompressionException(string message, Exception exception) 
            : base(message, exception)
        {
        }
    }
}
