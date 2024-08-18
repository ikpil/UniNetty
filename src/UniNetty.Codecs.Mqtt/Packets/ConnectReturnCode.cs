// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Mqtt.Packets
{
    public enum ConnectReturnCode
    {
        Accepted = 0x00,
        RefusedUnacceptableProtocolVersion = 0X01,
        RefusedIdentifierRejected = 0x02,
        RefusedServerUnavailable = 0x03,
        RefusedBadUsernameOrPassword = 0x04,
        RefusedNotAuthorized = 0x05
    }
}