// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs
{
    using System;

    public sealed class NullNameValidator<T> : INameValidator<T>
    {
        public void ValidateName(T name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
        }
    }
}
