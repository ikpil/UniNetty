// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using UniNetty.Common.Internal.Logging;

namespace UniNetty.Examples.Telnet.Server
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using UniNetty.Transport.Channels;

    public class TelnetServerHandler : SimpleChannelInboundHandler<string>
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<TelnetServerHandler>();

        public override void ChannelActive(IChannelHandlerContext contex)
        {
            contex.WriteAsync($"Welcome to {Dns.GetHostName()} !\r\n");
            contex.WriteAndFlushAsync($"It is {DateTime.Now} now !\r\n");
        }

        protected override void ChannelRead0(IChannelHandlerContext contex, string msg)
        {
            // Generate and write a response.
            string response;
            bool close = false;
            if (string.IsNullOrEmpty(msg))
            {
                response = "Please type something.\r\n";
            }
            else if (string.Equals("bye", msg, StringComparison.OrdinalIgnoreCase))
            {
                response = "Have a good day!\r\n";
                close = true;
            }
            else
            {
                response = "Did you say '" + msg + "'?\r\n";
            }

            Task wait_close = contex.WriteAndFlushAsync(response);
            if (close)
            {
                Task.WaitAll(wait_close);
                contex.CloseAsync();
            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext contex)
        {
            contex.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext contex, Exception e)
        {
            Logger.Info("{0}", e.StackTrace);
            contex.CloseAsync();
        }

        public override bool IsSharable => true;
    }
}