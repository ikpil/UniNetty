// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.Multipart
{
    enum MultiPartStatus
    {
        Notstarted,
        Preamble,
        HeaderDelimiter,
        Disposition,
        Field,
        Fileupload,
        MixedPreamble,
        MixedDelimiter,
        MixedDisposition,
        MixedFileUpload,
        MixedCloseDelimiter,
        CloseDelimiter,
        PreEpilogue,
        Epilogue
    }
}
