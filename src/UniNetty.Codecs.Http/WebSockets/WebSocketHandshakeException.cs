// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.WebSockets
{
    using System;

    public class WebSocketHandshakeException : Exception
    {
        public WebSocketHandshakeException(string message, Exception innereException)
            : base(message, innereException)
        {
        }

        public WebSocketHandshakeException(string message)
            : base(message)
        {
        }

        public WebSocketHandshakeException(Exception innerException)
            : base(null, innerException)
        {
        }
    }
}
