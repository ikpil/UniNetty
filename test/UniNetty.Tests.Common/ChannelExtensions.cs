// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Tests.Common
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UniNetty.Transport.Channels;

    public static class ChannelExtensions
    {
        public static Task WriteAndFlushManyAsync(this IChannel channel, params object[] messages)
        {
            var list = new List<Task>();
            foreach (object m in messages)
            {
                list.Add(channel.WriteAsync(m));
            }
            IEnumerable<Task> tasks = list.ToArray();
            channel.Flush();
            return Task.WhenAll(tasks);
        }
    }
}