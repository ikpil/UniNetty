// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace UniNetty.Examples.Factorial
{
    using System.Collections.Generic;
    using UniNetty.Buffers;
    using UniNetty.Codecs;
    using UniNetty.Transport.Channels;

    public class NumberEncoder : MessageToMessageEncoder<BigInteger>
    {
        public override void Encode(IChannelHandlerContext context, BigInteger message, List<object> output)
        {
            IByteBuffer buffer = context.Allocator.Buffer();

            //https://msdn.microsoft.com/en-us/library/BigInteger.tobytearray(v=vs.110).aspx
            //BigInteger.ToByteArray() return a Little-Endian bytes
            //IByteBuffer is Big-Endian by default
            byte[] data = message.ToByteArray();
            buffer.WriteByte((byte)'F');
            buffer.WriteInt(data.Length);
            buffer.WriteBytes(data);
            output.Add(buffer);
        }
    }
}