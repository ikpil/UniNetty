// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Channels
{
    using System.Net;
    using UniNetty.Common;

    public interface IAddressedEnvelope<out T> : IReferenceCounted
    {
        T Content { get; }

        EndPoint Sender { get; }

        EndPoint Recipient { get; }
    }
}