// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Tests.Channel.Sockets
{
    using System;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using UniNetty.Buffers;
    using UniNetty.Transport.Channels;
    using UniNetty.Transport.Channels.Sockets;

    static class NetUtil
    {
        internal static readonly AddressFamily[] AddressFamilyTypes =
        {
            AddressFamily.InterNetwork,
            AddressFamily.InterNetworkV6
        };

        internal static readonly IByteBufferAllocator[] Allocators =
        {
            PooledByteBufferAllocator.Default,
            UnpooledByteBufferAllocator.Default
        };

        public static bool IsSupport(AddressFamily addressFamily)
        {
            var nis = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var ni in nis)
            {
                if (ni.OperationalStatus != OperationalStatus.Up)
                    continue;

                var unicastAddresses = ni.GetIPProperties().UnicastAddresses;

                foreach (var ip in unicastAddresses)
                {
                    if (ip.Address.AddressFamily == addressFamily)
                    {
                        return true; // 지원됨
                    }
                }
            }

            return false;
        }

        public static IPAddress GetLoopbackAddress(AddressFamily addressFamily)
        {
            if (addressFamily == AddressFamily.InterNetwork)
            {
                return IPAddress.Loopback;
            }

            if (addressFamily == AddressFamily.InterNetworkV6)
            {
                return IPAddress.IPv6Loopback;
            }

            throw new NotSupportedException($"Address family {addressFamily} is not supported. Expecting InterNetwork/InterNetworkV6");
        }

        public static NetworkInterface LoopbackInterface(AddressFamily addressFamily)
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            if (addressFamily == AddressFamily.InterNetwork)
            {
                return networkInterfaces[NetworkInterface.LoopbackInterfaceIndex];
            }

            if (addressFamily == AddressFamily.InterNetworkV6)
            {
                return networkInterfaces[NetworkInterface.IPv6LoopbackInterfaceIndex];
            }

            throw new NotSupportedException($"Address family {addressFamily} is not supported. Expecting InterNetwork/InterNetworkV6");
        }

        internal class DummyHandler : SimpleChannelInboundHandler<DatagramPacket>
        {
            protected override void ChannelRead0(IChannelHandlerContext ctx, DatagramPacket msg)
            {
                // Do nothing
            }
        }
    }
}