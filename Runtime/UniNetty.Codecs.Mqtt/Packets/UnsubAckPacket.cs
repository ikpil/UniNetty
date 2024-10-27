// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Mqtt.Packets
{
    public sealed class UnsubAckPacket : PacketWithId
    {
        public override PacketType PacketType => PacketType.UNSUBACK;

        public static UnsubAckPacket InResponseTo(UnsubscribePacket unsubscribePacket)
        {
            return new UnsubAckPacket
            {
                PacketId = unsubscribePacket.PacketId
            };
        }
    }
}