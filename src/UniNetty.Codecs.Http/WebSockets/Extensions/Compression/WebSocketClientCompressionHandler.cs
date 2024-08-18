// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.WebSockets.Extensions.Compression
{
    public sealed class WebSocketClientCompressionHandler : WebSocketClientExtensionHandler
    {
        public static readonly WebSocketClientCompressionHandler Instance = new WebSocketClientCompressionHandler();

        public override bool IsSharable => true;

        WebSocketClientCompressionHandler()
            : base(new PerMessageDeflateClientExtensionHandshaker(),
                new DeflateFrameClientExtensionHandshaker(false),
                new DeflateFrameClientExtensionHandshaker(true))
        {
        }
    }
}
