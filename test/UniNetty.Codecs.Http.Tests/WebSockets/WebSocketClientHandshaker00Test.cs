// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.Tests.WebSockets
{
    using System;
    using UniNetty.Codecs.Http.WebSockets;
    using UniNetty.Common.Utilities;

    public class WebSocketClientHandshaker00Test : WebSocketClientHandshakerTest
    {
        protected override WebSocketClientHandshaker NewHandshaker(Uri uri) =>
            new WebSocketClientHandshaker00(uri, WebSocketVersion.V00, null, null, 1024);

        protected override AsciiString GetOriginHeaderName() => HttpHeaderNames.Origin;
    }
}
