// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.Multipart
{
    public interface IFileUpload : IHttpData
    {
        string FileName { get; set; }

        string ContentType { get; set; }

        string ContentTransferEncoding { get; set; }
    }
}
