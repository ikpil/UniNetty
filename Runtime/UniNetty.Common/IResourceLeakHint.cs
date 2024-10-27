// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Common
{
    /// <summary>
    ///     A hint object that provides human-readable message for easier resource leak tracking.
    /// </summary>
    public interface IResourceLeakHint
    {
        /// <summary>
        ///     Returns a human-readable message that potentially enables easier resource leak tracking.
        /// </summary>
        /// <returns></returns>
        string ToHintString();
    }
}