// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.Multipart
{
    using System.Collections.Generic;
    using UniNetty.Common.Utilities;

    public interface IInterfaceHttpPostRequestDecoder
    {
        bool IsMultipart { get; }

        int DiscardThreshold { get; set; }

        List<IInterfaceHttpData> GetBodyHttpDatas();

        List<IInterfaceHttpData> GetBodyHttpDatas(AsciiString name);

        IInterfaceHttpData GetBodyHttpData(AsciiString name);

        IInterfaceHttpPostRequestDecoder Offer(IHttpContent content);

        bool HasNext { get; }

        IInterfaceHttpData Next();

        IInterfaceHttpData CurrentPartialHttpData { get; }

        void Destroy();

        void CleanFiles();

        void RemoveHttpDataFromClean(IInterfaceHttpData data);
    }
}
