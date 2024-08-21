// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Base64
{
    using System.Collections.Generic;
    using UniNetty.Buffers;
    using UniNetty.Transport.Channels;

    public sealed class Base64Encoder : MessageToMessageDecoder<IByteBuffer>
    {
        readonly bool breakLines;
        readonly Base64Dialect dialect;

        public Base64Encoder() : this(true) { }

        public Base64Encoder(bool breakLines) : this(breakLines, Base64Dialect.STANDARD)
        {
        }

        public Base64Encoder(bool breakLines, Base64Dialect dialect)
        {
            this.breakLines = breakLines;
            this.dialect = dialect;
        }

        public override void Decode(IChannelHandlerContext context, IByteBuffer message, List<object> output) => output.Add(Base64.Encode(message, this.breakLines, this.dialect));

        public override bool IsSharable => true;
    }
}