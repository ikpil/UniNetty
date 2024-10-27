// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Channels.Pool
{
    using System.Threading.Tasks;
    using UniNetty.Common.Utilities;

    /// <summary>
    /// <see cref="IChannelHealthChecker"/> implementation that checks if <see cref="IChannel.Active"/> returns <c>true</c>.
    /// </summary>
    public class ChannelActiveHealthChecker : IChannelHealthChecker
    {
        public static readonly IChannelHealthChecker Instance;

        static ChannelActiveHealthChecker()
        {
            Instance = new ChannelActiveHealthChecker();
        }

        ChannelActiveHealthChecker()
        {
        }

        public ValueTask<bool> IsHealthyAsync(IChannel channel) => new ValueTask<bool>(channel.Active);
    }
}