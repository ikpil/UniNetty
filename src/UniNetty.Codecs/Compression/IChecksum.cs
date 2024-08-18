// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Compression
{
    interface IChecksum
    {
        void Update(byte[] buf, int index, int len);

        void Reset();

        void Reset(long init);

        long GetValue();

        IChecksum Copy();
    }
}
