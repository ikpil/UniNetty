// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.Multipart
{
    using System;

    public class ErrorDataEncoderException : Exception
    {
        public ErrorDataEncoderException(string message)
            : base(message)
        {
        }
        public ErrorDataEncoderException(Exception innerException)
            : base(null, innerException)
        {
        }

        public ErrorDataEncoderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
