// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Bootstrapping
{
    using System.Net;
    using System.Threading.Tasks;

    public interface INameResolver
    {
        bool IsResolved(EndPoint address);

        Task<EndPoint> ResolveAsync(EndPoint address);
    }
}