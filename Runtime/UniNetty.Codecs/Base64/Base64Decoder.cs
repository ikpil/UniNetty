// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Base64
{
    using System.Collections.Generic;
    using UniNetty.Buffers;
    using UniNetty.Transport.Channels;

    public sealed class Base64Decoder : MessageToMessageDecoder<IByteBuffer>
    {
        readonly Base64Dialect dialect;

        public Base64Decoder()
            : this(Base64Dialect.STANDARD)
        {
        }

        public Base64Decoder(Base64Dialect dialect)
        {
            this.dialect = dialect;
        }

        public override void Decode(IChannelHandlerContext context, IByteBuffer message, List<object> output) => output.Add(Base64.Decode(message, this.dialect));

        public override bool IsSharable => true;
    }
}