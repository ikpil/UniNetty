// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.Tests.WebSockets
{
    using System;
    using UniNetty.Codecs.Http.WebSockets;

    public class WebSocketClientHandshaker08Test : WebSocketClientHandshaker07Test
    {
        protected override WebSocketClientHandshaker NewHandshaker(Uri uri) => new WebSocketClientHandshaker08(uri, WebSocketVersion.V08, null, false, null, 1024);
    }
}
