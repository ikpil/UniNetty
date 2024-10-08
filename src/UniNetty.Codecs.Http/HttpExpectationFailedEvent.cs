// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http
{
    // A user event designed to communicate that a expectation has failed and there should be no expectation that a
    // body will follow.
    public sealed class HttpExpectationFailedEvent
    {
        public static readonly HttpExpectationFailedEvent Default = new HttpExpectationFailedEvent();

        HttpExpectationFailedEvent()
        {
        }
    }
}
