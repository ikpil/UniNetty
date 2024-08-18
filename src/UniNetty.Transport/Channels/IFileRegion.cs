// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Channels
{
    using System.IO;
    using UniNetty.Common;

    public interface IFileRegion : IReferenceCounted
    {
        long Position { get; }

        long Transferred { get; }

        long Count { get; }

        long TransferTo(Stream target, long position);
    }
}
