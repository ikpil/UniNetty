// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Common.Internal
{
    using UniNetty.Common.Utilities;

    public interface IAppendable
    {
        IAppendable Append(char c);

        IAppendable Append(ICharSequence sequence);

        IAppendable Append(ICharSequence sequence, int start, int end);
    }
}
