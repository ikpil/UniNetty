// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.WebSockets
{
    using UniNetty.Transport.Channels;

    /// <summary>
    ///     Marker interface which all WebSocketFrame encoders need to implement. This makes it 
    ///     easier to access the added encoder later in the <see cref="IChannelPipeline"/>.
    /// </summary>
    public interface IWebSocketFrameEncoder : IChannelHandler
    {
    }
}
