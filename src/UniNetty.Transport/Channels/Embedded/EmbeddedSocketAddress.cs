// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Channels.Embedded
{
    using System.Net;

    sealed class EmbeddedSocketAddress : EndPoint
    {
        public override string ToString() => "embedded";
    }
}