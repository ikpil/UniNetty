// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.Tests.Multipart
{
    using UniNetty.Codecs.Http.Multipart;
    using Xunit;

    public sealed class DiskFileUploadTest
    {
        [Fact]
        public void DiskFileUploadEquals()
        {
            var f2 = new DiskFileUpload("d1", "d1", "application/json", null, null, 100);
            Assert.Equal(f2, f2);
        }
    }
}
