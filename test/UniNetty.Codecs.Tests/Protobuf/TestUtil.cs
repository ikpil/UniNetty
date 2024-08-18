// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Tests.Protobuf
{
    using UniNetty.Buffers;

    static class TestUtil
    {
        internal static byte[] GetReadableBytes(IByteBuffer byteBuffer)
        {
            var data = new byte[byteBuffer.ReadableBytes];
            byteBuffer.ReadBytes(data);

            return data;
        }
    }
}
