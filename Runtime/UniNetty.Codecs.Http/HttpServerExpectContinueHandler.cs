// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http
{
    using System.Threading.Tasks;
    using UniNetty.Buffers;
    using UniNetty.Common.Utilities;
    using UniNetty.Transport.Channels;

    public class HttpServerExpectContinueHandler : ChannelHandlerAdapter
    {
        static readonly IFullHttpResponse ExpectationFailed = new DefaultFullHttpResponse(
            HttpVersion.Http11, HttpResponseStatus.ExpectationFailed, Unpooled.Empty);

        static readonly IFullHttpResponse Accept = new DefaultFullHttpResponse(
            HttpVersion.Http11, HttpResponseStatus.Continue, Unpooled.Empty);

        static HttpServerExpectContinueHandler()
        {
            ExpectationFailed.Headers.Set(HttpHeaderNames.ContentLength, HttpHeaderValues.Zero);
            Accept.Headers.Set(HttpHeaderNames.ContentLength, HttpHeaderValues.Zero);
        }

        protected virtual IHttpResponse AcceptMessage(IHttpRequest request) => (IHttpResponse)Accept.RetainedDuplicate();

        protected virtual IHttpResponse RejectResponse(IHttpRequest request) => (IHttpResponse)ExpectationFailed.RetainedDuplicate();

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            if (message is IHttpRequest req)
            {
                if (HttpUtil.Is100ContinueExpected(req))
                {
                    IHttpResponse accept = this.AcceptMessage(req);

                    if (accept == null)
                    {
                        // the expectation failed so we refuse the request.
                        IHttpResponse rejection = this.RejectResponse(req);
                        ReferenceCountUtil.Release(message);
                        context.WriteAndFlushAsync(rejection)
                            .ContinueWith(CloseOnFailure, context, TaskContinuationOptions.ExecuteSynchronously);
                        return;
                    }

                    context.WriteAndFlushAsync(accept)
                        .ContinueWith(CloseOnFailure, context, TaskContinuationOptions.ExecuteSynchronously);
                    req.Headers.Remove(HttpHeaderNames.Expect);
                }
                base.ChannelRead(context, message);
            }
        }

        static Task CloseOnFailure(Task task, object state)
        {
            if (task.IsFaulted)
            {
                var context = (IChannelHandlerContext)state;
                return context.CloseAsync();
            }
            return TaskEx.Completed;
        }
    }
}
