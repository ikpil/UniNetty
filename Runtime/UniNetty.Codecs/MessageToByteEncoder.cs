// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;
    using UniNetty.Buffers;
    using UniNetty.Common.Utilities;
    using UniNetty.Transport.Channels;

    public abstract class MessageToByteEncoder<T> : ChannelHandlerAdapter
    {
        public virtual bool AcceptOutboundMessage(object message) => message is T;

        public override Task WriteAsync(IChannelHandlerContext context, object message)
        {
            Contract.Requires(context != null);

            IByteBuffer buffer = null;
            Task result;
            try
            {
                if (this.AcceptOutboundMessage(message))
                {
                    buffer = this.AllocateBuffer(context);
                    var input = (T)message;
                    try
                    {
                        this.Encode(context, input, buffer);
                    }
                    finally
                    {
                        ReferenceCountUtil.Release(input);
                    }

                    if (buffer.IsReadable())
                    {
                        result = context.WriteAsync(buffer);
                    }
                    else
                    {
                        buffer.Release();
                        result = context.WriteAsync(Unpooled.Empty);
                    }

                    buffer = null;
                }
                else
                {
                    return context.WriteAsync(message);
                }
            }
            catch (EncoderException e)
            {
                return TaskEx.FromException(e);
            }
            catch (Exception ex)
            {
                return TaskEx.FromException(new EncoderException(ex));
            }
            finally
            {
                buffer?.Release();
            }

            return result;
        }

        protected virtual IByteBuffer AllocateBuffer(IChannelHandlerContext context)
        {
            Contract.Requires(context != null);

            return context.Allocator.Buffer();
        }

        public abstract void Encode(IChannelHandlerContext context, T message, IByteBuffer output);
    }
}
