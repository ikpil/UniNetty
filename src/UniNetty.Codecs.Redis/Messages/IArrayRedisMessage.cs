// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Redis.Messages
{
    using System.Collections.Generic;
    using UniNetty.Common;

    public interface IArrayRedisMessage : IReferenceCounted, IRedisMessage
    {
        bool IsNull { get; }

        IList<IRedisMessage> Children { get; }
    }
}
