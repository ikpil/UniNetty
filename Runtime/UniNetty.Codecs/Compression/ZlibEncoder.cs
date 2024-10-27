// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Compression
{
    using System.Threading.Tasks;
    using UniNetty.Buffers;

    public abstract class ZlibEncoder : MessageToByteEncoder<IByteBuffer>
    {
        public abstract bool IsClosed { get; }

        /**
         * Close this {@link ZlibEncoder} and so finish the encoding.
         *
         * The returned {@link ChannelFuture} will be notified once the
         * operation completes.
         */
        public abstract Task CloseAsync();
    }
}
