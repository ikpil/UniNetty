// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.WebSockets
{
    using System.Text;
    using UniNetty.Buffers;

    public class TextWebSocketFrame : WebSocketFrame
    {
        public TextWebSocketFrame()
            : base(Unpooled.Buffer(0))
        {
        }

        public TextWebSocketFrame(string text)
            : base(FromText(text))
        {
        }

        public TextWebSocketFrame(IByteBuffer binaryData)
            : base(binaryData)
        {
        }

        public TextWebSocketFrame(bool finalFragment, int rsv, string text)
            : base(finalFragment, rsv, FromText(text))
        {
        }

        static IByteBuffer FromText(string text) => string.IsNullOrEmpty(text) 
            ? Unpooled.Empty : Unpooled.CopiedBuffer(text, Encoding.UTF8);

        public TextWebSocketFrame(bool finalFragment, int rsv, IByteBuffer binaryData)
            : base(finalFragment, rsv, binaryData)
        {
        }

        public string Text() => this.Content.ToString(Encoding.UTF8);

        public override IByteBufferHolder Replace(IByteBuffer content) => new TextWebSocketFrame(this.IsFinalFragment, this.Rsv, content);
    }
}
