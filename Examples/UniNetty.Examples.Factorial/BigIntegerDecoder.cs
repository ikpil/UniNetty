// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Examples.Factorial
{

    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using UniNetty.Buffers;
    using UniNetty.Codecs;
    using UniNetty.Transport.Channels;

    public class BigIntegerDecoder : ByteToMessageDecoder
    {
        public override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            if (input.ReadableBytes < 5)
            {
                return;
            }

            input.MarkReaderIndex();

            int magicNumber = input.ReadByte();
            if (magicNumber != 'F')
            {
                input.ResetReaderIndex();
                throw new Exception("Invalid magic number: " + magicNumber);
            }

            int dataLength = input.ReadInt();
            if (input.ReadableBytes < dataLength)
            {
                input.ResetReaderIndex();
                return;
            }

            var decoded = new byte[dataLength];
            input.ReadBytes(decoded);

            output.Add(new BigInteger(decoded));
        }
    }
}