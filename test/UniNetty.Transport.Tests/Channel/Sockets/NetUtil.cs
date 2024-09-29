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
        /**
         * See <a href="https://tools.ietf.org/html/rfc7346#section-2">RFC7346, IPv6 Multicast Address Scopes</a>
         */
        public static readonly IPAddress MULTICAST_IPV4 = IPAddress.Parse("224.0.0.2");

        public static readonly IPAddress MULTICAST_IPV6_SITE_LOCAL = IPAddress.Parse("FF05::1");

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

                var ipProp = ni.GetIPProperties();
                foreach (var ip in ipProp.UnicastAddresses)
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
            var nis = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var ni in nis)
            {
                if (ni.OperationalStatus != OperationalStatus.Up)
                    continue;

                if (ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    continue;

                var ipProps = ni.GetIPProperties();
                foreach (var multicast in ipProps.UnicastAddresses)
                {
                    if (multicast.Address.AddressFamily == addressFamily)
                    {
                        return ni;
                    }
                }
            }

            throw new NotSupportedException($"Address family {addressFamily} is not supported. Expecting InterNetwork/InterNetworkV6");
        }


        public static NetworkInterface MulticastInterface(AddressFamily addressFamily)
        {
            var nis = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var ni in nis)
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                    continue;
                
                if (!ni.SupportsMulticast)
                    continue;

                if (ni.OperationalStatus != OperationalStatus.Up)
                    continue;

                var ipProps = ni.GetIPProperties();
                if (0 >= ipProps.UnicastAddresses.Count)
                    continue;

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