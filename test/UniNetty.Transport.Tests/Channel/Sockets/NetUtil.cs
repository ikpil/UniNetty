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

    public static class NetUtil
    {
        public static readonly IPAddress MULTICAST_IPV4 = IPAddress.Parse("230.0.0.1");
        public static readonly IPAddress MULTICAST_IPV6_LINKLOCAL = IPAddress.Parse("FF02::1");
        public static readonly IPAddress MULTICAST_IPV6_SITELOCAL = IPAddress.Parse("FF05::1");

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
                    // pass link-local
                    if (AddressFamily.InterNetworkV6 == addressFamily && ip.Address.IsIPv6LinkLocal)
                    {
                        continue;
                    }

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

        public static NetworkInterface MulticastInterface(AddressFamily addressFamily)
        {
            var nis = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var ni in nis)
            {
                if (!ni.SupportsMulticast)
                    continue;

                if (ni.OperationalStatus != OperationalStatus.Up)
                    continue;

                var ipProps = ni.GetIPProperties();

                foreach (var unicast in ipProps.UnicastAddresses)
                {
                    if (unicast.Address.AddressFamily == addressFamily)
                    {
                        return ni;
                    }
                }
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