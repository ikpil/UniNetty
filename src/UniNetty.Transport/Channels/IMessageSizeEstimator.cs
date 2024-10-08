// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Channels
{
    public interface IMessageSizeEstimator
    {
        /// <summary>
        ///     Creates a new handle. The handle provides the actual operations.
        /// </summary>
        IMessageSizeEstimatorHandle NewHandle();
    }
}