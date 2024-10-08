// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http
{
    using UniNetty.Codecs.Compression;
    using UniNetty.Common.Utilities;
    using UniNetty.Transport.Channels.Embedded;

    public class HttpContentDecompressor : HttpContentDecoder
    {
        readonly bool strict;

        public HttpContentDecompressor() : this(false)
        {
        }

        public HttpContentDecompressor(bool strict)
        {
            this.strict = strict;
        }

        protected override EmbeddedChannel NewContentDecoder(ICharSequence contentEncoding)
        {
            if (HttpHeaderValues.Gzip.ContentEqualsIgnoreCase(contentEncoding) 
                || HttpHeaderValues.XGzip.ContentEqualsIgnoreCase(contentEncoding))
            {
                return new EmbeddedChannel(
                    this.HandlerContext.Channel.Id, 
                    this.HandlerContext.Channel.Metadata.HasDisconnect, 
                    this.HandlerContext.Channel.Configuration, 
                    ZlibCodecFactory.NewZlibDecoder(ZlibWrapper.Gzip));
            }

            if (HttpHeaderValues.Deflate.ContentEqualsIgnoreCase(contentEncoding) 
                || HttpHeaderValues.XDeflate.ContentEqualsIgnoreCase(contentEncoding))
            {
                ZlibWrapper wrapper = this.strict ? ZlibWrapper.Zlib : ZlibWrapper.ZlibOrNone;
                return new EmbeddedChannel(
                    this.HandlerContext.Channel.Id, 
                    this.HandlerContext.Channel.Metadata.HasDisconnect, 
                    this.HandlerContext.Channel.Configuration,
                    ZlibCodecFactory.NewZlibDecoder(wrapper));
            }

            // 'identity' or unsupported
            return null;
        }
    }
}
